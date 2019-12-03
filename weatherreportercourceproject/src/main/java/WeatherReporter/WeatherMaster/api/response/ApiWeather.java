package WeatherReporter.WeatherMaster.api.response;

import lombok.Data;
import org.codehaus.jackson.annotate.JsonIgnoreProperties;


@Data
@JsonIgnoreProperties(ignoreUnknown = true)
public class ApiWeather {
    private long dt;
    private String dt_txt;
    private ApiWeatherDetails main;
}
