package com.example.radio2019;

public final class C {
    static private final String prefix = "com.itcollege.radio2019.";
    static public final String SERVICE_MEDIASOURCE_KEY = prefix + "SERVICE_MEDIASOURCE_KEY";
    static public final String SERVICE_MEDIASROUCE_KEY_JSON = prefix + "SERVICE_MEDIASROUCE_KEY_JSON";

    // Mediaplayer statuses
    static public final int MUSICSERVICE_STOPPED = 0;
    static public final int MUSICSERVICE_BUFFERING = 1;
    static public final int MUSICSERVICE_PLAYING = 2;

    // save instance in statisticsActivity
    static public final String SEARCH_DATETIME_DATA = "SEARCH_DATETIME_DATA";
    static public final String SEARCH_STATION_DATA = "SEARCH_STATION_DATA";
    static public final String STATION_CHANGE = "STATION_CHANGE";
    static public final String PREVIOUS_STATION_ID = "PREVIOUS_STATION_ID";
    static public final String PREVIOUS_TRACK_TITLE = "PREVIOUS_TRACK_TITLE";
    static public final String MUSIC_SERVICE_UI = "MUSIC_SERVICE_UI";
    static public final String STATION_ID = "STATION_ID";

    // datetime database search values
    static public final String SELECT_ALL = "SELECT ALL";
    static public final String SELECT_LAST_1_HOUR = "1 HOUR";
    static public final String SELECT_LAST_12_HOURS = "12 HOURS";
    static public final String SELECT_LAST_DAY = "LAST DAY";

    // Mediaplayer to Activity broadcast intent messages
    static public final String MUSICSERVICE_INTENT_PLAYING = prefix + "MUSICSERVICE_INTENT_PLAYING";
    static public final String MUSICSERVICE_INTENT_BUFFERING = prefix + "MUSICSERVICE_INTENT_BUFFERING";
    static public final String MUSICSERVICE_INTENT_STOPPED = prefix + "MUSICSERVICE_INTENT_STOPPED";
    static public final String MUSICSERVICE_INTENT_SONGINFO = prefix + "MUSICSERVICE_INTENT_SONGINFO";
    static public final String MUSICSERVICE_ARTIST = prefix + "MUSICSERVICE_ARTIST";
    static public final String MUSICSERVICE_TRACKTITLE = prefix + "MUSICSERVICE_TRACKTITLE";

    // Activity to Mediaplayer broadcast intent messages
    static public final String ACTIVITY_INTENT_STARTMUSIC = prefix + "ACTIVITY_INTENT_STARTMUSIC";
    static public final String ACTIVITY_INTENT_STOPPMUSIC = prefix + "ACTIVITY_INTENT_STOPPMUSIC";

    // Main activity PLAY button labels
    static public final String BUTTONCONTROLMUSIC_LABEL_PLAYING = "STOP";
    static public final String BUTTONCONTROLMUSIC_LABEL_BUFFERING = "BUFFERING";
    static public final String BUTTONCONTROLMUSIC_LABEL_STOPPED = "PLAY";

    // Volume
    static public final String MUSICPLAYER_VOLUME_KEY = "MUSICPLAYER_VOLUME_KEY";

    static public final String MUSICSERVICE_VOLLEYTAG = prefix + "MUSICSERVICE_VOLLEYTAG";

    static public final String PLAYING_RADIO_ID = "PLAYING_RADIO_ID";

}
