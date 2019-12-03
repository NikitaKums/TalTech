package com.example.sportmapapp.Repositories;

import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;

import com.example.sportmapapp.DatabaseHelper;
import com.example.sportmapapp.Domain.DBLocation;

import java.util.ArrayList;
import java.util.List;

public class LocationRepository {

    private DatabaseHelper dbHelper;
    private Context context;
    private SQLiteDatabase db;


    public LocationRepository(Context context){
        this.context = context;
    }

    public LocationRepository open(){
        dbHelper = new DatabaseHelper(context);
        db = dbHelper.getWritableDatabase();
        return this;
    }

    public void close(){
        dbHelper.close();
    }

    public void add(DBLocation location){
        ContentValues contentValues = new ContentValues();
        contentValues.put(DatabaseHelper.LATITUDE, location.latitude);
        contentValues.put(DatabaseHelper.LONGITUDE, location.longitude);
        contentValues.put(DatabaseHelper.CP_LATITUDE, location.checkPointLatitude);
        contentValues.put(DatabaseHelper.CP_LONGITUDE, location.checkPointLongitude);
        contentValues.put(DatabaseHelper.SESSION_FK_ID, location.sessionId);
        db.insert(DatabaseHelper.LOCATION_TABLE_NAME,null, contentValues);
    }

    public List<ArrayList<DBLocation>> getAll(){
        ArrayList<ArrayList<DBLocation>> dbLocations = new ArrayList<>();

        int previousSessionId = -1;

        Cursor cursor = db.rawQuery("SELECT * FROM "
                + DatabaseHelper.LOCATION_TABLE_NAME, null);

        if (cursor.moveToFirst()){
            do {

                int locationId = cursor.getInt(cursor.getColumnIndex(DatabaseHelper.LOCATION_ID));
                String latitude = cursor.getString(cursor.getColumnIndex(DatabaseHelper.LATITUDE));
                String longitude = cursor.getString(cursor.getColumnIndex(DatabaseHelper.LONGITUDE));
                String cpLatitude = cursor.getString(cursor.getColumnIndex(DatabaseHelper.CP_LATITUDE));
                String cpLongitude = cursor.getString(cursor.getColumnIndex(DatabaseHelper.CP_LONGITUDE));
                int sessionId = cursor.getInt(cursor.getColumnIndex(DatabaseHelper.SESSION_ID));

                if (sessionId == previousSessionId){
                    for (ArrayList<DBLocation> dbLocationList : dbLocations){
                        if (dbLocationList.get(0).id == locationId){
                            dbLocationList.add(new DBLocation(locationId, latitude, longitude, cpLatitude, cpLongitude, sessionId));
                        }
                    }
                } else {
                    ArrayList<DBLocation> newLocationList = new ArrayList<>();
                    newLocationList.add(new DBLocation(locationId, latitude, longitude, cpLatitude, cpLongitude, sessionId));
                    dbLocations.add(newLocationList);
                    previousSessionId = sessionId;
                }

            } while (cursor.moveToNext());
        }
        cursor.close();
        return dbLocations;
    }

    public List<DBLocation> getBySessionId(int sessionId){
        ArrayList<DBLocation> dbLocations = new ArrayList<>();

        Cursor cursor = db.rawQuery("SELECT * FROM "
                + DatabaseHelper.LOCATION_TABLE_NAME + " WHERE " +
                DatabaseHelper.SESSION_FK_ID + " = " + sessionId, null);

        if (cursor.moveToFirst()){
            do {
                dbLocations.add(new DBLocation(
                        cursor.getInt(cursor.getColumnIndex(DatabaseHelper.LOCATION_ID)),
                        cursor.getString(cursor.getColumnIndex(DatabaseHelper.LATITUDE)),
                        cursor.getString(cursor.getColumnIndex(DatabaseHelper.LONGITUDE)),
                        cursor.getString(cursor.getColumnIndex(DatabaseHelper.CP_LATITUDE)),
                        cursor.getString(cursor.getColumnIndex(DatabaseHelper.CP_LONGITUDE)),
                        cursor.getInt(cursor.getColumnIndex(DatabaseHelper.SESSION_ID))
                ));
            } while (cursor.moveToNext());
        }
        cursor.close();
        return dbLocations;
    }

    public void deleteById(int sessionId){
        db.execSQL("DELETE FROM " + DatabaseHelper.LOCATION_TABLE_NAME + " WHERE " + DatabaseHelper.SESSION_FK_ID + " = " + sessionId);
    }

}