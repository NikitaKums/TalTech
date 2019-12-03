package com.example.radio2019.Domain;

import java.util.ArrayList;

public class Artist {
    public int id;
    public String artistName;
    public ArrayList<Song> songs;

    public Artist(String artistName){
        this(0, artistName);
    }

    public Artist(int id, String artistName, ArrayList<Song> songs){
        this.id = id;
        this.artistName = artistName;
        this.songs = songs;
    }

    public Artist(int id, String artistName){
        this.id = id;
        this.artistName = artistName;
    }
}
