package WeatherReporter.WeatherMaster.domain;

import lombok.Data;

import java.util.List;

@Data
public class WeatherReport {
    private WeatherReportDetails weatherReportDetails;
    private CurrentWeatherReport currentWeatherReport;
    List<ForecastReport> forecastReport;
}
