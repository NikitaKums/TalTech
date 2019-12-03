package WeatherReporter.WeatherMaster.api;

import WeatherReporter.WeatherMaster.api.response.ApiWeatherReport;
import WeatherReporter.WeatherMaster.exception.CityNameMissingException;
import org.junit.Before;
import org.junit.Test;

import java.util.Calendar;
import java.util.Date;

import static org.junit.Assert.assertEquals;
import static org.junit.Assert.assertTrue;

public class WeatherApiTest {
    private ApiWeatherReport apiWeatherReport;
    private WeatherApi weatherApi;
    private String city;

    @Before
    public void setup(){
        weatherApi = new WeatherApi();
        city = "Moscow";
    }


    @Test(expected = CityNameMissingException.class)
    public void shouldThrowExceptionForEmptyCityName() throws CityNameMissingException {
        weatherApi.getWeatherInformationFromApi("");
    }

    @Test
    public void shouldContainCityNameInResult() throws CityNameMissingException {
        apiWeatherReport = weatherApi.getWeatherInformationFromApi(city);

        assertEquals("Moscow", apiWeatherReport.getCity().getName());
    }

    @Test
    public void shouldContainRightCoordinatesInResult() throws CityNameMissingException{
        apiWeatherReport = weatherApi.getWeatherInformationFromApi(city);

        assertEquals("Latitude does not match expected", "55.7507", apiWeatherReport.getCity().getCoord().getLat().toString());
        assertEquals("Longitude does not match expected", "37.6177", apiWeatherReport.getCity().getCoord().getLon().toString());

    }

    @Test
    public void shouldContainWeatherDataInResult() throws CityNameMissingException {
        apiWeatherReport = weatherApi.getWeatherInformationFromApi(city);
        assertTrue(apiWeatherReport.getList().size() > 0);
    }

    @Test
    public void shouldReturnNotOlderThanTodayForecast() throws CityNameMissingException {
        Calendar calendar = Calendar.getInstance();
        Date currentDate = calendar.getTime();
        apiWeatherReport = weatherApi.getWeatherInformationFromApi(city);
        calendar.setTimeInMillis(apiWeatherReport.getList().get(0).getDt() * 1000);
        assertTrue(calendar.getTime().after(currentDate));
    }
}
