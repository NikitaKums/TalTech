package WeatherReporter.WeatherMaster.api.response;

import lombok.Data;
import org.codehaus.jackson.annotate.JsonIgnoreProperties;

@Data
@JsonIgnoreProperties(ignoreUnknown = true)
public class Coord {
    private Double lat;
    private Double lon;
}
