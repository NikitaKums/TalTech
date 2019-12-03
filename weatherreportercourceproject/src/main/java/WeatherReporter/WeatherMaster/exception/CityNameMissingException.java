package WeatherReporter.WeatherMaster.exception;

public class CityNameMissingException extends Exception {
    public CityNameMissingException(String s){
        super(s);
    }
}