package com.example.sportmapapp;

import android.content.Context;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;

public class DatabaseHelper extends SQLiteOpenHelper {

    public static final String DB_NAME = "SPORTMAPAPP.DB";
    public static final int DB_VERSION = 1;

    // Sessions
    public static final String SESSION_TABLE_NAME = "SESSIONS";

    public static final String SESSION_ID = "_id";
    public static final String TOTAL_DISTANCE = "total_distance";
    public static final String TOTAL_TIME = "total_time";
    public static final String PACE = "pace";

    public static final String SESSION_CREATE_TABLE_SQL = "create table " + SESSION_TABLE_NAME +
            "(" +
            SESSION_ID + " INTEGER PRIMARY KEY AUTOINCREMENT, " +
            TOTAL_DISTANCE + " TEXT, " +
            TOTAL_TIME + " TEXT, " +
            PACE + " TEXT );";

    // Locations
    public static final String LOCATION_TABLE_NAME = "LOCATIONS";

    public static final String LOCATION_ID = "_id";
    public static final String SESSION_FK_ID = "session_fk_id";
    public static final String LATITUDE = "latitude";
    public static final String LONGITUDE = "longitude";
    public static final String CP_LATITUDE = "cp_latitude";
    public static final String CP_LONGITUDE = "cp_longitude";

    public static final String LOCATION_CREATE_TABLE_SQL = "create table " + LOCATION_TABLE_NAME +
            "(" +
            LOCATION_ID + " INTEGER PRIMARY KEY AUTOINCREMENT, " +
            LATITUDE + " TEXT NOT NULL, " +
            LONGITUDE + " TEXT NOT NULL, " +
            CP_LATITUDE + " TEXT, " +
            CP_LONGITUDE + " TEXT, " +
            SESSION_FK_ID + " INTEGER NOT NULL, " +
            "FOREIGN KEY(" + SESSION_FK_ID + ") REFERENCES " + SESSION_TABLE_NAME + "(_id));";

    public DatabaseHelper(Context context) {
        super(context, DB_NAME, null, DB_VERSION);
    }

    @Override
    public void onCreate(SQLiteDatabase db) {
        db.execSQL(SESSION_CREATE_TABLE_SQL);
        db.execSQL(LOCATION_CREATE_TABLE_SQL);
    }

    @Override
    public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
        db.execSQL("DROP TABLE IF EXISTS " + SESSION_TABLE_NAME);
        db.execSQL("DROP TABLE IF EXISTS " + LOCATION_TABLE_NAME);
        onCreate(db);
    }

}
