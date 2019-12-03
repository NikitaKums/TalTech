package WeatherReporter.FileMaster;

import WeatherReporter.FileMaster.model.City;
import WeatherReporter.FileMaster.model.CityWrapper;
import WeatherReporter.WeatherMaster.domain.WeatherReport;
import WeatherReporter.WeatherMaster.domain.WeatherReportDetails;
import org.apache.commons.io.FileUtils;
import org.junit.After;
import org.junit.Before;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.mockito.Mock;
import org.mockito.junit.MockitoJUnitRunner;

import java.io.File;
import java.io.IOException;
import java.io.InputStream;
import java.util.ArrayList;
import java.util.List;

import static org.junit.Assert.assertTrue;
import static org.mockito.Mockito.*;

@RunWith(MockitoJUnitRunner.class)
public class FileMasterTest {
    private FileMaster fileMaster;
    private CityWrapper cityWrapper;
    private InputStream inputStream;
    private WeatherReport weatherReport;
    private City apiCity;
    private List<City> cities;
    private WeatherReportDetails weatherReportDetails;
    private String directoryName = "TestWeatherReport";

    @Mock
    private FileMaster fileMasterMock;

    @Before
    public void setup(){
        fileMaster = new FileMaster();
        weatherReport = new WeatherReport();
        weatherReportDetails = new WeatherReportDetails();
        cityWrapper = new CityWrapper();
        apiCity = new City();
        cities = new ArrayList<>();
        inputStream = FileMasterTest.class.getClassLoader().getResourceAsStream("testCitiesFile.json");
    }

    @After
    public void cleanup(){
        File file = new File(directoryName);
        try {
            if (file.exists()){
                FileUtils.cleanDirectory(file);
                FileUtils.deleteDirectory(file);
            }
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    @Test
    public void shouldBeAbleToGetCitiesFromFile(){
        cityWrapper = fileMaster.getCitiesFromFile(inputStream);
        boolean citiesPresent = false;
        if (cityWrapper.getCities().get(0).getName().equals("Moscow") &&
                cityWrapper.getCities().get(1).getName().equals("Tartu") &&
                cityWrapper.getCities().get(2).getName().equals("New York")) {
            citiesPresent = true;
        }
        assertTrue("Should be able to get all city names from file", citiesPresent);
    }

    @Test
    public void shouldCreateDirectoryForWeatherReportResults(){
        weatherReportDetails.setCity("city_name");
        weatherReport.setWeatherReportDetails(weatherReportDetails);
        fileMaster.writeToFile(weatherReport, directoryName);
        File file = new File(directoryName);
        assertTrue("Should create a directory to store weather report files into", file.isDirectory());
    }

    @Test
    public void shouldCreateFilesForAllCities(){
        cityWrapper = fileMaster.getCitiesFromFile(inputStream);

        for (City tempCity : cityWrapper.getCities()){
            weatherReportDetails.setCity(tempCity.getName());
            weatherReport.setWeatherReportDetails(weatherReportDetails);
            fileMaster.writeToFile(weatherReport, directoryName);
        }

        File file = new File(directoryName + "/Moscow.json");
        assertTrue("Could not find file named Moscow.json", file.exists());

        file = new File(directoryName + "/New_York.json");
        assertTrue("Could not find file named New_York.json", file.exists());

        file = new File(directoryName + "/Tartu.json");
        assertTrue("Could not find file named Tartu.json", file.exists());
    }

    // Mock
    @Test
    public void shouldGetAllCitiesFromFileInSingleRead(){
        when(fileMasterMock.getCitiesFromFile(any(InputStream.class))).thenReturn(getCityWrapperForMocking());

        cityWrapper = fileMasterMock.getCitiesFromFile(inputStream);

        verify(fileMasterMock, times(1)).getCitiesFromFile(any(InputStream.class));
    }

    // Stub
    @Test
    public void shouldHaveCityNameAsFileName(){
        apiCity.setName("Moscow");
        cities.add(apiCity);
        cityWrapper.setCities(cities);

        when(fileMasterMock.getCitiesFromFile(any(InputStream.class))).thenReturn(cityWrapper);
        cityWrapper = fileMasterMock.getCitiesFromFile(inputStream);
        weatherReportDetails.setCity(cityWrapper.getCities().get(0).getName());
        weatherReport.setWeatherReportDetails(weatherReportDetails);

        fileMaster.writeToFile(weatherReport, directoryName);
        File file = new File(directoryName + "/Moscow.json");

        assertTrue("Should write to file named <city>.json", file.exists());
    }

    private CityWrapper getCityWrapperForMocking(){
        List<City> tempCities = new ArrayList<>();

        City apiCity1 = new City();
        apiCity1.setName("Moscow");
        tempCities.add(apiCity1);

        City apiCity2 = new City();
        apiCity2.setName("Tallinn");
        tempCities.add(apiCity2);

        City apiCity3 = new City();
        apiCity3.setName("Tartu");
        tempCities.add(apiCity3);

        CityWrapper tempCityWrapper = new CityWrapper();
        tempCityWrapper.setCities(tempCities);

        return tempCityWrapper;
    }

}
