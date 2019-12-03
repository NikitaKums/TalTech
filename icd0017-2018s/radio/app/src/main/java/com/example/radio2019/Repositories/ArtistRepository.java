package com.example.radio2019.Repositories;

import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;

import com.example.radio2019.C;
import com.example.radio2019.DatabaseHelper;
import com.example.radio2019.Domain.Artist;
import com.example.radio2019.Domain.Song;

import java.util.ArrayList;
import java.util.List;

public class ArtistRepository {
    private DatabaseHelper dbHelper;
    private Context context;
    private SQLiteDatabase db;

    public ArtistRepository(Context context){
        this.context = context;
    }

    public ArtistRepository open(){
        dbHelper = new DatabaseHelper(context);
        db = dbHelper.getWritableDatabase();
        return this;
    }

    public void close(){
        dbHelper.close();
    }

    public long add(Artist artist){
        ContentValues contentValues = new ContentValues();
        contentValues.put(DatabaseHelper.ARTIST_NAME, artist.artistName);
        return db.insert(DatabaseHelper.ARTIST_TABLE_NAME,null, contentValues);
    }

    private Cursor fetch(int stationId, String dateTime){

        String dateTimeQuery;

        switch (dateTime){
            case C.SELECT_LAST_1_HOUR:
                dateTimeQuery = "> datetime('now', '-1 hour')";
                break;
            case C.SELECT_LAST_12_HOURS:
                dateTimeQuery = "> datetime('now', '-12 hour')";
                break;
            case C.SELECT_LAST_DAY:
                dateTimeQuery = "> datetime('now', '-1 day')";
                break;
            default:
                dateTimeQuery = "< datetime('now')";
                break;
        }

        Cursor cursor = db.rawQuery(
                "SELECT " + DatabaseHelper.ARTIST_TABLE_NAME + "." + DatabaseHelper.ARTIST_ID + ", " +
                        DatabaseHelper.ARTIST_TABLE_NAME + "." + DatabaseHelper.ARTIST_NAME + ", " +
                        DatabaseHelper.SONG_TABLE_NAME + "." + DatabaseHelper.SONG_SONGTITLE+", " +
                        DatabaseHelper.SONG_TABLE_NAME + "." + DatabaseHelper.SONG_ID + ", " +
                        DatabaseHelper.SONG_TABLE_NAME + "." + DatabaseHelper.SONG_TIMES_PLAYED + " FROM " +
                        DatabaseHelper.ARTIST_TABLE_NAME + " LEFT JOIN " +
                        DatabaseHelper.SONG_TABLE_NAME + " ON " +
                        DatabaseHelper.ARTIST_TABLE_NAME + "." + DatabaseHelper.ARTIST_ID + " = " +
                        DatabaseHelper.SONG_TABLE_NAME + "." + DatabaseHelper.SONG_ARTIST_FK_ID + " WHERE " +
                DatabaseHelper.SONG_TABLE_NAME + "." + DatabaseHelper.SONG_STATION_FK_ID + " = " + stationId + " AND " +
                DatabaseHelper.SONG_DATETIME + dateTimeQuery, null);

        if (cursor != null){
            cursor.moveToFirst();
        }

        return cursor;
    }

    // returns artists with songs that have played on given station

    public List<Artist> getAllArtistAndSongsByStationId(int stationId, String dateTime){
        ArrayList<Artist> artists =  new ArrayList<>();
        Cursor cursor = fetch(stationId, dateTime);

        boolean alreadyInList = false;

        int artistId;
        String artistName;

        int songId;
        String songTitle;
        int songTimesPlayed;

        if (cursor.moveToFirst()){
            do{
                alreadyInList = false;

                artistId = cursor.getInt(cursor.getColumnIndex(DatabaseHelper.ARTIST_ID));
                artistName = cursor.getString(cursor.getColumnIndex(DatabaseHelper.ARTIST_NAME));

                songId = cursor.getInt(cursor.getColumnIndex(DatabaseHelper.SONG_ID));
                songTitle = cursor.getString(cursor.getColumnIndex(DatabaseHelper.SONG_SONGTITLE));
                songTimesPlayed = cursor.getInt(cursor.getColumnIndex(DatabaseHelper.SONG_TIMES_PLAYED));

                for (Artist artist: artists) {
                    if (artist.artistName.equals(artistName)){
                        artist.songs.add(new Song(songId, songTitle, songTimesPlayed));
                        alreadyInList = true;
                    }
                }
                if (!alreadyInList) {
                    ArrayList<Song> songs = new ArrayList<>();
                    songs.add(new Song(songId, songTitle, songTimesPlayed));
                    artists.add(new Artist(artistId, artistName, songs));
                }
            } while (cursor.moveToNext());
        }

        return artists;
    }

    // get artist by name | can also check if artist exists with this method
    public Artist insertArtistIfNotExists(String name, String trackTitle){
        Artist artist;
        if (name.isEmpty() || trackTitle.isEmpty()){
            return null;
        }
        Cursor cursor = db.rawQuery("SELECT " + DatabaseHelper.ARTIST_NAME + ", " + DatabaseHelper.ARTIST_ID + " FROM " + DatabaseHelper.ARTIST_TABLE_NAME +
                " WHERE " + DatabaseHelper.ARTIST_NAME + " = '" + name + "'", null);
        if (cursor.moveToNext()){
            artist = new Artist(cursor.getInt(cursor.getColumnIndex(DatabaseHelper.ARTIST_ID)),
                    cursor.getString(cursor.getColumnIndex(DatabaseHelper.ARTIST_NAME)));
        } else {
            artist = new Artist(name);
            long id = add(artist);
            artist.id = (int) id;
        }
        cursor.close();
        return artist;
    }


}
