package com.example.radio2019;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.os.Bundle;

import android.support.v4.content.LocalBroadcastManager;
import android.support.v7.app.AppCompatActivity;

import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

import com.example.radio2019.Domain.Artist;
import com.example.radio2019.Domain.Station;
import com.example.radio2019.Repositories.ArtistRepository;
import com.example.radio2019.Repositories.SongRepository;
import com.example.radio2019.Repositories.StationRepository;

import java.util.ArrayList;
import java.util.List;


public class StatisticsActivity extends AppCompatActivity implements RecyclerViewStatisticsAdapter.ItemClickListener {
    static private final String TAG = StatisticsActivity.class.getSimpleName();

    private RecyclerViewStatisticsAdapter mAdapter;

    StationRepository stationRepository;
    SongRepository songRepository;
    ArtistRepository artistRepository;

    private Spinner mSpinnerRadioNames;
    private Spinner mSpinnerTextViewTimeFrom;

    TextView mTextViewTotalUniqueArtists;
    TextView mTextViewTotalUniqueSongs;

    private int selectedRadioId;
    private String selectedTime;
    private int selectedRadioFromMainActivity;

    private Button mButtonSearch;

    List<Station> stations;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        Log.d(TAG, "onCreate");
        super.onCreate(savedInstanceState);
        setContentView(R.layout.statistics_main);


        selectedRadioFromMainActivity = getIntent().getIntExtra(C.PLAYING_RADIO_ID, -1);
        if (getIntent().getIntExtra(C.MUSIC_SERVICE_UI, -1) == 0){
            setTitle("Radio not playing");
        } else {
            setTitle("Radio playing");
        }

        mSpinnerRadioNames = (Spinner) findViewById(R.id.spinnerRadioNames);
        mSpinnerTextViewTimeFrom = (Spinner) findViewById(R.id.spinnerTimeFrom);
        mButtonSearch = (Button) findViewById(R.id.buttonSearch);
        mTextViewTotalUniqueArtists = (TextView) findViewById(R.id.textViewTotalUniqueArtists);
        mTextViewTotalUniqueSongs = (TextView) findViewById(R.id.textViewTotalUniqueSongs);

        stationRepository = new StationRepository(this);
        stationRepository.open();
        songRepository = new SongRepository(this);
        songRepository.open();
        artistRepository = new ArtistRepository(this);
        artistRepository.open();

        // Radio names spinner dropdown elements
        stations = stationRepository.getAll();
        List<String> stationNames = new ArrayList<>();

        for (Station station : stations){
            stationNames.add(station.stationName);
        }
        InitializeSpinners(stationNames, mSpinnerRadioNames);

        List<String> timeFrom = new ArrayList<>();
        timeFrom.add(C.SELECT_ALL);
        timeFrom.add(C.SELECT_LAST_1_HOUR);
        timeFrom.add(C.SELECT_LAST_12_HOURS);
        timeFrom.add(C.SELECT_LAST_DAY);

        InitializeSpinners(timeFrom, mSpinnerTextViewTimeFrom);
        SearchForStatistics();
    }

    public void InitializeSpinners(List<String> values, Spinner spinner){
        ArrayAdapter<String> dataAdapter = new ArrayAdapter<>(this, R.layout.spinner_item, values);
        dataAdapter.setDropDownViewResource(R.layout.spinner_dropdown_item);
        spinner.setAdapter(dataAdapter);
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu){
        getMenuInflater().inflate(R.menu.actionbar_menu, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        if (item.getItemId() == R.id.Back){
            finish();
        } else {
            return super.onOptionsItemSelected(item);
        }
        return true;
    }

    public void SearchForStatistics(){
        if (selectedRadioFromMainActivity != -1){
            selectedRadioId = selectedRadioFromMainActivity;
            mSpinnerRadioNames.setSelection(selectedRadioFromMainActivity - 1);
            selectedRadioFromMainActivity = -1;
        } else {
            selectedRadioId = stationRepository.getStationByName(mSpinnerRadioNames.getSelectedItem().toString());
        }

        selectedTime = mSpinnerTextViewTimeFrom.getSelectedItem().toString();

        List<Artist> artists = artistRepository.getAllArtistAndSongsByStationId(selectedRadioId, selectedTime);

        mTextViewTotalUniqueArtists.setText(String.valueOf(artists.size()));
        int songCount = 0;
        for (Artist artist: artists){
            songCount += artist.songs.size();
        }
        mTextViewTotalUniqueSongs.setText(String.valueOf(songCount));

        // set up recyclerView for mStations
        RecyclerView statisticsRecycleView =  findViewById(R.id.recyclerViewStatistics);
        statisticsRecycleView.setLayoutManager(new LinearLayoutManager(this));
        mAdapter = new RecyclerViewStatisticsAdapter(this, artists);
        mAdapter.setItemClickListener(this);
        statisticsRecycleView.setAdapter(mAdapter);
    }

    public void searchButtonOnClick(View view) {
        SearchForStatistics();
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
        stationRepository.close();
        artistRepository.close();
        songRepository.close();
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
        outState.putInt(C.SEARCH_STATION_DATA, selectedRadioId);
        outState.putString(C.SEARCH_DATETIME_DATA, selectedTime);
    }

    @Override
    protected void onRestoreInstanceState(Bundle savedInstanceState) {
        Log.d(TAG, "onRestoreInstanceState");
        super.onRestoreInstanceState(savedInstanceState);
        selectedRadioId = savedInstanceState.getInt(C.SEARCH_STATION_DATA);
        selectedTime = savedInstanceState.getString(C.SEARCH_DATETIME_DATA);
        SearchForStatistics();
    }

    @Override
    public void onStatisticsRecyclerViewRowClick(View view, int position) {
    }

}
