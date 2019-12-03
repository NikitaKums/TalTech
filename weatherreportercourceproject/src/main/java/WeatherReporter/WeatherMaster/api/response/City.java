package WeatherReporter.WeatherMaster.api.response;

import lombok.Data;
import org.codehaus.jackson.annotate.JsonIgnoreProperties;

@Data
@JsonIgnoreProperties(ignoreUnknown = true)
public class City {
    private String name;
    private Coord coord;
}
