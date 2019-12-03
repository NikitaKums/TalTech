package WeatherReporter.WeatherMaster;

import WeatherReporter.WeatherMaster.api.WeatherApi;
import WeatherReporter.WeatherMaster.api.response.ApiWeather;
import WeatherReporter.WeatherMaster.api.response.ApiWeatherReport;
import WeatherReporter.WeatherMaster.domain.*;
import WeatherReporter.WeatherMaster.exception.CityNameMissingException;
import WeatherReporter.WeatherMaster.exception.CityNotFoundException;

import java.math.BigDecimal;
import java.math.RoundingMode;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.List;

public class WeatherMaster {
    private WeatherApi weatherApi;

    public WeatherMaster(WeatherApi weatherApi){
        this.weatherApi = weatherApi;
    }

    public WeatherReport getWeatherInformation(String city) throws CityNotFoundException, CityNameMissingException {
        ApiWeatherReport apiWeatherReport = weatherApi.getWeatherInformationFromApi(city);
        if (apiWeatherReport.getCity() == null && apiWeatherReport.getList() == null){
            throw new CityNotFoundException("City with name '" + city + "' was not found");
        }
        return getWeatherReport(apiWeatherReport);
    }

    private WeatherReport getWeatherReport(ApiWeatherReport apiWeatherReport){
        WeatherReport weatherReport = new WeatherReport();

        weatherReport.setWeatherReportDetails(getWeatherReportDetails(apiWeatherReport));

        if (apiWeatherReport.getList() != null && apiWeatherReport.getList().size() > 0){
            weatherReport.setCurrentWeatherReport(getCurrentWeatherReport(apiWeatherReport.getList().get(0)));
        }
        weatherReport.setForecastReport(getForecastReport(apiWeatherReport));

        return weatherReport;
    }

    private List<ForecastReport> getForecastReport(ApiWeatherReport apiWeatherReport) {
        List<ForecastReport> forecastReports = new ArrayList<>();
        ForecastDetails forecastDetails;
        ForecastReport forecastReport;

        int numberOfDaysToAdd = 1;
        Calendar calendar = Calendar.getInstance();
        Date currentDate = calendar.getTime();

        for (ApiWeather apiWeatherForecast : apiWeatherReport.getList()){

            calendar.setTimeInMillis(apiWeatherForecast.getDt() * 1000);

            if (calendar.getTime().after(getDateInAmountOfDays(numberOfDaysToAdd, currentDate))){
                forecastDetails = new ForecastDetails();
                forecastReport = new ForecastReport();

                forecastDetails.setTemperature(apiWeatherForecast.getMain().getTemp());
                forecastDetails.setMin_temperature(apiWeatherForecast.getMain().getTemp_min());
                forecastDetails.setMax_temperature(apiWeatherForecast.getMain().getTemp_max());
                forecastDetails.setHumidity(apiWeatherForecast.getMain().getHumidity());
                forecastDetails.setPressure(apiWeatherForecast.getMain().getPressure());
                forecastReport.setDate(apiWeatherForecast.getDt_txt());

                forecastReport.setWeather(forecastDetails);
                forecastReports.add(forecastReport);

                numberOfDaysToAdd += 1;
            }
            if (numberOfDaysToAdd > 3){
                break;
            }
        }

        return forecastReports;
    }

    private CurrentWeatherReport getCurrentWeatherReport(ApiWeather apiWeather) {
        CurrentWeatherReport currentWeatherReport = new CurrentWeatherReport();

        currentWeatherReport.setTemperature(apiWeather.getMain().getTemp());
        currentWeatherReport.setMin_temperature(apiWeather.getMain().getTemp_min());
        currentWeatherReport.setMax_temperature(apiWeather.getMain().getTemp_max());
        currentWeatherReport.setHumidity(apiWeather.getMain().getHumidity());
        currentWeatherReport.setPressure(apiWeather.getMain().getPressure());

        return currentWeatherReport;
    }

    private WeatherReportDetails getWeatherReportDetails(ApiWeatherReport apiWeatherReport) {
        WeatherReportDetails weatherReportDetails = new WeatherReportDetails();

        weatherReportDetails.setCity(apiWeatherReport.getCity().getName());
        weatherReportDetails.setCoordinates(
                round(apiWeatherReport.getCity().getCoord().getLat(), 2) + ","
                        + round(apiWeatherReport.getCity().getCoord().getLon(), 2));
        weatherReportDetails.setTemperatureUnit("Celsius");

        return weatherReportDetails;
    }

    private Date getDateInAmountOfDays(int days, Date currentDate){
        return new Date(currentDate.getTime() + days * 24 * 60 * 60 * 1000);
    }

    private static double round(double value, int places) {
        if (places < 0) throw new IllegalArgumentException();

        BigDecimal bd = BigDecimal.valueOf(value);
        bd = bd.setScale(places, RoundingMode.HALF_UP);
        return bd.doubleValue();
    }
}
