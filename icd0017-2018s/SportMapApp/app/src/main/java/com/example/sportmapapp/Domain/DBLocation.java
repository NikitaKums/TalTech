package com.example.sportmapapp.Domain;

public class DBLocation {
    public int id;
    public String latitude;
    public String longitude;
    public String checkPointLatitude;
    public String checkPointLongitude;
    public int sessionId;

    public DBLocation(String latitude, String longitude, String checkPointLatitude, String checkPointLongitude, int sessionId){
        this(0, latitude, longitude, checkPointLatitude, checkPointLongitude, sessionId);
    }

    public DBLocation(int id, String latitude, String longitude, String checkPointLatitude, String checkPointLongitude, int sessionId){
        this.id = id;
        this.latitude = latitude;
        this.longitude = longitude;
        this.checkPointLatitude = checkPointLatitude;
        this.checkPointLongitude = checkPointLongitude;
        this.sessionId = sessionId;
    }
}
