package WeatherReporter.WeatherMaster.api.response;

import lombok.Data;
import org.codehaus.jackson.annotate.JsonIgnoreProperties;

@Data
@JsonIgnoreProperties(ignoreUnknown = true)
public class ApiWeatherDetails {
    private int temp;
    private double temp_min;
    private double temp_max;
    private int pressure;
    private int humidity;
}
