package com.example.radio2019.Domain;

public class Song {
    public int id;
    public String songTitle;
    public int timesPlayed;

    public Song(String songTitle, int timesPlayed){
        this(0, songTitle, timesPlayed);
    }

    public Song(int id, String songTitle, int timesPlayed){
        this.id = id;
        this.songTitle = songTitle;
        this.timesPlayed = timesPlayed;
    }
}
