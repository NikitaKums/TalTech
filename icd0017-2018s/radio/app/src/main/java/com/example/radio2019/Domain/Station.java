package com.example.radio2019.Domain;

public class Station {
    public int id;
    public String stationName;

    // ---- meta ----
    public String stationListenUrl;
    public String stationJSONUrl;

    public Station(String stationName, String stationListenUrl, String stationJSONUrl){
        this(0, stationName, stationListenUrl, stationJSONUrl);
    }

    public Station(int id, String stationName, String stationListenUrl, String stationJSONUrl){
        this.id = id;
        this.stationName = stationName;
        this.stationListenUrl = stationListenUrl;
        this.stationJSONUrl = stationJSONUrl;
    }
}
