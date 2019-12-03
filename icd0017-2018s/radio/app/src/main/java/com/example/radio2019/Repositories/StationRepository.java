package com.example.radio2019.Repositories;

import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;

import com.example.radio2019.DatabaseHelper;
import com.example.radio2019.Domain.Station;

import java.util.ArrayList;
import java.util.List;

public class StationRepository {

    private DatabaseHelper dbHelper;
    private Context context;
    private SQLiteDatabase db;

    public StationRepository(Context context){
        this.context = context;
    }

    public StationRepository open(){
        dbHelper = new DatabaseHelper(context);
        db = dbHelper.getWritableDatabase();
        return this;
    }

    public void close(){
        dbHelper.close();
    }

    public void add(Station station){
        ContentValues contentValues = new ContentValues();
        contentValues.put(DatabaseHelper.STATION_NAME, station.stationName);
        contentValues.put(DatabaseHelper.STATION_LISTEN_URL, station.stationListenUrl);
        contentValues.put(DatabaseHelper.STATION_JSON_URL, station.stationJSONUrl);
        db.insert(DatabaseHelper.STATION_TABLE_NAME,null, contentValues);
    }

    private Cursor fetch(){
        String[] columns = new String[]{
                DatabaseHelper.STATION_ID,
                DatabaseHelper.STATION_NAME,
                DatabaseHelper.STATION_LISTEN_URL,
                DatabaseHelper.STATION_JSON_URL
        };
        Cursor cursor = db.query(DatabaseHelper.STATION_TABLE_NAME, columns,
                null,null,null, null, null);
        // is this really neccesary?
        if (cursor != null){
            cursor.moveToFirst();
        }

        return cursor;
    }

    public List<Station> getAll(){
        ArrayList<Station> stations =  new ArrayList<>();
        Cursor cursor = fetch();
        if (cursor.moveToFirst()){
            do{
                stations.add(new Station(
                        cursor.getInt(cursor.getColumnIndex(DatabaseHelper.STATION_ID)),
                        cursor.getString(cursor.getColumnIndex(DatabaseHelper.STATION_NAME)),
                        cursor.getString(cursor.getColumnIndex(DatabaseHelper.STATION_LISTEN_URL)),
                        cursor.getString(cursor.getColumnIndex(DatabaseHelper.STATION_JSON_URL))));
            } while (cursor.moveToNext());
        }

        return stations;
    }

    public int getStationByName(String stationName){
        Cursor cursor = db.rawQuery("SELECT " +
                DatabaseHelper.STATION_ID + ", " +
                DatabaseHelper.STATION_NAME + " FROM " +
                DatabaseHelper.STATION_TABLE_NAME + " WHERE " +
                DatabaseHelper.STATION_NAME + " = '" + stationName +"'", null);
        cursor.moveToFirst();
        int result = cursor.getInt(cursor.getColumnIndex(DatabaseHelper.STATION_ID));
        cursor.close();
        return result;
    }



}

