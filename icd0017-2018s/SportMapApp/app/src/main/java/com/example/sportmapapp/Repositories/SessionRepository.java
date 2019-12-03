package com.example.sportmapapp.Repositories;

import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;

import com.example.sportmapapp.DatabaseHelper;
import com.example.sportmapapp.Domain.Session;

import java.util.ArrayList;
import java.util.List;

public class SessionRepository {

    private DatabaseHelper dbHelper;
    private Context context;
    private SQLiteDatabase db;

    private int lastInsertId;

    public SessionRepository(Context context){
        this.context = context;
    }

    public SessionRepository open(){
        dbHelper = new DatabaseHelper(context);
        db = dbHelper.getWritableDatabase();
        return this;
    }

    public void close(){
        dbHelper.close();
    }

    public void add(Session session){
        ContentValues contentValues = new ContentValues();
        contentValues.put(DatabaseHelper.TOTAL_DISTANCE, session.totalDistance);
        contentValues.put(DatabaseHelper.TOTAL_TIME, session.totalTime);
        contentValues.put(DatabaseHelper.PACE, session.pace);
        lastInsertId = (int) db.insert(DatabaseHelper.SESSION_TABLE_NAME,null, contentValues);
    }

    public void update(Session session){
        db.execSQL("UPDATE " +
                DatabaseHelper.SESSION_TABLE_NAME + " SET " +
                DatabaseHelper.TOTAL_DISTANCE + " = '" +
                session.totalDistance + "', " +
                DatabaseHelper.TOTAL_TIME + " = '" +
                session.totalTime + "', " +
                DatabaseHelper.PACE + " = '" +
                session.pace + "' WHERE " +
                DatabaseHelper.SESSION_ID + " = " + session.id);
    }


    public Session getSessionById(int sessionId){
        Session session = null;

        Cursor cursor = db.rawQuery("SELECT * FROM " +
                DatabaseHelper.SESSION_TABLE_NAME + " WHERE " +
                DatabaseHelper.SESSION_ID + " = " + sessionId, null);

        if (cursor.moveToFirst()){
            session = new Session(cursor.getInt(cursor.getColumnIndex(DatabaseHelper.SESSION_ID)),
                    cursor.getString(cursor.getColumnIndex(DatabaseHelper.TOTAL_DISTANCE)),
                    cursor.getString(cursor.getColumnIndex(DatabaseHelper.TOTAL_TIME)),
                    cursor.getString(cursor.getColumnIndex(DatabaseHelper.PACE)));
        }
        cursor.close();
        return session;
    }

    public List<Session> getSessions(){
        ArrayList<Session> sessions = new ArrayList<>();

        Cursor cursor = db.rawQuery("SELECT * FROM " +
                DatabaseHelper.SESSION_TABLE_NAME, null);

        if (cursor.moveToFirst()){
           do {
               sessions.add(new Session(cursor.getInt(cursor.getColumnIndex(DatabaseHelper.SESSION_ID)),
                       cursor.getString(cursor.getColumnIndex(DatabaseHelper.TOTAL_DISTANCE)),
                       cursor.getString(cursor.getColumnIndex(DatabaseHelper.TOTAL_TIME)),
                       cursor.getString(cursor.getColumnIndex(DatabaseHelper.PACE))));
           } while (cursor.moveToNext());
        }
        cursor.close();
        return sessions;
    }

    public int getLastSessionId(){
        return lastInsertId;
    }

    public void deleteById(int sessionId) {
        db.execSQL("DELETE FROM " + DatabaseHelper.SESSION_TABLE_NAME + " WHERE " + DatabaseHelper.SESSION_ID + " = " + sessionId);
    }
}
