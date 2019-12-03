package com.example.sportmapapp;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.IntentFilter;
import android.graphics.Color;
import android.hardware.Sensor;
import android.hardware.SensorEvent;
import android.hardware.SensorEventListener;
import android.hardware.SensorManager;
import android.location.Location;
import android.support.v4.app.FragmentActivity;
import android.os.Bundle;
import android.support.v4.content.LocalBroadcastManager;
import android.support.v7.app.AlertDialog;
import android.util.Log;
import android.view.View;
import android.view.animation.Animation;
import android.view.animation.RotateAnimation;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;

import com.google.android.gms.maps.CameraUpdateFactory;
import com.google.android.gms.maps.GoogleMap;
import com.google.android.gms.maps.OnMapReadyCallback;
import com.google.android.gms.maps.SupportMapFragment;
import com.google.android.gms.maps.model.BitmapDescriptorFactory;
import com.google.android.gms.maps.model.CameraPosition;
import com.google.android.gms.maps.model.LatLng;
import com.google.android.gms.maps.model.Marker;
import com.google.android.gms.maps.model.MarkerOptions;
import com.google.android.gms.maps.model.Polyline;
import com.google.android.gms.maps.model.PolylineOptions;

import java.util.ArrayList;

public class MapsActivity extends FragmentActivity implements OnMapReadyCallback, SensorEventListener {
    static private final String TAG = MapsActivity.class.getSimpleName();
    private BroadcastReceiver mBroadcastReceiver;

    static public final String northUp = "North up";
    static public final String free = "Free";
    static public final String currentLocation = "Current DBLocation";

    private GoogleMap mMap;
    private boolean mKeepCentered = true;

    private boolean mCompassVisible = true;

    private boolean mLocationServiceRunning;

    ArrayList<LatLng> mLatLongList = new ArrayList<>();
    ArrayList<LatLng> mCheckPointList = new ArrayList<>();

    private long mTotalTimer;
    private long mWayPointTimer;
    private long mCheckPointTimer;

    private boolean mNotExiting = false;

    private LatLng mWayPointLastLatLng;

    private int mTotalDistance;
    private float mSpeed;

    private float mBearing;
    private String mButtonDirectionText = northUp;

    private Location mPreviousLocation = new Location("");
    private Location mCurrentLocation = new Location("");
    private Polyline mPolyLine;
    private PolylineOptions mPolylineOptions;
    private Marker mPolylineEndMarker;
    private LatLng mLatLng;

    private int mWayPointTotalDistance = 0;
    private int mWayPointDirectDistance = 0;
    private Marker mWayPointMarker;
    private Location mWayPointLocation = new Location("");

    private int mCheckPointTotalDistance = 0;
    private int mCheckPointDirectDistance = 0;
    private Location mCheckPointLocation = new Location("");

    private boolean mCheckBearingAgain = false;

    private Button mButtonCentered;

    private ImageView mCompassImg;
    private SensorManager mSensorManager;
    private Sensor mSensor;
    private float currentDegree = 0.5f;


    // ================ Total ================
    private Button mButtonStartOrStopService;
    private TextView mTextViewTotalDistance;
    private TextView mTextViewTotalTime;
    private TextView mTextViewPace;


    // ============== CheckPoint ============== Multiple on map
    private Button mButtonCheckPointAdd;
    private TextView mTextViewTotalDistanceCheckPoint;
    private TextView mTextViewDirectDistanceCheckPoint;
    private TextView mTextViewCheckPointTime;
    // ========================================


    // =============== WayPoint =============== Single on map
    private Button mButtonWayPointAdd;
    private TextView mTextViewTotalDistanceWayPoint;
    private TextView mTextViewDirectDistanceWayPoint;
    private TextView mTextViewWayPointTime;
    // ========================================

    private Button mButtonDirection;
    private Button mButtonReset;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        Log.d(TAG, "onCreate");
        super.onCreate(savedInstanceState);
        setContentView(R.layout.main_layout);
        // Obtain the SupportMapFragment and get notified when the map is ready to be used.
        SupportMapFragment mapFragment = (SupportMapFragment) getSupportFragmentManager()
                .findFragmentById(R.id.map);
        mapFragment.getMapAsync(this);

        // ================ Total ================
        mButtonStartOrStopService = (Button) findViewById(R.id.buttonStartOrStopService);
        mTextViewTotalDistance = (TextView) findViewById(R.id.textViewTotalDistance);
        mTextViewTotalTime = (TextView) findViewById(R.id.textViewTotalTime);
        mTextViewPace = (TextView) findViewById(R.id.textViewPace);
        mButtonStartOrStopService.setText(C.START);
        // ========================================

        // ============== CheckPoint ==============
        mButtonCheckPointAdd = (Button) findViewById(R.id.buttonCheckPointAdd);
        mTextViewTotalDistanceCheckPoint = (TextView) findViewById(R.id.textViewTotalDistanceCheckPoint);
        mTextViewDirectDistanceCheckPoint = (TextView) findViewById(R.id.textViewDirectDistanceCheckPoint);
        mTextViewCheckPointTime = (TextView) findViewById(R.id.textViewCheckPointTime);
        // ========================================

        // =============== WayPoint ===============
        mButtonWayPointAdd = (Button) findViewById(R.id.buttonWayPointAdd);
        mTextViewTotalDistanceWayPoint = (TextView) findViewById(R.id.textViewTotalDistanceWayPoint);
        mTextViewDirectDistanceWayPoint = (TextView) findViewById(R.id.textViewDirectDistanceWayPoint);
        mTextViewWayPointTime = (TextView) findViewById(R.id.textViewWayPointTime);
        // ========================================

        mButtonDirection = (Button) findViewById(R.id.buttonDirection);
        mButtonCentered = (Button) findViewById(R.id.buttonCentered);
        mButtonReset = (Button) findViewById(R.id.buttonReset);

        mButtonDirection.setText(northUp);

        mButtonCheckPointAdd.setEnabled(false);
        mButtonWayPointAdd.setEnabled(false);

        mCompassImg = (ImageView) findViewById(R.id.imageViewCompass);
        mCompassImg.setVisibility(View.VISIBLE);
        mSensorManager = (SensorManager) getSystemService(Context.SENSOR_SERVICE);
        mSensor = mSensorManager.getDefaultSensor(Sensor.TYPE_ORIENTATION);

        mPolylineOptions = new PolylineOptions()
                .width(7)
                .color(Color.BLACK);

        if (savedInstanceState == null){
            registerReceiver();
        }
    }


    /**
     * Manipulates the map once available.
     * This callback is triggered when the map is ready to be used.
     * This is where we can add markers or lines, add listeners or move the camera. In this case,
     * we just add a marker near Sydney, Australia.
     * If Google Play services is not installed on the device, the user will be prompted to install
     * it inside the SupportMapFragment. This method will only be triggered once the user has
     * installed Google Play services and returned to the app.
     */
    @Override
    public void onMapReady(GoogleMap googleMap) {
        Log.d(TAG, "onMapReady");
        mMap = googleMap;

        if (mWayPointLastLatLng != null && mWayPointLastLatLng.latitude != 0 && mWayPointLastLatLng.longitude != 0) {
            mWayPointMarker = mMap.addMarker(new MarkerOptions()
                    .position(mWayPointLastLatLng).icon(BitmapDescriptorFactory.fromResource(R.drawable.baseline_flag_black_18dp)));
        }

        for (int i = 0; i < mLatLongList.size(); i++) {
            mMap.addPolyline(mPolylineOptions.add(mLatLongList.get(i)));
        }

        System.out.println(mCheckPointList.size());
        for (int i = 0; i < mCheckPointList.size(); i++) {
            mMap.addMarker(new MarkerOptions()
                    .position(mCheckPointList.get(i)).icon(BitmapDescriptorFactory.fromResource(R.drawable.baseline_beenhere_black_18dp)));
        }

        if (mPreviousLocation != null) {
            mLatLng = new LatLng(mPreviousLocation.getLatitude(), mPreviousLocation.getLongitude());
        }
        // Add a marker in Sydney and move the camera
        //mMap.addMarker(new MarkerOptions().position(latLng).title("some title"));
        mMap.moveCamera(CameraUpdateFactory.zoomTo(17));
        mMap.getUiSettings().setCompassEnabled(false);
    }

    private void registerReceiver() {
        IntentFilter intentFilter = new IntentFilter();
        intentFilter.addAction(C.LOCATION_UPDATE);
        intentFilter.addAction(C.PREVIOUS_LOCATION);
        intentFilter.addAction(C.TIMERS);
        intentFilter.addAction(C.TOTAL_DISTANCE);
        intentFilter.addAction(C.SPEED);
        intentFilter.addAction(C.WAYPOINT_REACHED);
        intentFilter.addAction(C.WAYPOINT_TOTAL_DISTANCE);
        intentFilter.addAction(C.WAYPOINT_DIRECT_DISTANCE);
        intentFilter.addAction(C.CHECKPOINT_REACHED);
        intentFilter.addAction(C.CHECKPOINT_TOTAL_DISTANCE);
        intentFilter.addAction(C.CHECKPOINT_DIRECT_DISTANCE);
        mBroadcastReceiver = new MapsActivityBroadcastReceiver();
        LocalBroadcastManager.getInstance(getApplicationContext()).registerReceiver(mBroadcastReceiver, intentFilter);

    }


    // ============================== LIFECYCLE EVENTS ===============================
    @Override
    protected void onStart() {
        Log.d(TAG, "onStart");
        super.onStart();
    }

    @Override
    protected void onResume() {
        Log.d(TAG, "onResume");
        super.onResume();
        if (mSensor != null) {
            mSensorManager.registerListener(this, mSensor, SensorManager.SENSOR_DELAY_FASTEST);
        }
        if (mLocationServiceRunning) {
            startLocationService();
        }
        if (mBroadcastReceiver == null) {
            registerReceiver();
        }
        mNotExiting = false;
    }

    @Override
    protected void onPause() {
        Log.d(TAG, "onPause");
        super.onPause();
    }


    @Override
    protected void onStop() {
        Log.d(TAG, "onStop");
        super.onStop();
        mSensorManager.unregisterListener(this, mSensor);
    }

    @Override
    protected void onDestroy() {
        Log.d(TAG, "onDestroy");
        super.onDestroy();
        LocalBroadcastManager.getInstance(this).unregisterReceiver(mBroadcastReceiver);
        Intent removeNotificationIntent = new Intent(C.REMOVE_NOTIFICATION);
        LocalBroadcastManager.getInstance(getApplicationContext()).sendBroadcast(removeNotificationIntent);
    }


    @Override
    protected void onRestart() {
        Log.d(TAG, "onRestart");
        super.onRestart();
    }

    @Override
    protected void onSaveInstanceState(Bundle outState) {
        Log.d(TAG, "onSaveInstanceState");
        super.onSaveInstanceState(outState);
        outState.putBoolean(C.LOCATION_SERVICE_RUNNING, mLocationServiceRunning);
        outState.putParcelableArrayList(C.LAT_LONG_LIST, mLatLongList);
        outState.putParcelableArrayList(C.CHECKPOINT_LAT_LONG_LIST, mCheckPointList);
        outState.putBoolean(C.SAVE_CENTERED, mKeepCentered);
        outState.putBoolean(C.SAVE_COMPASS, mCompassVisible);
        outState.putString(C.SAVE_NORTH_UP, mButtonDirectionText);
        outState.putInt(C.SAVE_TOTAL_DISTANCE, mTotalDistance);
        outState.putInt(C.SAVE_TOTAL_DISTANCE_CP, mCheckPointTotalDistance);
        outState.putInt(C.SAVE_TOTAL_DISTANCE_WP, mWayPointTotalDistance);
        outState.putFloat(C.SAVE_PACE, mSpeed);
        outState.putInt(C.SAVE_DIRECT_DISTANCE_CP, mCheckPointDirectDistance);
        outState.putInt(C.SAVE_DIRECT_DISTANCE_WP, mWayPointDirectDistance);
        outState.putLong(C.SAVE_TOTAL_TIME, mTotalTimer);
        outState.putLong(C.SAVE_TOTAL_TIME_CP, mCheckPointTimer);
        outState.putLong(C.SAVE_TOTAL_TIME_WP, mWayPointTimer);
        outState.putBoolean(C.SAVE_SETTING_POINT_BOOL, mNotExiting);
        if (mWayPointLastLatLng != null) {
            outState.putDouble(C.SAVE_WAYPOINT_LAST_LAT, mWayPointLastLatLng.latitude);
            outState.putDouble(C.SAVE_WAYPOINT_LAST_LNG, mWayPointLastLatLng.longitude);
        }
    }

    @Override
    protected void onRestoreInstanceState(Bundle savedInstanceState) {
        Log.d(TAG, "onRestoreInstanceState");
        super.onRestoreInstanceState(savedInstanceState);
        mLatLongList = savedInstanceState.getParcelableArrayList(C.LAT_LONG_LIST);
        mCheckPointList = savedInstanceState.getParcelableArrayList(C.CHECKPOINT_LAT_LONG_LIST);
        mLocationServiceRunning = savedInstanceState.getBoolean(C.LOCATION_SERVICE_RUNNING);
        mKeepCentered = savedInstanceState.getBoolean(C.SAVE_CENTERED);
        mCompassVisible = savedInstanceState.getBoolean(C.SAVE_COMPASS);
        mNotExiting = savedInstanceState.getBoolean(C.SAVE_SETTING_POINT_BOOL);
        setCompass();
        setCompass();
        mButtonDirectionText = savedInstanceState.getString(C.SAVE_NORTH_UP);

        if (mButtonDirectionText.equals(northUp)) {
            mButtonDirection.setText(northUp);
            mCheckBearingAgain = false;
            mBearing = 0;
        } else if (mButtonDirectionText.equals(currentLocation)) {
            mButtonDirection.setText(currentLocation);
            mBearing = mPreviousLocation.bearingTo(mCurrentLocation);
            mCheckBearingAgain = true;
        } else {
            mButtonDirection.setText(free);
            mCheckBearingAgain = false;
            mBearing = 90;
        }

        mTotalDistance = savedInstanceState.getInt(C.SAVE_TOTAL_DISTANCE);
        mCheckPointTotalDistance = savedInstanceState.getInt(C.SAVE_TOTAL_DISTANCE_CP);
        mWayPointTotalDistance = savedInstanceState.getInt(C.SAVE_TOTAL_DISTANCE_WP);
        mSpeed = savedInstanceState.getFloat(C.SAVE_PACE);
        mCheckPointDirectDistance = savedInstanceState.getInt(C.SAVE_DIRECT_DISTANCE_CP);
        mWayPointDirectDistance = savedInstanceState.getInt(C.SAVE_DIRECT_DISTANCE_WP);
        mTotalTimer = savedInstanceState.getLong(C.SAVE_TOTAL_TIME);
        mCheckPointTimer = savedInstanceState.getLong(C.SAVE_TOTAL_TIME_CP);
        mWayPointTimer = savedInstanceState.getLong(C.SAVE_TOTAL_TIME_WP);
        mWayPointLastLatLng = new LatLng(savedInstanceState.getDouble(C.SAVE_WAYPOINT_LAST_LAT), savedInstanceState.getDouble(C.SAVE_WAYPOINT_LAST_LNG));
        updateUI();
    }
    // ============================================================================

    public void updateMap() {
        Log.d(TAG, "updateMap");
        if (mPolylineEndMarker != null) {
            mPolylineEndMarker.remove();
        }
        if (mCheckBearingAgain) {
            mBearing = mPreviousLocation.bearingTo(mCurrentLocation);
        }
        if (mMap != null) {
            mLatLng = new LatLng(mCurrentLocation.getLatitude(), mCurrentLocation.getLongitude());
            mLatLongList.add(mLatLng);
            mPolyLine = mMap.addPolyline(mPolylineOptions.add(mLatLng));
            mPolylineEndMarker = mMap.addMarker(new MarkerOptions()
                    .position(mLatLng).icon(BitmapDescriptorFactory.fromResource(R.drawable.baseline_directions_run_black_18dp)));
            if (mKeepCentered) {
                if (mButtonDirectionText.equals(free)) {
                    mMap.moveCamera(CameraUpdateFactory.newCameraPosition(new CameraPosition(mLatLng, mMap.getCameraPosition().zoom, mMap.getCameraPosition().tilt, mMap.getCameraPosition().bearing)));
                } else {
                    mMap.moveCamera(CameraUpdateFactory.newCameraPosition(new CameraPosition(mLatLng, mMap.getCameraPosition().zoom, mMap.getCameraPosition().tilt, mBearing)));
                }
            } // mKeepCentered
        } // mMap != null
    }


    public void startOrStopServiceButtonOnClick(View view) {
        Log.d(TAG, "startOrStopServiceButtonOnClick");

        if (mButtonStartOrStopService.getText().equals(C.START)) {
            startLocationService();
        } else {
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.setTitle("Stop Session");
            alert.setMessage("Are you sure you want to Stop current session?");
            alert.setPositiveButton("Yes", new DialogInterface.OnClickListener() {
                @Override
                public void onClick(DialogInterface dialog, int which) {
                    stopLocationService();
                    dialog.dismiss();
                }
            });

            alert.setNegativeButton("No", new DialogInterface.OnClickListener() {
                @Override
                public void onClick(DialogInterface dialog, int which) {
                    dialog.dismiss();
                }
            });
            alert.show();
        }
    }

    private void startLocationService() {
        Intent startLocationService = new Intent(this, LocationService.class);
        this.startService(startLocationService);
        mButtonCheckPointAdd.setEnabled(true);
        mButtonWayPointAdd.setEnabled(true);
        mButtonStartOrStopService.setText(C.STOP);
        mLocationServiceRunning = true;
        mButtonReset.setEnabled(false);
    }

    private void stopLocationService() {
        Intent stopLocationService = new Intent(this, LocationService.class);
        this.stopService(stopLocationService);
        mButtonCheckPointAdd.setEnabled(false);
        mButtonWayPointAdd.setEnabled(false);
        mButtonStartOrStopService.setText(C.START);
        mLocationServiceRunning = false;
        mButtonReset.setEnabled(true);
    }

    public void checkPointAddButtonOnClick(View view) {
        Log.d(TAG, "checkPointAddButtonOnClick");
        addCheckPoint();
    }

    public void addCheckPoint() {
        mNotExiting = true;
        Intent startCheckPointActivity = new Intent(this, PointSettingActivity.class);
        startCheckPointActivity.putExtra(C.WHICH_POINT_TO_SET, C.CHECKPOINT);
        this.startActivity(startCheckPointActivity);
    }

    public void wayPointAddButtonOnClick(View view) {
        Log.d(TAG, "wayPointAddButtonOnClick");
        addWayPoint();
    }

    public void addWayPoint() {
        mNotExiting = true;
        Intent startWayPointActivity = new Intent(this, PointSettingActivity.class);
        startWayPointActivity.putExtra(C.WHICH_POINT_TO_SET, C.WAYPOINT);
        this.startActivity(startWayPointActivity);
    }

    public void updateUI() {
        mTextViewWayPointTime.setText(secondsToHMS(mWayPointTimer));
        mTextViewTotalTime.setText(secondsToHMS(mTotalTimer));
        mTextViewCheckPointTime.setText(secondsToHMS(mCheckPointTimer));

        mTextViewTotalDistance.setText(String.valueOf(mTotalDistance) + " m");

        mTextViewDirectDistanceWayPoint.setText(String.valueOf(mWayPointDirectDistance) + " m");
        mTextViewTotalDistanceWayPoint.setText(String.valueOf(mWayPointTotalDistance) + " m");

        mTextViewDirectDistanceCheckPoint.setText(String.valueOf(mCheckPointDirectDistance) + " m");
        mTextViewTotalDistanceCheckPoint.setText(String.valueOf(mCheckPointTotalDistance) + " m");

        if (mSpeed >= Float.MAX_VALUE) {
            mSpeed = 0;
        }
        mTextViewPace.setText(String.format("%.2f", mSpeed) + " km/min");
        mButtonCentered.setText(mKeepCentered ? "Centered" : "Not centered");
    }

    public void updateWayPoint() {
        if (mWayPointMarker != null) {
            mWayPointMarker.remove();
        }
        LatLng latLngWayPoint = new LatLng(mWayPointLocation.getLatitude(), mWayPointLocation.getLongitude());
        mWayPointLastLatLng = latLngWayPoint;
        mWayPointMarker = mMap.addMarker(new MarkerOptions()
                .position(latLngWayPoint).icon(BitmapDescriptorFactory.fromResource(R.drawable.baseline_flag_black_18dp)));
    }

    public void updateCheckPoint() {
        Log.d(TAG, "updateCheckPoint");
        LatLng latLngCheckPoint = new LatLng(mCheckPointLocation.getLatitude(), mCheckPointLocation.getLongitude());
        mCheckPointList.add(latLngCheckPoint);
        System.out.println(mCheckPointList.size());
        mMap.addMarker(new MarkerOptions()
                .position(latLngCheckPoint).icon(BitmapDescriptorFactory.fromResource(R.drawable.baseline_beenhere_black_18dp)));
    }

    public void buttonCenteredOnClick(View view) {
        mKeepCentered = !mKeepCentered;
        mButtonCentered.setText(mKeepCentered ? "Centered" : "Not centered");
    }

    public void buttonDirectionOnClick(View view) {
        buttonDirectionSetting();
    }

    private void buttonDirectionSetting() {
        mButtonDirectionText = mButtonDirection.getText().toString();

        if (mButtonDirectionText.equals(northUp)) {
            mButtonDirection.setText(currentLocation);
            mBearing = mPreviousLocation.bearingTo(mCurrentLocation);
            mCheckBearingAgain = true;

        } else if (mButtonDirectionText.equals(currentLocation)) {
            mButtonDirection.setText(free);
            mCheckBearingAgain = false;
            mBearing = 90;

        } else {
            mButtonDirection.setText(northUp);
            mCheckBearingAgain = false;
            mBearing = 0;
        }
        mButtonDirectionText = mButtonDirection.getText().toString();
    }

    @Override
    public void onSensorChanged(SensorEvent event) {
        if (mCompassImg.getVisibility() == ImageView.VISIBLE) {
            int degree = Math.round(event.values[0]);

            RotateAnimation mRotateAnimation = new RotateAnimation(currentDegree,
                    -degree, Animation.RELATIVE_TO_SELF, 0.5f,
                    Animation.RELATIVE_TO_SELF, 0.5f);
            mRotateAnimation.setDuration(1000);
            mRotateAnimation.setFillAfter(true);

            mCompassImg.startAnimation(mRotateAnimation);
            currentDegree = -degree;
        }
    }

    @Override
    public void onAccuracyChanged(Sensor sensor, int accuracy) {

    }

    public void buttonCompassOnClick(View view) {
        mCompassImg.clearAnimation();

        if (mCompassImg.getVisibility() == ImageView.VISIBLE) {
            setCompass();
        } else {
            setCompass();
        }
    }

    private void setCompass() {
        mCompassImg.clearAnimation();

        if (mCompassVisible) {
            mCompassImg.setVisibility(ImageView.GONE);
            mSensorManager.unregisterListener(this, mSensor);
            mCompassVisible = false;
        } else {
            mCompassImg.setVisibility(ImageView.VISIBLE);
            mSensorManager.registerListener(this, mSensor, SensorManager.SENSOR_DELAY_FASTEST);
            mCompassVisible = true;
        }
    }

    public void buttonSessionsOnClick(View view) {
        Intent startSessionsActivity = new Intent(this, RecyclerViewGPSActivity.class);
        this.startActivity(startSessionsActivity);
        mNotExiting = true;
    }

    public void buttonResetOnClick(View view) {
        resetStuff();
    }

    public void resetStuff(){
        mMap.clear();
        mTextViewTotalDistance.setText(R.string.placeholder);
        mTextViewTotalTime.setText(R.string.placeholder);
        mTextViewPace.setText(R.string.placeholder);

        mTextViewTotalDistanceCheckPoint.setText(R.string.placeholder);
        mTextViewDirectDistanceCheckPoint.setText(R.string.placeholder);
        mTextViewCheckPointTime.setText(R.string.placeholder);

        mTextViewTotalDistanceWayPoint.setText(R.string.placeholder);
        mTextViewDirectDistanceWayPoint.setText(R.string.placeholder);
        mTextViewWayPointTime.setText(R.string.placeholder);
        mLatLongList.clear();
        mCheckPointList.clear();
        mPolylineOptions = new PolylineOptions()
                .width(7)
                .color(Color.BLACK);
        if (mPolyLine != null){
            mPolyLine.remove();
        }
    }

    public class MapsActivityBroadcastReceiver extends BroadcastReceiver {

        @Override
        public void onReceive(Context context, Intent intent) {
            Log.d(TAG, "onReceive " + intent.getAction());
            switch (intent.getAction()) {
                case C.LOCATION_UPDATE:
                    mCurrentLocation.setLatitude(intent.getExtras().getDouble(C.LOCATION_UPDATE_LAT));
                    mCurrentLocation.setLongitude(intent.getExtras().getDouble(C.LOCATION_UPDATE_LONG));
                    updateMap();
                    break;
                case C.PREVIOUS_LOCATION:
                    mPreviousLocation.setLatitude(intent.getExtras().getDouble(C.PREVIOUS_LOCATION_LAT));
                    mPreviousLocation.setLongitude(intent.getExtras().getDouble(C.PREVIOUS_LOCATION_LONG));
                    break;
                case C.TIMERS:
                    mTotalTimer = intent.getExtras().getLong(C.TOTAL_TIMER);
                    mCheckPointTimer = intent.getExtras().getLong(C.CHECKPOINT_TIMER);
                    mWayPointTimer = intent.getExtras().getLong(C.WAYPOINT_TIMER);
                    break;
                case C.TOTAL_DISTANCE:
                    mTotalDistance = intent.getExtras().getInt(C.TOTAL_DISTANCE_DISTANCE);
                    break;
                case C.SPEED:
                    mSpeed = intent.getExtras().getFloat(C.SPEED_SPEED);
                    break;
                case C.WAYPOINT_REACHED:
                    mWayPointLocation.setLatitude(intent.getExtras().getDouble(C.WAYPOINT_REACHED_LAT));
                    mWayPointLocation.setLongitude(intent.getExtras().getDouble(C.WAYPOINT_REACHED_LONG));
                    updateWayPoint();
                    break;
                case C.WAYPOINT_DIRECT_DISTANCE:
                    mWayPointDirectDistance = intent.getExtras().getInt(C.WAYPOINT_DIRECT_DISTANCE_DISTANCE);
                    break;
                case C.WAYPOINT_TOTAL_DISTANCE:
                    mWayPointTotalDistance = intent.getExtras().getInt(C.WAYPOINT_TOTAL_DISTANCE_DISTANCE);
                    break;
                case C.CHECKPOINT_REACHED:
                    mCheckPointLocation.setLatitude(intent.getExtras().getDouble(C.CHECKPOINT_REACHED_LAT));
                    mCheckPointLocation.setLongitude(intent.getExtras().getDouble(C.CHECKPOINT_REACHED_LONG));
                    updateCheckPoint();
                    break;
                case C.CHECKPOINT_DIRECT_DISTANCE:
                    mCheckPointDirectDistance = intent.getExtras().getInt(C.CHECKPOINT_DIRECT_DISTANCE_DISTANCE);
                    break;
                case C.CHECKPOINT_TOTAL_DISTANCE:
                    mCheckPointTotalDistance = intent.getExtras().getInt(C.CHECKPOINT_TOTAL_DISTANCE_DISTANCE);
                    break;
            }
            updateUI();
        }
    }

    public static String secondsToHMS(long second) {

        int hours = (int) second / 3600;
        int remainder = (int) second - hours * 3600;
        int minutes = remainder / 60;
        remainder = remainder - minutes * 60;
        int seconds = remainder;
        return (hours + ":" + minutes + ":" + seconds);
    }
}
