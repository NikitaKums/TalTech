package com.example.radio2019;

import android.content.Context;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;

public class DatabaseHelper extends SQLiteOpenHelper {

    public static final String DB_NAME = "RADIO.DB";
    public static final int DB_VERSION = 1;

    // Stations
    public static final String STATION_TABLE_NAME = "STATIONS";

    public static final String STATION_ID = "_id";
    public static final String STATION_NAME = "stationName";
    public static final String STATION_LISTEN_URL = "stationListenUrl";
    public static final String STATION_JSON_URL = "stationJSONUrl";

    public static final String STATION_CREATE_TABLE_SQL = "create table " + STATION_TABLE_NAME +
            "(" +
            STATION_ID + " INTEGER PRIMARY KEY AUTOINCREMENT, " +
            STATION_NAME + " TEXT NOT NULL, " +
            STATION_LISTEN_URL + " TEXT NOT NULL, " +
            STATION_JSON_URL + " TEXT NOT NULL);";


    // Artists
    public static final String ARTIST_TABLE_NAME = "ARTISTS";

    public static final String ARTIST_ID = "_id";
    public static final String ARTIST_NAME = "artistName";

    public static final String ARTISTS_CREATE_TABLE_SQL = "create table " + ARTIST_TABLE_NAME +
            "(" +
            ARTIST_ID + " INTEGER PRIMARY KEY AUTOINCREMENT, " +
            ARTIST_NAME + " TEXT NOT NULL );";

    // Songs
    public static final String SONG_TABLE_NAME = "SONGS";

    public static final String SONG_ID = "_id";
    public static final String SONG_SONGTITLE = "songTitle";
    public static final String SONG_TIMES_PLAYED = "timesPlayed";
    public static final String SONG_ARTIST_FK_ID = "song_artist_fk_id";
    public static final String SONG_STATION_FK_ID = "song_station_fk_id";
    public static final String SONG_DATETIME = "song_datetime";

    public static final String SONG_CREATE_TABLE_SQL = "create table " + SONG_TABLE_NAME +
            "(" +
            SONG_ID + " INTEGER PRIMARY KEY AUTOINCREMENT, " +
            SONG_SONGTITLE + " TEXT NOT NULL, " +
            SONG_TIMES_PLAYED + " INTEGER NOT NULL, " +
            SONG_ARTIST_FK_ID + " INTEGER NOT NULL, " +
            SONG_STATION_FK_ID + " INTEGER NOT NULL, " +
            SONG_DATETIME + " DATETIME DEFAULT CURRENT_TIMESTAMP, " +
            "FOREIGN KEY(" + SONG_STATION_FK_ID + ") REFERENCES " + STATION_TABLE_NAME + "(_id), " +
            "FOREIGN KEY(" + SONG_ARTIST_FK_ID +") REFERENCES " + ARTIST_TABLE_NAME + "(_id));";


    public DatabaseHelper(Context context) {
        super(context, DB_NAME, null, DB_VERSION);
    }

    @Override
    public void onCreate(SQLiteDatabase db) {
        db.execSQL(SONG_CREATE_TABLE_SQL);
        db.execSQL(STATION_CREATE_TABLE_SQL);
        db.execSQL(ARTISTS_CREATE_TABLE_SQL);
    }

    @Override
    public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
        db.execSQL("DROP TABLE IF EXISTS " + STATION_TABLE_NAME);
        db.execSQL("DROP TABLE IF EXISTS " + ARTIST_TABLE_NAME);
        db.execSQL("DROP TABLE IF EXISTS " + SONG_TABLE_NAME);
        onCreate(db);
    }
}
