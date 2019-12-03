package com.example.radio2019.Repositories;

import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;

import com.example.radio2019.DatabaseHelper;
import com.example.radio2019.Domain.Song;

import java.util.ArrayList;
import java.util.List;

public class SongRepository {

    private DatabaseHelper dbHelper;
    private Context context;
    private SQLiteDatabase db;

    public SongRepository(Context context){
        this.context = context;
    }

    public SongRepository open(){
        dbHelper = new DatabaseHelper(context);
        db = dbHelper.getWritableDatabase();
        return this;
    }

    public void close(){
        dbHelper.close();
    }

    public void add(Song song, int artistId, int stationId){
        ContentValues contentValues = new ContentValues();
        contentValues.put(DatabaseHelper.SONG_SONGTITLE, song.songTitle);
        contentValues.put(DatabaseHelper.SONG_TIMES_PLAYED, song.timesPlayed);
        contentValues.put(DatabaseHelper.SONG_ARTIST_FK_ID, artistId);
        contentValues.put(DatabaseHelper.SONG_STATION_FK_ID, stationId);
        db.insert(DatabaseHelper.SONG_TABLE_NAME,null, contentValues);
    }

    private Cursor fetch(){
        String[] columns = new String[]{
                DatabaseHelper.SONG_ID,
                DatabaseHelper.SONG_SONGTITLE,
                DatabaseHelper.SONG_TIMES_PLAYED,
                DatabaseHelper.SONG_DATETIME
        };
        Cursor cursor = db.query(DatabaseHelper.SONG_TABLE_NAME, columns,
                null,null,null, null, null);
        // is this really neccesary?
        if (cursor != null){
            cursor.moveToFirst();
        }

        return cursor;
    }

    public List<Song> getAll(){
        ArrayList<Song> songs =  new ArrayList<>();
        Cursor cursor = fetch();
        if (cursor.moveToFirst()){
            do{
                songs.add(new Song(
                        cursor.getInt(cursor.getColumnIndex(DatabaseHelper.SONG_ID)),
                        cursor.getString(cursor.getColumnIndex(DatabaseHelper.SONG_SONGTITLE)),
                        cursor.getInt(cursor.getColumnIndex(DatabaseHelper.SONG_TIMES_PLAYED))));
            } while (cursor.moveToNext());
        }

        return songs;
    }

    // check if song exists
    public Song exists(String songTitle, int artistId, int stationId){

        Cursor cursor = db.rawQuery("SELECT * FROM " +
                DatabaseHelper.SONG_TABLE_NAME + " WHERE " +
                DatabaseHelper.SONG_SONGTITLE  + " = '" +
                songTitle +"' AND " +
                DatabaseHelper.SONG_ARTIST_FK_ID + " = " + artistId + " AND "+
                DatabaseHelper.SONG_STATION_FK_ID + " = " + stationId, null);

        Song song = null;
        if (cursor.moveToFirst()){
               song = new Song(cursor.getInt(cursor.getColumnIndex(DatabaseHelper.SONG_ID)),
                        cursor.getString(cursor.getColumnIndex(DatabaseHelper.SONG_SONGTITLE)),
                        cursor.getInt(cursor.getColumnIndex(DatabaseHelper.SONG_TIMES_PLAYED)));

        }
        cursor.close();
        return song;
    }

    public void updateOrInsertSong(String artist, String songTitle, int artistId, int stationId){

        if (!artist.isEmpty() && !songTitle.isEmpty()){
            Song song = exists(songTitle, artistId, stationId);
            if (song != null){
                int incrementPlayTimes = song.timesPlayed + 1;
                db.execSQL("UPDATE " +
                        DatabaseHelper.SONG_TABLE_NAME + " SET " +
                        DatabaseHelper.SONG_TIMES_PLAYED + " = " +
                        incrementPlayTimes + ", " +
                        DatabaseHelper.SONG_DATETIME + " = datetime() WHERE " +
                        DatabaseHelper.SONG_SONGTITLE  + " = '" +
                        songTitle +"' AND " +
                        DatabaseHelper.SONG_STATION_FK_ID + " = " + stationId + " AND " +
                        DatabaseHelper.SONG_ARTIST_FK_ID + " = " + artistId);
            } else {
                add(new Song(songTitle, 1), artistId, stationId);
            }
        }

    }


}
