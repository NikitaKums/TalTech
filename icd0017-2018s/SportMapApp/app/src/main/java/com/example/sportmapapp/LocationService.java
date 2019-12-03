package com.example.sportmapapp;

import android.Manifest;
import android.app.Notification;
import android.app.NotificationChannel;
import android.app.NotificationManager;
import android.app.PendingIntent;
import android.app.Service;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.content.pm.PackageManager;
import android.location.Criteria;
import android.location.Location;
import android.location.LocationListener;
import android.location.LocationManager;
import android.os.Build;
import android.os.Bundle;
import android.os.IBinder;
import android.os.SystemClock;
import android.os.VibrationEffect;
import android.os.Vibrator;
import android.support.v4.app.ActivityCompat;
import android.support.v4.app.NotificationCompat;
import android.support.v4.app.NotificationManagerCompat;
import android.support.v4.content.LocalBroadcastManager;
import android.util.Log;
import android.widget.Chronometer;
import android.widget.RemoteViews;

import com.example.sportmapapp.Domain.DBLocation;
import com.example.sportmapapp.Domain.Session;
import com.example.sportmapapp.Repositories.LocationRepository;
import com.example.sportmapapp.Repositories.SessionRepository;

import java.util.Timer;
import java.util.TimerTask;

public class LocationService extends Service implements LocationListener {
    static private final String TAG = LocationService.class.getSimpleName();
    private BroadcastReceiver mBroadcastReceiver;

    NotificationManagerCompat mNotificationManagerCompat;

    private LocationManager mLocationManager;
    private String mLocationProvider;
    private Location mPreviousLocation;
    private Location mCurrentLocation;

    private LocationRepository mLocationRepository;
    private SessionRepository mSessionRepository;
    private double mReachedCheckPointLat = -1;
    private double mReachedCheckPointLong = -1;
    private int mCurrentSessionId;

    private Timer mTimer;
    private float mTotalDistance = 0;

    private float mSpeed;

    private int mWayPointDirectDistance;
    private int mWayPointTotalDistance;
    private Location mLocationOnWayPointSet;
    private boolean mShouldCheckWayPoint;

    private int mCheckPointDirectDistance;
    private int mCheckPointTotalDistance;
    private Location mLocationOnCheckPointSet;
    private boolean mShouldCheckCheckPoint;

    private Chronometer mTotalTimeChronometer;
    private Chronometer mWayPointChronometer;
    private Chronometer mCheckPointChronometer;
    private long mWayPointChronometerTime;
    private long mCheckPointChronometerTime;

    @Override
    public void onCreate() {
        Log.d(TAG, "onCreate");
        super.onCreate();

        mLocationManager = (LocationManager) getSystemService(Context.LOCATION_SERVICE);

        Criteria locationSelectionCriteria = new Criteria();
        mLocationProvider = mLocationManager.getBestProvider(locationSelectionCriteria, false);

        mTotalTimeChronometer = new Chronometer(this);
        mTotalTimeChronometer.setBase(SystemClock.elapsedRealtime());
        mTotalTimeChronometer.start();
        mWayPointChronometer = new Chronometer(this);
        mCheckPointChronometer = new Chronometer(this);
        startTimer();

        mLocationRepository = new LocationRepository(this);
        mLocationRepository.open();
        mSessionRepository = new SessionRepository(this);
        mSessionRepository.open();
        mSessionRepository.add(new Session("0", "0", "0"));

        mCurrentSessionId = mSessionRepository.getLastSessionId();

        if (ActivityCompat.checkSelfPermission(this, Manifest.permission.ACCESS_FINE_LOCATION) != PackageManager.PERMISSION_GRANTED && ActivityCompat.checkSelfPermission(this, Manifest.permission.ACCESS_COARSE_LOCATION) != PackageManager.PERMISSION_GRANTED) {
            // TODO: Consider calling
            //    ActivityCompat#requestPermissions
            // here to request the missing permissions, and then overriding
            //   public void onRequestPermissionsResult(int requestCode, String[] permissions,
            //                                          int[] grantResults)
            // to handle the case where the user grants the permission. See the documentation
            // for ActivityCompat#requestPermissions for more details.
            return;
        }
        Location location = mLocationManager.getLastKnownLocation(mLocationProvider);

        if (location != null) {
            onLocationChanged(location);
        }

        IntentFilter intentFilter = new IntentFilter();
        intentFilter.addAction(C.WAYPOINT_ACTIVITY);
        intentFilter.addAction(C.CHECKPOINT_ACTIVITY);
        intentFilter.addAction(C.MAPS_ACTIVITY_STOPPED);
        intentFilter.addAction(C.SET_CHECKPOINT);
        intentFilter.addAction(C.SET_WAYPOINT);
        intentFilter.addAction(C.NOTIFICATION_INTENT_ACTIVITY);
        intentFilter.addAction(C.NOTIFICATION_INTENT_CHECKPOINT);
        intentFilter.addAction(C.NOTIFICATION_INTENT_WAYPOINT);
        intentFilter.addAction(C.REMOVE_NOTIFICATION);
        intentFilter.addAction(C.CURRENT_SESSION_ID);

        createNotificationChannel();

        mBroadcastReceiver = new LocationServiceBroadCastReceiver();
        LocalBroadcastManager.getInstance(getApplicationContext()).registerReceiver(mBroadcastReceiver, intentFilter);

    }

    @Override
    public int onStartCommand(Intent intent, int flags, int startId) {
        Log.d(TAG, "onStartCommand");

        if (ActivityCompat.checkSelfPermission(this, Manifest.permission.ACCESS_FINE_LOCATION) != PackageManager.PERMISSION_GRANTED && ActivityCompat.checkSelfPermission(this, Manifest.permission.ACCESS_COARSE_LOCATION) != PackageManager.PERMISSION_GRANTED) {
            // TODO: Consider calling
            //    ActivityCompat#requestPermissions
            // here to request the missing permissions, and then overriding
            //   public void onRequestPermissionsResult(int requestCode, String[] permissions,
            //                                          int[] grantResults)
            // to handle the case where the user grants the permission. See the documentation
            // for ActivityCompat#requestPermissions for more details.
            return Service.START_NOT_STICKY;
        }
        mLocationManager.requestLocationUpdates(mLocationProvider, 1000, 1, this);
        return Service.START_NOT_STICKY;
    }

    @Override
    public void onDestroy() {
        Log.d(TAG, "onDestroy");
        super.onDestroy();
        mLocationManager.removeUpdates(this);
        removeNotification();
        mTotalTimeChronometer.stop();
        removeNotification();
        if (mCheckPointChronometer != null) {
            mCheckPointChronometer.stop();
        }
        if (mWayPointChronometer != null) {
            mWayPointChronometer.stop();
        }
        mSessionRepository.update(new Session(mCurrentSessionId, String.valueOf((int)mTotalDistance),
                MapsActivity.secondsToHMS((SystemClock.elapsedRealtime() - mCheckPointChronometer.getBase()) / 1000),
                String.format("%.2f", mSpeed)));

        mLocationRepository.close();
        mSessionRepository.close();
        mTimer.cancel();
    }

    // send intents to MapsActivity each second to update timers
    private void startTimer() {
        Log.d(TAG, "startTimer");
        mTimer = new Timer();
        mTimer.schedule(new TimerTask() {
            @Override
            public void run() {
                displayNotification();
                Intent currentSessionIntent = new Intent(C.CURRENT_SESSION_ID_RECEIVE);
                currentSessionIntent.putExtra(C.CURRENT_SESSION_ID_ID, mCurrentSessionId);
                LocalBroadcastManager.getInstance(getApplicationContext()).sendBroadcast(currentSessionIntent);

                Intent updateTimersInUIIntent = new Intent(C.TIMERS);
                updateTimersInUIIntent.putExtra(C.TOTAL_TIMER, ((SystemClock.elapsedRealtime() - mTotalTimeChronometer.getBase()) / 1000));
                if (mCheckPointChronometer != null && mShouldCheckCheckPoint) {
                    updateTimersInUIIntent.putExtra(C.CHECKPOINT_TIMER, ((SystemClock.elapsedRealtime() - mCheckPointChronometer.getBase()) / 1000));
                }
                if (mCheckPointChronometer != null && !mShouldCheckCheckPoint) {
                    updateTimersInUIIntent.putExtra(C.CHECKPOINT_TIMER, mCheckPointChronometerTime);
                }
                if (mWayPointChronometer != null && mShouldCheckWayPoint) {
                    updateTimersInUIIntent.putExtra(C.WAYPOINT_TIMER, ((SystemClock.elapsedRealtime() - mWayPointChronometer.getBase()) / 1000));
                }
                if (mWayPointChronometer != null && !mShouldCheckWayPoint) {
                    updateTimersInUIIntent.putExtra(C.WAYPOINT_TIMER, mWayPointChronometerTime);
                }
                LocalBroadcastManager.getInstance(getApplicationContext()).sendBroadcast(updateTimersInUIIntent);

            }
        }, 0, 1000);
    }

    @Override
    public IBinder onBind(Intent intent) {
        return null;
    }

    @Override
    public void onLocationChanged(Location location) {
        Log.d(TAG, "onLocationChanged " + location.getLatitude() + " " + location.getLongitude());

        /*Date d = new Date(location.getTime());

        SimpleDateFormat sdf = new SimpleDateFormat("MM-dd-YYYY'T'HH:mm:ss");

        System.out.println(sdf.format(d));*/

        mCurrentLocation = location;
        mSpeed = getSpeed();
        if (mPreviousLocation != null) {
            mTotalDistance += mPreviousLocation.distanceTo(location);

            Intent totalDistanceIntent = new Intent(C.TOTAL_DISTANCE);
            totalDistanceIntent.putExtra(C.TOTAL_DISTANCE_DISTANCE, (int) mTotalDistance);
            LocalBroadcastManager.getInstance(getApplicationContext()).sendBroadcast(totalDistanceIntent);

            Intent previousLocationIntent = new Intent(C.PREVIOUS_LOCATION);
            previousLocationIntent.putExtra(C.PREVIOUS_LOCATION_LAT, mPreviousLocation.getLatitude());
            previousLocationIntent.putExtra(C.PREVIOUS_LOCATION_LONG, mPreviousLocation.getLongitude());
            LocalBroadcastManager.getInstance(getApplicationContext()).sendBroadcast(previousLocationIntent);
        }

        Intent speedIntent = new Intent(C.SPEED);
        speedIntent.putExtra(C.SPEED_SPEED, mSpeed);
        LocalBroadcastManager.getInstance(getApplicationContext()).sendBroadcast(speedIntent);

        Intent notifyMapOfLocationChangeIntent = new Intent(C.LOCATION_UPDATE);
        notifyMapOfLocationChangeIntent.putExtra(C.LOCATION_UPDATE_LAT, location.getLatitude());
        notifyMapOfLocationChangeIntent.putExtra(C.LOCATION_UPDATE_LONG, location.getLongitude());
        LocalBroadcastManager.getInstance(getApplicationContext()).sendBroadcast(notifyMapOfLocationChangeIntent);

        // ========================= WAYPOINT =========================
        if (mLocationOnWayPointSet != null && mShouldCheckWayPoint) {

            mWayPointChronometerTime = (SystemClock.elapsedRealtime() - mWayPointChronometer.getBase()) / 1000;
            wayPointChecker();
            mWayPointTotalDistance += mPreviousLocation.distanceTo(mCurrentLocation);

            Intent wayPointDirectDistanceIntent = new Intent(C.WAYPOINT_DIRECT_DISTANCE);
            wayPointDirectDistanceIntent.putExtra(C.WAYPOINT_DIRECT_DISTANCE_DISTANCE, (int) mLocationOnWayPointSet.distanceTo(mCurrentLocation));
            LocalBroadcastManager.getInstance(getApplicationContext()).sendBroadcast(wayPointDirectDistanceIntent);


            Intent wayPointTotalDistanceIntent = new Intent(C.WAYPOINT_TOTAL_DISTANCE);
            wayPointTotalDistanceIntent.putExtra(C.WAYPOINT_TOTAL_DISTANCE_DISTANCE, mWayPointTotalDistance);
            LocalBroadcastManager.getInstance(getApplicationContext()).sendBroadcast(wayPointTotalDistanceIntent);

        }

        // ========================= CHECKPOINT =========================
        if (mLocationOnCheckPointSet != null && mShouldCheckCheckPoint) {

            mCheckPointChronometerTime = (SystemClock.elapsedRealtime() - mCheckPointChronometer.getBase()) / 1000;
            checkPointChecker();
            mCheckPointTotalDistance += mPreviousLocation.distanceTo(mCurrentLocation);

            Intent checkPointDirectDistanceIntent = new Intent(C.CHECKPOINT_DIRECT_DISTANCE);
            checkPointDirectDistanceIntent.putExtra(C.CHECKPOINT_DIRECT_DISTANCE_DISTANCE, (int) mLocationOnCheckPointSet.distanceTo(mCurrentLocation));
            LocalBroadcastManager.getInstance(getApplicationContext()).sendBroadcast(checkPointDirectDistanceIntent);


            Intent checkPointTotalDistanceIntent = new Intent(C.CHECKPOINT_TOTAL_DISTANCE);
            checkPointTotalDistanceIntent.putExtra(C.CHECKPOINT_TOTAL_DISTANCE_DISTANCE, mCheckPointTotalDistance);
            LocalBroadcastManager.getInstance(getApplicationContext()).sendBroadcast(checkPointTotalDistanceIntent);

        }
        DBLocation locationToInsert;
        if (mReachedCheckPointLong != -1 && mReachedCheckPointLat != -1){
            // location with checkpoint stuff
            locationToInsert = new DBLocation(String.valueOf(mCurrentLocation.getLatitude()),
                    String.valueOf(mCurrentLocation.getLongitude()),
                    String.valueOf(mReachedCheckPointLat),
                    String.valueOf(mReachedCheckPointLong),
                    mCurrentSessionId);
        } else {
            // location without checkpoint stuff
            locationToInsert = new DBLocation(String.valueOf(mCurrentLocation.getLatitude()),
                    String.valueOf(mCurrentLocation.getLongitude()),
                    null,
                    null,
                    mCurrentSessionId);
        }
        mLocationRepository.add(locationToInsert);
        mReachedCheckPointLat = -1;
        mReachedCheckPointLong = -1;
        mPreviousLocation = location;
    }

    public void wayPointChecker() {
        if (mLocationOnWayPointSet.distanceTo(mCurrentLocation) >= mWayPointDirectDistance) {

            vibratePhone();

            Intent wayPointReachedIntent = new Intent(C.WAYPOINT_REACHED);
            wayPointReachedIntent.putExtra(C.WAYPOINT_REACHED_LAT, mCurrentLocation.getLatitude());
            wayPointReachedIntent.putExtra(C.WAYPOINT_REACHED_LONG, mCurrentLocation.getLongitude());
            LocalBroadcastManager.getInstance(getApplicationContext()).sendBroadcast(wayPointReachedIntent);

            mShouldCheckWayPoint = false;
            mWayPointChronometer.stop();
        }
    }

    public void checkPointChecker() {
        if (mLocationOnCheckPointSet.distanceTo(mCurrentLocation) >= mCheckPointDirectDistance) {

            vibratePhone();

            Intent checkPointReachedIntent = new Intent(C.CHECKPOINT_REACHED);
            checkPointReachedIntent.putExtra(C.CHECKPOINT_REACHED_LAT, mCurrentLocation.getLatitude());
            checkPointReachedIntent.putExtra(C.CHECKPOINT_REACHED_LONG, mCurrentLocation.getLongitude());
            LocalBroadcastManager.getInstance(getApplicationContext()).sendBroadcast(checkPointReachedIntent);

            mReachedCheckPointLat = mCurrentLocation.getLatitude();
            mReachedCheckPointLong = mCurrentLocation.getLongitude();

            mShouldCheckCheckPoint = false;
            mCheckPointChronometer.stop();
        }
    }

    private void createNotificationChannel() {
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.O) {
            NotificationChannel channel = new NotificationChannel(C.NOTIFICATION_CHANNEL_ID,
                    "SportMap notifications", NotificationManager.IMPORTANCE_DEFAULT);

            channel.setDescription("SportMap notifications");
            NotificationManager notificationManager = getSystemService(NotificationManager.class);
            notificationManager.createNotificationChannel(channel);
            channel.setSound(null, null);
        }
    }

    private void vibratePhone(){
        Vibrator v = (Vibrator) getSystemService(Context.VIBRATOR_SERVICE);
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.O) {
            v.vibrate(VibrationEffect.createOneShot(500, VibrationEffect.DEFAULT_AMPLITUDE));
        } else {
            v.vibrate(500);
        }
    }

    private void displayNotification() {
        RemoteViews remoteView = new RemoteViews(getPackageName(), R.layout.notification);
        remoteView.setTextViewText(R.id.textViewNotificationTotalDistance, String.valueOf((int) mTotalDistance) + " m");
        remoteView.setTextViewText(R.id.textViewNotificationTotalTime, MapsActivity.secondsToHMS((SystemClock.elapsedRealtime() - mTotalTimeChronometer.getBase()) / 1000));
        float speed = getSpeed();
        if (speed >= Float.MAX_VALUE) {
            speed = 0;
        }
        remoteView.setTextViewText(R.id.textViewNotificationPace, String.format("%.2f", speed) + " km/min");

        if (mShouldCheckCheckPoint) {
            remoteView.setTextViewText(R.id.textViewNotificationCheckPointTotalDistance, String.valueOf(mCheckPointTotalDistance) + " m");
            remoteView.setTextViewText(R.id.textViewNotificationCheckPointDirectDistance, String.valueOf((int) mLocationOnCheckPointSet.distanceTo(mCurrentLocation)) + " m");
            remoteView.setTextViewText(R.id.textViewNotificationCheckPointTime, MapsActivity.secondsToHMS((SystemClock.elapsedRealtime() - mCheckPointChronometer.getBase()) / 1000));
        }
        if (mShouldCheckWayPoint) {
            remoteView.setTextViewText(R.id.textViewNotificationWayPointTotalDistance, String.valueOf(mWayPointTotalDistance) + " m");
            remoteView.setTextViewText(R.id.textViewNotificationWayPointDirectDistance, String.valueOf((int) mLocationOnWayPointSet.distanceTo(mCurrentLocation)) + " m");
            remoteView.setTextViewText(R.id.textViewNotificationWayPointTime, MapsActivity.secondsToHMS((SystemClock.elapsedRealtime() - mWayPointChronometer.getBase()) / 1000));
        }

        Intent checkPointIntent = new Intent(this, PointSettingActivity.class);
        checkPointIntent.putExtra(C.WHICH_POINT_TO_SET, C.CHECKPOINT);
        PendingIntent pendingCheckPointIntent = PendingIntent.getActivity(getApplicationContext(), 0, checkPointIntent, 0);
        remoteView.setOnClickPendingIntent(R.id.imageButtonNotificationCheckPointActivity, pendingCheckPointIntent);

        Intent wayPointIntent = new Intent(this, PointSettingActivity.class);
        wayPointIntent.putExtra(C.WHICH_POINT_TO_SET, C.WAYPOINT);
        PendingIntent pendingWayPointIntent = PendingIntent.getActivity(getApplicationContext(), 1, wayPointIntent, 0);
        remoteView.setOnClickPendingIntent(R.id.imageButtonNotificationWayPointActivity, pendingWayPointIntent);

        Intent resumeActivityIntent = new Intent(this, MapsActivity.class);
        resumeActivityIntent.setAction(Intent.ACTION_MAIN);
        resumeActivityIntent.addCategory(Intent.CATEGORY_LAUNCHER);
        resumeActivityIntent.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
        PendingIntent resumeActivityPendingIntent = PendingIntent.getActivity(this, 2, resumeActivityIntent, 0);
        remoteView.setOnClickPendingIntent(R.id.imageButtonNotificationActivity, resumeActivityPendingIntent);

        Notification customNotification = new NotificationCompat.Builder(this, C.NOTIFICATION_CHANNEL_ID)
                .setSmallIcon(R.drawable.baseline_gps_fixed_black_18dp)
                .setCustomContentView(remoteView)
                .build();

        customNotification.flags = NotificationCompat.FLAG_ONGOING_EVENT;

        mNotificationManagerCompat = NotificationManagerCompat.from(this);
        mNotificationManagerCompat.notify(C.NOTIFICATION_ID, customNotification);
    }

    private void removeNotification() {
        mNotificationManagerCompat.cancelAll();
    }


    @Override
    public void onStatusChanged(String provider, int status, Bundle extras) {
        Log.d(TAG, "onStatusChanged");
    }

    @Override
    public void onProviderEnabled(String provider) {
        Log.d(TAG, "onProviderEnabled");
    }

    @Override
    public void onProviderDisabled(String provider) {
        Log.d(TAG, "onProviderDisabled");
    }

    public float getSpeed() {
        float seconds = ((SystemClock.elapsedRealtime() - mTotalTimeChronometer.getBase()) / 1000);
        return (mTotalDistance / 1000) / (seconds / 60);
    }

    public class LocationServiceBroadCastReceiver extends BroadcastReceiver {

        @Override
        public void onReceive(Context context, Intent intent) {
            Log.d(TAG, "onReceive " + intent.getAction());
            switch (intent.getAction()) {
                case C.WAYPOINT_ACTIVITY:
                    mShouldCheckWayPoint = true;
                    mWayPointTotalDistance = 0;
                    mLocationOnWayPointSet = mCurrentLocation;
                    mWayPointDirectDistance = intent.getExtras().getInt(C.WAYPOINT_DISTANCE);
                    mWayPointChronometer.setBase(SystemClock.elapsedRealtime());
                    mWayPointChronometer.start();
                    break;
                case C.CHECKPOINT_ACTIVITY:
                    mShouldCheckCheckPoint = true;
                    mCheckPointTotalDistance = 0;
                    mLocationOnCheckPointSet = mCurrentLocation;
                    mCheckPointDirectDistance = intent.getExtras().getInt(C.CHECKPOINT_DISTANCE);
                    mCheckPointChronometer.setBase(SystemClock.elapsedRealtime());
                    mCheckPointChronometer.start();
                    break;
                case C.NOTIFICATION_INTENT_ACTIVITY:
                    break;
                case C.NOTIFICATION_INTENT_CHECKPOINT:
                    break;
                case C.NOTIFICATION_INTENT_WAYPOINT:
                    break;
                case C.REMOVE_NOTIFICATION:
                    removeNotification();
                    break;
            }
        }
    }
}
