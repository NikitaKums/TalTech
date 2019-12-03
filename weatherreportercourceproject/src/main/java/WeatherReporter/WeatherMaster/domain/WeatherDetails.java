package WeatherReporter.WeatherMaster.domain;

import lombok.Data;

@Data
public class WeatherDetails {
    private int temperature;
    private double min_temperature;
    private double max_temperature;
    private int humidity;
    private int pressure;
}
