package com.example.sportmapapp;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.IntentFilter;
import android.content.pm.PackageManager;
import android.graphics.Color;
import android.location.Location;
import android.net.Uri;
import android.os.Build;
import android.os.Bundle;
import android.os.Environment;
import android.support.v4.content.LocalBroadcastManager;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.DividerItemDecoration;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.util.Log;
import android.view.View;
import android.widget.ImageView;
import android.widget.Toast;

import com.example.sportmapapp.Domain.DBLocation;
import com.example.sportmapapp.Domain.Session;
import com.example.sportmapapp.Repositories.LocationRepository;
import com.example.sportmapapp.Repositories.SessionRepository;
import com.google.android.gms.maps.CameraUpdateFactory;
import com.google.android.gms.maps.GoogleMap;
import com.google.android.gms.maps.OnMapReadyCallback;
import com.google.android.gms.maps.SupportMapFragment;
import com.google.android.gms.maps.model.BitmapDescriptorFactory;
import com.google.android.gms.maps.model.LatLng;
import com.google.android.gms.maps.model.MarkerOptions;
import com.google.android.gms.maps.model.PolylineOptions;

import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.OutputStreamWriter;
import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.List;

public class RecyclerViewGPSActivity extends AppCompatActivity implements OnMapReadyCallback, RecyclerViewGPSAdapter.ItemClickListener {
    static private final String TAG = RecyclerViewGPSActivity.class.getSimpleName();
    private BroadcastReceiver mBroadcastReceiver;

    private RecyclerViewGPSAdapter mAdapter;
    private GoogleMap mMap;

    private int mChosenSessionId = -1;
    private RecyclerView mStatisticsRecycleView;
    private int mCurrentlyRunningSessionId;

    private LocationRepository mLocationRepository;
    private SessionRepository mSessionRepository;
    private List<Session> sessions;

    @Override
    public void onCreate(Bundle savedInstanceState) {
        Log.d(TAG, "onCreate");
        super.onCreate(savedInstanceState);
        setContentView(R.layout.gps_statistics_main);

        SupportMapFragment mapFragment = (SupportMapFragment) getSupportFragmentManager()
                .findFragmentById(R.id.map);
        mapFragment.getMapAsync(this);

        ImageView mCompassImg = (ImageView) findViewById(R.id.imageViewCompass);
        mCompassImg.setVisibility(ImageView.GONE);

        mLocationRepository = new LocationRepository(this);
        mSessionRepository = new SessionRepository(this);
        mLocationRepository.open();
        mSessionRepository.open();

        // set up recyclerView
        mStatisticsRecycleView =  findViewById(R.id.recyclerViewStatistics);
        mStatisticsRecycleView.setLayoutManager(new LinearLayoutManager(this));
        sessions = mSessionRepository.getSessions();
        mAdapter = new RecyclerViewGPSAdapter(this, sessions);
        mAdapter.setItemClickListener(this);
        mStatisticsRecycleView.setAdapter(mAdapter);

        DividerItemDecoration dividerItemDecoration = new DividerItemDecoration(mStatisticsRecycleView.getContext(), LinearLayoutManager.VERTICAL);
        dividerItemDecoration.setDrawable(mStatisticsRecycleView.getContext().getResources().getDrawable(R.drawable.recyclerview_divider));
        mStatisticsRecycleView.addItemDecoration(dividerItemDecoration);

        IntentFilter intentFilter = new IntentFilter();
        intentFilter.addAction(C.CURRENT_SESSION_ID_RECEIVE);

        mBroadcastReceiver = new RecyclerViewGPSActivityBroadCastReceiver();
        LocalBroadcastManager.getInstance(getApplicationContext()).registerReceiver(mBroadcastReceiver, intentFilter);

    }

    @Override
    public void onMapReady(GoogleMap googleMap) {
        mMap = googleMap;
        if (mChosenSessionId != -1){
            displayTrackOnMap();
        }
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
        mLocationRepository.close();
        mSessionRepository.close();
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
        outState.putInt(C.CHOSEN_SESSION_ID, mChosenSessionId);
    }

    @Override
    protected void onRestoreInstanceState(Bundle savedInstanceState) {
        Log.d(TAG, "onRestoreInstanceState");
        super.onRestoreInstanceState(savedInstanceState);
        mChosenSessionId = savedInstanceState.getInt(C.CHOSEN_SESSION_ID);
    }

    @Override
    public void onStatisticsRecyclerViewRowClick(View view, int position) {
        Log.d(TAG, "onStatisticsRecyclerViewRowClick: " + position);
        mChosenSessionId = sessions.get(position).id;
        displayTrackOnMap();
    }

    @Override
    public void onButtonExportInRecyclerViewClick(View view, int position) {
        Log.d(TAG, "onButtonExportInRecyclerViewClick: " + position);
        createGpx(position);
    }

    @Override
    public void onButtonDeleteInRecyclerViewClick(View view, final int position) {
        Log.d(TAG, "onButtonDeleteInRecyclerViewClick: " + position);

        AlertDialog.Builder alert = new AlertDialog.Builder(this);
        alert.setTitle("Delete");
        alert.setMessage("Are you sure you want to delete?");
        alert.setPositiveButton("Yes", new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {
                deleteSession(position);
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

    public void createGpx(int position){
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.N) {
            if (checkSelfPermission(android.Manifest.permission.WRITE_EXTERNAL_STORAGE) == PackageManager.PERMISSION_GRANTED){
                Log.v(TAG,"Permission is granted");
                writeGpx(position);
            } else {
                Toast.makeText(this, "Permission to write denied.", Toast.LENGTH_LONG).show();
            }
        } else {
            writeGpx(position);
        }
    }

    public void writeGpx(int position){
        DateFormat dateFormat = new SimpleDateFormat("yyyy_MM_dd_HH_mm_ss");
        Date date = new Date();
        String fileName = "SportMapApp_" + dateFormat.format(date) + ".gpx";
        List<DBLocation> locations = mLocationRepository.getBySessionId(sessions.get(position).id);
        System.out.println(fileName);

        File directory = Environment.getExternalStoragePublicDirectory(Environment.DIRECTORY_DOWNLOADS);
        if (!directory.exists()){
            directory.mkdir();
        }
        File file = new File(directory, fileName);

        String header = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>" +
                "<gpx xmlns=\"http://www.topografix.com/GPX/1/1\" creator=\"SportMapApp\" version=\"1.1\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" " +
                " xsi:schemaLocation=\"http://www.topografix.com/GPX/1/1 http://www.topografix.com/GPX/1/1/gpx.xsd\">" +
                "<trk>\n<trkseg>\n";

        StringBuilder segments = new StringBuilder();
        for (DBLocation location : locations) {
            segments.append("<trkpt lat=\"").append(location.latitude).append("\" lon=\"").append(location.longitude).append("\"></trkpt>\n");
            if (location.checkPointLatitude != null){
                segments.append("<wpt lat=\"").append(location.checkPointLatitude).append("\" lon=\"").append(location.checkPointLongitude).append("\"></wpt>\n");
            }
        }
        String footer = "</trkseg></trk></gpx>";

        try {
            FileOutputStream fileOutput = new FileOutputStream(file);
            OutputStreamWriter outputStreamWriter = new OutputStreamWriter(fileOutput);

            outputStreamWriter.write(header);
            outputStreamWriter.write(segments.toString());
            outputStreamWriter.write(footer);
            outputStreamWriter.flush();
            outputStreamWriter.close();

        } catch (IOException e) {
            Log.e("createGpx", "Error writing file", e);
            Toast.makeText(this, "Could not write file.", Toast.LENGTH_LONG).show();
        }
        Toast.makeText(this, "File " + fileName + " has been exported to: " + directory, Toast.LENGTH_LONG).show();

        Intent intent = new Intent(Intent.ACTION_MEDIA_SCANNER_SCAN_FILE);
        intent.setData(Uri.fromFile(file));
        sendBroadcast(intent);
    }

    public void deleteSession(int position){
        int sessionId = sessions.get(position).id;

        if (sessionId == mCurrentlyRunningSessionId){
            Toast.makeText(this, "Cannot remove currently running session. ", Toast.LENGTH_SHORT).show();
            return;
        }
        mLocationRepository.deleteById(sessionId);
        mSessionRepository.deleteById(sessionId);

        sessions = mSessionRepository.getSessions();

        mAdapter.updateData(sessions);
    }

    public void displayTrackOnMap(){
        mMap.clear();
        List<DBLocation> locations = mLocationRepository.getBySessionId(mChosenSessionId);

        PolylineOptions polylineOptions = new PolylineOptions().width(7).color(Color.BLACK);

        LatLng latLng = new LatLng(0, 0);

        for (DBLocation location : locations){
            if (location.checkPointLatitude != null){
                mMap.addMarker(new MarkerOptions()
                        .position(new LatLng(Double.parseDouble(location.checkPointLatitude), Double.parseDouble(location.checkPointLongitude)))
                        .icon(BitmapDescriptorFactory.fromResource(R.drawable.baseline_beenhere_black_18dp)));
            }
            latLng = new LatLng(Double.parseDouble(location.latitude), Double.parseDouble(location.longitude));
            mMap.addPolyline(polylineOptions.add(latLng));
        }

        mMap.moveCamera(CameraUpdateFactory.newLatLngZoom(latLng, 17));
    }

    public class RecyclerViewGPSActivityBroadCastReceiver extends BroadcastReceiver {

        @Override
        public void onReceive(Context context, Intent intent) {
            Log.d(TAG, "onReceive " + intent.getExtras());
            switch (intent.getAction()) {
                case C.CURRENT_SESSION_ID_RECEIVE:
                    mCurrentlyRunningSessionId = intent.getExtras().getInt(C.CURRENT_SESSION_ID_ID);
                    break;
            }
        }
    }
}
