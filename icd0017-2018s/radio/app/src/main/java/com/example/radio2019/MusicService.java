package com.example.radio2019;

import android.app.Service;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.media.AudioManager;
import android.media.MediaPlayer;
import android.os.AsyncTask;
import android.os.IBinder;
import android.support.v4.content.LocalBroadcastManager;
import android.util.Log;

import com.android.volley.Request;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.StringRequest;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.IOException;
import java.util.concurrent.Executors;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.TimeUnit;


public class MusicService extends Service implements MediaPlayer.OnCompletionListener, MediaPlayer.OnErrorListener, MediaPlayer.OnPreparedListener {
    static private final String TAG = MusicService.class.getSimpleName();

    // on android 9 add this to manifest
    // <application android:usesCleartextTraffic="true"



    // not the best option in real life, problems with buffers and formats and ...
    // use exoplayer or something else
    private MediaPlayer mMediaPlayer = new MediaPlayer();
    private String mMediaSource = "";
    private String mMediaSourceJSON = "";
    private ScheduledExecutorService mScheduledExecutorService;
    private BroadcastReceiver mBroadcastReceiver;

    @Override
    public void onCreate() {
        Log.d(TAG, "onCreate");
        super.onCreate();


        IntentFilter intentFilter = new IntentFilter();
        intentFilter.addAction(C.ACTIVITY_INTENT_STARTMUSIC);
        intentFilter.addAction(C.ACTIVITY_INTENT_STOPPMUSIC);
        intentFilter.addAction(C.MUSICPLAYER_VOLUME_KEY);
        intentFilter.addAction(C.STATION_CHANGE);

        mBroadcastReceiver = new MusicServiceBroadcastReceiver();
        LocalBroadcastManager.getInstance(getApplicationContext()).registerReceiver(mBroadcastReceiver, intentFilter);


        mMediaPlayer.setOnCompletionListener(this);
        mMediaPlayer.setOnErrorListener(this);
        mMediaPlayer.setOnPreparedListener(this);
        mMediaPlayer.reset();
        mMediaPlayer.setAudioStreamType(AudioManager.STREAM_MUSIC);

    }

    @Override
    public int onStartCommand(Intent intent, int flags, int startId) {
        Log.d(TAG, "onStartCommand");
        if (intent == null){
            Log.e(TAG, "Intent was null!!!");
            return Service.START_NOT_STICKY;
        }

        mMediaPlayer.reset();
        // do not use magic strings!!!!
        mMediaSource = intent.getExtras().getString(C.SERVICE_MEDIASOURCE_KEY);
        mMediaSourceJSON = intent.getExtras().getString(C.SERVICE_MEDIASROUCE_KEY_JSON);

        try {
            mMediaPlayer.setDataSource(mMediaSource);
            mMediaPlayer.prepareAsync();

            // Inform main activity that we are buffering
            Intent intentInformActivity = new Intent(C.MUSICSERVICE_INTENT_BUFFERING);
            LocalBroadcastManager.getInstance(getApplicationContext()).sendBroadcast(intentInformActivity);

        } catch (IOException e) {
            e.printStackTrace();
        }

        return Service.START_NOT_STICKY;
    }

    // ====================== Typically not used =====================
    @Override
    public IBinder onBind(Intent intent) {
        return null;
    }


    // ====================== MediaPLayer Lifecycle events =====================
    @Override
    public void onCompletion(MediaPlayer mp) {
        Log.d(TAG, "onCompletion");

    }

    @Override
    public boolean onError(MediaPlayer mp, int what, int extra) {
        Log.d(TAG, "onError");
        return false;
    }

    @Override
    public void onPrepared(MediaPlayer mp) {
        Log.d(TAG, "onPrepared");
        mMediaPlayer.start();

        // Inform main activity, that we are playing
        Intent intentInformActivity = new Intent(C.MUSICSERVICE_INTENT_PLAYING);
        LocalBroadcastManager.getInstance(getApplicationContext()).sendBroadcast(intentInformActivity);

        StartMediaInfoGathering();
    }

    private void StartMediaInfoGathering(){
        mScheduledExecutorService = Executors.newScheduledThreadPool(5);
        mScheduledExecutorService.scheduleAtFixedRate(
                new Runnable() {
                    @Override
                    public void run() {
                        new GetSongInfo().execute();
                    }
                }, 0, 15, TimeUnit.SECONDS
        );
    }

    private class GetSongInfo extends AsyncTask<Void, Void, Void>{

        @Override
        protected Void doInBackground(Void... voids) {
            final String url = mMediaSourceJSON;
            StringRequest stringRequest = new StringRequest(
                    Request.Method.GET,
                    url,
                    new Response.Listener<String>() {
                        @Override
                        public void onResponse(String response) {
                            Log.d(TAG, "onResponse " + response);

                            try {
                                JSONObject jsonObjectStationInfo = new JSONObject(response);
                                JSONArray jsonArraySongHistory = jsonObjectStationInfo.getJSONArray("SongHistoryList");
                                JSONObject jsonArraySongInfo = jsonArraySongHistory.getJSONObject(0);

                                String artist = jsonArraySongInfo.getString("Artist");
                                String trackTitle = jsonArraySongInfo.getString("Title");

                                Intent sendSongInfoIntent = new Intent(C.MUSICSERVICE_INTENT_SONGINFO);
                                sendSongInfoIntent.putExtra(C.MUSICSERVICE_ARTIST, artist);
                                sendSongInfoIntent.putExtra(C.MUSICSERVICE_TRACKTITLE, trackTitle);
                                LocalBroadcastManager.getInstance(getApplicationContext()).sendBroadcast(sendSongInfoIntent);

                            } catch (JSONException e) {
                                Log.d(TAG, e.getMessage());
                                e.printStackTrace();
                            }

                        }
                    },
                    new Response.ErrorListener() {
                        @Override
                        public void onErrorResponse(VolleyError error) {
                            Log.d(TAG, "onErrorResponse " + error.getMessage());
                        }
                    }
            );

            stringRequest.addMarker(C.MUSICSERVICE_VOLLEYTAG);
            // add the request to Volley queue
            WebApiSingletonServiceHandler.getInstance(getApplicationContext()).addToRequestQueue(stringRequest);
            return null;
        }
    }


    public class MusicServiceBroadcastReceiver extends BroadcastReceiver {
        @Override
        public void onReceive(Context context, Intent intent) {
            Log.d(TAG, "onReceive");
            switch (intent.getAction()){
                case C.ACTIVITY_INTENT_STOPPMUSIC:
                    LocalBroadcastManager.getInstance(getApplicationContext()).sendBroadcast((new Intent(C.MUSICSERVICE_INTENT_STOPPED)));
                    WebApiSingletonServiceHandler.getInstance(getApplicationContext()).cancelRequestQueue(C.MUSICSERVICE_VOLLEYTAG);
                    mScheduledExecutorService.shutdownNow();
                    mMediaPlayer.stop();
                    break;
                case C.STATION_CHANGE:
                    WebApiSingletonServiceHandler.getInstance(getApplicationContext()).cancelRequestQueue(C.MUSICSERVICE_VOLLEYTAG);
                    try { // if station is double clicked
                        mScheduledExecutorService.shutdownNow();
                        mMediaPlayer.stop();
                        break;
                    } catch (Exception e) {
                        break;
                    }
            }
        }

    }

}
