package WeatherReporter.WeatherMaster.domain;

import lombok.Data;

@Data
public class ForecastReport {
    private String date;
    ForecastDetails weather;
}
