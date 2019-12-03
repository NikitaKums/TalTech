package com.example.radio2019;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.database.ContentObserver;
import android.media.AudioManager;
import android.os.Handler;
import android.support.v4.content.LocalBroadcastManager;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.support.v7.widget.DividerItemDecoration;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.SeekBar;
import android.widget.TextView;

import com.example.radio2019.Domain.Artist;
import com.example.radio2019.Domain.Station;
import com.example.radio2019.Repositories.ArtistRepository;
import com.example.radio2019.Repositories.SongRepository;
import com.example.radio2019.Repositories.StationRepository;

import java.util.List;

public class MainActivity extends AppCompatActivity implements RecyclerViewStationAdapter.ItemClickListener {

    static private final String TAG = MainActivity.class.getSimpleName();
    private BroadcastReceiver mBroadcastReceiver;
    private SettingsContentObserver mSettingsContentObserver;

    private RecyclerViewStationAdapter mAdapter;
    private StationRepository mStationRepository;
    private SongRepository mSongRepository;
    private ArtistRepository mArtistRepository;
    List<Station> stations;
    private int playingStationId;

    private String mediaSourceKeyValue;
    private String mediaSourceKeyJSONValue;

    private int mMusicPlayerStatus = C.MUSICSERVICE_STOPPED;
    private Button mButtonControlMusic;

    private SeekBar mSeekBarVolume;
    private TextView mTextViewArtist;
    private TextView mTextViewTitle;

    private String mArtist;
    private String mTrackTitle;
    private String previousTrackTitle;
    private int previousStationId;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        Log.d(TAG, "onCreate");
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        setTitle("Radio not playing");

        mButtonControlMusic = (Button) findViewById(R.id.buttonControlMusic);
        mTextViewArtist = (TextView) findViewById(R.id.textViewArtist);
        mTextViewTitle = (TextView) findViewById(R.id.textViewTitle);
        mSeekBarVolume = (SeekBar) findViewById(R.id.seekBarVolume);

        AudioManager audio = (AudioManager) getApplicationContext().getSystemService(Context.AUDIO_SERVICE);
        mSeekBarVolume.setMax(audio.getStreamMaxVolume(AudioManager.STREAM_MUSIC));

        mSeekBarVolume.setOnSeekBarChangeListener(new SeekBar.OnSeekBarChangeListener() {
            @Override
            public void onProgressChanged(SeekBar seekBar, int progress, boolean fromUser) {
                AudioManager audio = (AudioManager) getApplicationContext().getSystemService(Context.AUDIO_SERVICE);
                audio.setStreamVolume(AudioManager.STREAM_MUSIC, progress, AudioManager.FLAG_SHOW_UI);
            }

            @Override
            public void onStartTrackingTouch(SeekBar seekBar) {

            }

            @Override
            public void onStopTrackingTouch(SeekBar seekBar) {

            }
        });

        IntentFilter intentFilter = new IntentFilter();
        intentFilter.addAction(C.MUSICSERVICE_INTENT_BUFFERING);
        intentFilter.addAction(C.MUSICSERVICE_INTENT_PLAYING);
        intentFilter.addAction(C.MUSICSERVICE_INTENT_STOPPED);
        intentFilter.addAction(C.MUSICSERVICE_INTENT_SONGINFO);


        mStationRepository = new StationRepository(this);
        mStationRepository.open();

        if (mStationRepository.getAll().size() == 0){
            mStationRepository.add(new Station("SKYPLUS", "http://sky.babahhcdn.com/SKYPLUS", "http://dad.akaver.com/api/SongTitles/SP"));
            mStationRepository.add(new Station("RRAP", "http://sky.babahhcdn.com/rrap", "http://dad.akaver.com/api/SongTitles/RRAP"));
            mStationRepository.add(new Station("NRJ", "http://sky.babahhcdn.com/NRJ", "http://dad.akaver.com/api/SongTitles/NRJ"));
            mStationRepository.add(new Station("RR", "http://sky.babahhcdn.com/RR", "http://dad.akaver.com/api/SongTitles/RR"));
            mStationRepository.add(new Station("SKY", "http://sky.babahhcdn.com/SKY", "http://dad.akaver.com/api/SongTitles/SKY"));
            mStationRepository.add(new Station("RETRO", "http://sky.babahhcdn.com/RETRO", "http://dad.akaver.com/api/SongTitles/RETRO"));
        }

        stations = mStationRepository.getAll();

        mSongRepository = new SongRepository(this);
        mSongRepository.open();
        mArtistRepository = new ArtistRepository(this);
        mArtistRepository.open();

        RecyclerView stationRecycleView =  findViewById(R.id.recyclerViewStations);
        stationRecycleView.setLayoutManager(new LinearLayoutManager(this));
        mAdapter = new RecyclerViewStationAdapter(this, stations);
        mAdapter.setItemClickListener(this);
        stationRecycleView.setAdapter(mAdapter);

        DividerItemDecoration dividerItemDecoration = new DividerItemDecoration(stationRecycleView.getContext(), LinearLayoutManager.VERTICAL);
        dividerItemDecoration.setDrawable(stationRecycleView.getContext().getResources().getDrawable(R.drawable.recyclerview_divider));
        stationRecycleView.addItemDecoration(dividerItemDecoration);

        if (savedInstanceState != null){
            playingStationId = savedInstanceState.getInt(C.STATION_ID);
        } else{
            playingStationId = 1;
        }
        mediaSourceKeyValue = stations.get(playingStationId - 1).stationListenUrl;
        mediaSourceKeyJSONValue = stations.get(playingStationId - 1).stationJSONUrl;


        mBroadcastReceiver = new MainActivityBroadcastReceiver();
        LocalBroadcastManager.getInstance(getApplicationContext()).registerReceiver(mBroadcastReceiver, intentFilter);

    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu){
        getMenuInflater().inflate(R.menu.actionbar_menu_main_activity, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        if (item.getItemId() == R.id.statistics){
            Intent startStatisticsActivity = new Intent(getApplicationContext(), StatisticsActivity.class);
            startStatisticsActivity.putExtra(C.PLAYING_RADIO_ID, playingStationId);
            startStatisticsActivity.putExtra(C.MUSIC_SERVICE_UI, mMusicPlayerStatus);
            startActivity(startStatisticsActivity);
        }
        else {
            return super.onOptionsItemSelected(item);
        }
        return true;
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
        mSettingsContentObserver = new SettingsContentObserver( new Handler() );
        this.getApplicationContext().getContentResolver().registerContentObserver(
                android.provider.Settings.System.CONTENT_URI, true,
                mSettingsContentObserver );
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
        mSongRepository.close();
        mArtistRepository.close();
        mStationRepository.close();

         try {
             LocalBroadcastManager.getInstance(this).unregisterReceiver(mBroadcastReceiver);
             getContentResolver().unregisterContentObserver(mSettingsContentObserver);
         } catch (Exception e) {
             Log.d(TAG, e.getMessage());
         }
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
        outState.putInt(C.STATION_ID, playingStationId);
        outState.putInt(C.PREVIOUS_STATION_ID, previousStationId);
        outState.putString(C.PREVIOUS_TRACK_TITLE, previousTrackTitle);
        outState.putInt(C.MUSIC_SERVICE_UI, mMusicPlayerStatus);
        outState.putString(C.MUSICSERVICE_ARTIST, mArtist);
        outState.putString(C.MUSICSERVICE_TRACKTITLE, mTrackTitle);
    }

    @Override
    protected void onRestoreInstanceState(Bundle savedInstanceState) {
        Log.d(TAG, "onRestoreInstanceState");
        super.onRestoreInstanceState(savedInstanceState);
        playingStationId = savedInstanceState.getInt(C.STATION_ID);
        previousStationId = savedInstanceState.getInt(C.PREVIOUS_STATION_ID);
        previousTrackTitle = savedInstanceState.getString(C.PREVIOUS_TRACK_TITLE);
        mMusicPlayerStatus = savedInstanceState.getInt(C.MUSIC_SERVICE_UI);
        mArtist = savedInstanceState.getString(C.MUSICSERVICE_ARTIST);
        mTrackTitle = savedInstanceState.getString(C.MUSICSERVICE_TRACKTITLE);
        UpdateUI();
    }


    public void buttonControlMusicOnClick(View view) {
        Log.d(TAG, "buttonControlMusicOnClick");

        switch (mMusicPlayerStatus){
            case C.MUSICSERVICE_PLAYING:
                Intent intentStopService = new Intent(C.ACTIVITY_INTENT_STOPPMUSIC);
                LocalBroadcastManager.getInstance(getApplicationContext()).sendBroadcast(intentStopService);
                break;
            case C.MUSICSERVICE_STOPPED:
                startMusicService();
                break;
        }
    }

    public void startMusicService(){
        Intent intentStartService = new Intent(this, MusicService.class);
        intentStartService.putExtra(C.SERVICE_MEDIASOURCE_KEY, mediaSourceKeyValue);
        intentStartService.putExtra(C.SERVICE_MEDIASROUCE_KEY_JSON, mediaSourceKeyJSONValue);
        this.startService(intentStartService);
    }

    public void buttonVolumeMinusOnClick(View view) {
        mSeekBarVolume.setProgress(mSeekBarVolume.getProgress() - 1);
    }

    public void buttonVolumePlusOnClick(View view) {
        mSeekBarVolume.setProgress(mSeekBarVolume.getProgress() + 1);
    }

    public void SaveToOrUpdateDataBase(String artist, String trackTitle){

        if (artist.equals("---") || trackTitle.equals("---")){
            return;
        }
        if (!trackTitle.equals(previousTrackTitle)){
            Artist insertArtist = mArtistRepository.insertArtistIfNotExists(artist, trackTitle);
            if (insertArtist!= null){
                mSongRepository.updateOrInsertSong(artist, trackTitle, insertArtist.id, playingStationId);
            }
        } else {
            if (playingStationId != previousStationId){
                Artist insertArtist = mArtistRepository.insertArtistIfNotExists(artist, trackTitle);
                if (insertArtist!= null){
                    mSongRepository.updateOrInsertSong(artist, trackTitle, insertArtist.id, playingStationId);
                }
            }
        }
        previousStationId = playingStationId;
        previousTrackTitle = trackTitle;
    }

    public void UpdateUI(){
        switch (mMusicPlayerStatus){
            case C.MUSICSERVICE_STOPPED:
                this.setTitle("Radio not playing");
                mButtonControlMusic.setText(C.BUTTONCONTROLMUSIC_LABEL_STOPPED);
                mArtist = "---";
                mTrackTitle = "---";
                break;
            case C.MUSICSERVICE_BUFFERING:
                mButtonControlMusic.setText(C.BUTTONCONTROLMUSIC_LABEL_BUFFERING);
                break;
            case C.MUSICSERVICE_PLAYING:
                this.setTitle("Radio playing");
                mButtonControlMusic.setText(C.BUTTONCONTROLMUSIC_LABEL_PLAYING);
                break;
        }
        mTextViewArtist.setText(mArtist);
        mTextViewTitle.setText(mTrackTitle);
    }

    @Override
    public void onStationRecyclerViewRowClick(View view, int position) {
        String oldSourceKeyValue = mediaSourceKeyValue;
        mediaSourceKeyValue = stations.get(position).stationListenUrl;
        playingStationId = stations.get(position).id;

        if (!oldSourceKeyValue.equals(mediaSourceKeyValue)){
            mediaSourceKeyJSONValue = stations.get(position).stationJSONUrl;
            Intent radioChangeIntent = new Intent(C.STATION_CHANGE);
            LocalBroadcastManager.getInstance(getApplicationContext()).sendBroadcast(radioChangeIntent);
            startMusicService();
            UpdateUI();
        }

    }

    public class MainActivityBroadcastReceiver extends BroadcastReceiver {

        @Override
        public void onReceive(Context context, Intent intent) {
            Log.d(TAG, "onReceive " + intent.getAction());
            switch (intent.getAction()){
                case C.MUSICSERVICE_INTENT_BUFFERING:
                    mMusicPlayerStatus = C.MUSICSERVICE_BUFFERING;
                    break;
                case C.MUSICSERVICE_INTENT_PLAYING:
                    mMusicPlayerStatus = C.MUSICSERVICE_PLAYING;
                    break;
                case C.MUSICSERVICE_INTENT_STOPPED:
                    mMusicPlayerStatus = C.MUSICSERVICE_STOPPED;
                    break;
                case C.MUSICSERVICE_INTENT_SONGINFO:
                    mArtist = intent.getStringExtra(C.MUSICSERVICE_ARTIST).replaceAll("'", "");
                    mTrackTitle = intent.getStringExtra(C.MUSICSERVICE_TRACKTITLE).replaceAll("'", "");
                    SaveToOrUpdateDataBase(mArtist, mTrackTitle);
                    break;
            }

            UpdateUI();
        }
    }

    private class SettingsContentObserver extends ContentObserver {

        private int PreviousVolume = -1;
        /**
         * Creates a content observer.
         *
         * @param handler The handler to run {@link #onChange} on, or null if none.
         */
        public SettingsContentObserver(Handler handler) {
            super(handler);
        }

        @Override
        public void onChange(boolean selfChange) {
            super.onChange(selfChange);
            Log.v(TAG, "Settings change detected");

            AudioManager audio = (AudioManager) getApplicationContext().getSystemService(Context.AUDIO_SERVICE);
            int currentVolume = audio.getStreamVolume(AudioManager.STREAM_MUSIC);

            System.out.println(currentVolume);

            if (currentVolume != PreviousVolume){
                PreviousVolume = currentVolume;
                mSeekBarVolume.setProgress(currentVolume);
            }
        }
    }

}
