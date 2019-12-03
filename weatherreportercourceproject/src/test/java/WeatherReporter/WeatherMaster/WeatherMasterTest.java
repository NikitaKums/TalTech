package WeatherReporter.WeatherMaster;

import WeatherReporter.WeatherMaster.api.WeatherApi;
import WeatherReporter.WeatherMaster.api.response.*;
import WeatherReporter.WeatherMaster.domain.ForecastReport;
import WeatherReporter.WeatherMaster.domain.WeatherReport;
import WeatherReporter.WeatherMaster.exception.CityNameMissingException;
import WeatherReporter.WeatherMaster.exception.CityNotFoundException;
import org.codehaus.jackson.map.ObjectMapper;
import org.junit.Before;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.mockito.Mock;
import org.mockito.junit.MockitoJUnitRunner;

import java.io.IOException;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.concurrent.TimeUnit;

import static org.junit.Assert.*;
import static org.mockito.ArgumentMatchers.anyString;
import static org.mockito.Mockito.*;

@RunWith(MockitoJUnitRunner.class)
public class WeatherMasterTest {
    private WeatherMaster weatherMaster;
    private WeatherReport weatherReport;
    private String city;

    @Mock
    private WeatherApi weatherApiMock;
    private WeatherMaster weatherMasterWithMock;

    @Before
    public void setup(){
        weatherMaster = new WeatherMaster(new WeatherApi());
        city = "Moscow";
    }

    @Test(expected = CityNotFoundException.class)
    public void shouldThrowExceptionForInvalidCityName() throws CityNotFoundException, CityNameMissingException {
        weatherMaster.getWeatherInformation("this_city_definitely_exists");
    }

    @Test
    public void shouldContainCityNameInResponse() throws CityNotFoundException, CityNameMissingException {
        weatherReport = weatherMaster.getWeatherInformation(city);
        String resultCity = weatherReport.getWeatherReportDetails().getCity();
        assertEquals("Moscow", resultCity);
    }

    @Test
    public void shouldContainCityCoordinatesInLatLonFormat() throws CityNotFoundException, CityNameMissingException {
        weatherReport = weatherMaster.getWeatherInformation(city);
        String coordinates = weatherReport.getWeatherReportDetails().getCoordinates();
        assertEquals("55.75,37.62", coordinates);
    }

    @Test
    public void shouldContainForecastReportForThreeDays() throws CityNotFoundException, CityNameMissingException {
        weatherReport = weatherMaster.getWeatherInformation(city);
        List<ForecastReport> forecastReports = weatherReport.getForecastReport();

        assertEquals(3, forecastReports.size());
    }

    @Test
    public void shouldContainDateAndWeatherDataInForecast() throws CityNotFoundException, CityNameMissingException {
        weatherReport = weatherMaster.getWeatherInformation(city);
        List<ForecastReport> forecastReports = weatherReport.getForecastReport();
        boolean requiredDataPresent = false;

        for (ForecastReport forecastReport : forecastReports){
            if (forecastReport.getDate() != null && forecastReport.getWeather() != null) {
                requiredDataPresent = true;
            } else {
                break;
            }
        }

        assertTrue("Forecast date and/or weather cannot be null", requiredDataPresent);
    }

    @Test
    public void shouldReturnTemperatureInCelsius() throws CityNotFoundException, CityNameMissingException {
        weatherReport = weatherMaster.getWeatherInformation(city);

        assertEquals("Celsius", weatherReport.getWeatherReportDetails().getTemperatureUnit());
    }

    // Mock
    @Test
    public void shouldCallWeatherApiOnceForCorrectCity() throws CityNameMissingException, CityNotFoundException {
        weatherMasterWithMock = new WeatherMaster(weatherApiMock);
        when(weatherApiMock.getWeatherInformationFromApi(anyString())).thenReturn(getApiWeatherReportForMocking());

        weatherReport = weatherMasterWithMock.getWeatherInformation(city);

        verify(weatherApiMock, times(1)).getWeatherInformationFromApi(anyString());
    }

    // Stub
    @Test
    public void shouldNotContainAnyNullFields() throws CityNameMissingException, CityNotFoundException, IOException {
        weatherMasterWithMock = new WeatherMaster(weatherApiMock);
        when(weatherApiMock.getWeatherInformationFromApi(anyString())).thenReturn(getApiWeatherReportForMocking());

        weatherReport = weatherMasterWithMock.getWeatherInformation(city);
        String weatherReportString = new ObjectMapper().writeValueAsString(weatherReport);

        assertFalse("No field should be a null", weatherReportString.contains("null"));
    }

    private ApiWeatherReport getApiWeatherReportForMocking(){
        City apiCity = new City();
        Coord coord = new Coord();
        ApiWeather apiWeather = new ApiWeather();
        ApiWeather apiWeather2 = new ApiWeather();
        ApiWeatherDetails apiWeatherDetails = new ApiWeatherDetails();
        ApiWeatherDetails apiWeatherDetails2 = new ApiWeatherDetails();
        List<ApiWeather> apiWeatherList = new ArrayList<>();
        ApiWeatherReport apiWeatherReport = new ApiWeatherReport();

        apiCity.setName(city);
        coord.setLon(37.6177);
        coord.setLat(55.7507);
        apiCity.setCoord(coord);

        apiWeather.setDt(System.currentTimeMillis() / 1000L);
        apiWeather.setDt_txt("2019-10-17 12:00:00");
        apiWeatherDetails.setHumidity(63);
        apiWeatherDetails.setPressure(1015);
        apiWeatherDetails.setTemp(19);
        apiWeatherDetails.setTemp_max(19.58);
        apiWeatherDetails.setTemp_min(15.95);
        apiWeather.setMain(apiWeatherDetails);

        apiWeather2.setDt((new Date().getTime() + TimeUnit.DAYS.toMillis(1)));
        apiWeather2.setDt_txt(apiWeather.getDt_txt());
        apiWeatherDetails2.setHumidity(apiWeatherDetails.getHumidity());
        apiWeatherDetails2.setPressure(apiWeatherDetails.getPressure());
        apiWeatherDetails2.setTemp(apiWeatherDetails.getTemp());
        apiWeatherDetails2.setTemp_max(apiWeatherDetails.getTemp_max());
        apiWeatherDetails2.setTemp_min(apiWeatherDetails.getTemp_min());
        apiWeather2.setMain(apiWeatherDetails2);

        apiWeatherList.add(apiWeather);
        apiWeatherList.add(apiWeather2);
        apiWeatherReport.setCity(apiCity);
        apiWeatherReport.setList(apiWeatherList);

        return apiWeatherReport;
    }
}
