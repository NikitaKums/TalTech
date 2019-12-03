package com.example.sportmapapp.Domain;

public class Session {
    public int id;
    public String totalDistance;
    public String totalTime;
    public String pace;

    public Session(String totalDistance, String totalTime, String pace){
        this(0, totalDistance, totalTime, pace);
    }

    public Session(int id, String totalDistance, String totalTime, String pace){
        this.id = id;
        this.totalDistance = totalDistance;
        this.totalTime = totalTime;
        this.pace = pace;
    }
}
