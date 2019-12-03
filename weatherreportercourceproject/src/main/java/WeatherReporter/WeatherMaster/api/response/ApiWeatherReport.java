package WeatherReporter.WeatherMaster.api.response;

import lombok.Data;
import org.codehaus.jackson.annotate.JsonIgnoreProperties;

import java.util.List;

@Data
@JsonIgnoreProperties(ignoreUnknown = true)
public class ApiWeatherReport {
    private City city;
    private List<ApiWeather> list;
}
