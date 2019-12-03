package com.example.sportmapapp;

import android.content.Intent;
import android.os.Bundle;
import android.support.v4.content.LocalBroadcastManager;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.View;
import android.widget.EditText;

public class PointSettingActivity extends AppCompatActivity {
    static private final String TAG = PointSettingActivity.class.getSimpleName();

    private EditText mEditTextDistance;
    private String mSettingForPoint;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        Log.d(TAG, "onCreate");
        super.onCreate(savedInstanceState);
        setContentView(R.layout.point_ask_main);

        mSettingForPoint = getIntent().getExtras().getString(C.WHICH_POINT_TO_SET);

        mEditTextDistance = (EditText) findViewById(R.id.editTextDistance);

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
    }

    @Override
    protected void onDestroy() {
        Log.d(TAG, "onDestroy");
        super.onDestroy();
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
    }

    @Override
    protected void onRestoreInstanceState(Bundle savedInstanceState) {
        Log.d(TAG, "onRestoreInstanceState");
        super.onRestoreInstanceState(savedInstanceState);
    }

    public void saveButtonOnClick(View view) {
        String data = mEditTextDistance.getText().toString().trim();
        int distance;
        try {
            distance = Integer.valueOf(data);

        } catch (Exception e){
            return;
        }
        if (distance > 0){
            if (mSettingForPoint.equals(C.CHECKPOINT)){
                Intent distanceIntent = new Intent(C.CHECKPOINT_ACTIVITY);
                distanceIntent.putExtra(C.CHECKPOINT_DISTANCE, distance);
                //sendBroadcast(distanceIntent);
                LocalBroadcastManager.getInstance(getApplicationContext()).sendBroadcast(distanceIntent);

            } else if (mSettingForPoint.equals(C.WAYPOINT)){
                Intent distanceIntent = new Intent(C.WAYPOINT_ACTIVITY);
                distanceIntent.putExtra(C.WAYPOINT_DISTANCE, distance);
                //sendBroadcast(distanceIntent);
                LocalBroadcastManager.getInstance(getApplicationContext()).sendBroadcast(distanceIntent);
            }
            this.finish();
        }
    }

    public void cancelButtonOnClick(View view) {
        this.finish();
    }
}
