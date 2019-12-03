package WeatherReporter;

import WeatherReporter.FileMaster.FileMaster;
import WeatherReporter.FileMaster.model.City;
import WeatherReporter.FileMaster.model.CityWrapper;
import WeatherReporter.WeatherMaster.WeatherMaster;
import WeatherReporter.WeatherMaster.api.WeatherApi;
import WeatherReporter.WeatherMaster.exception.CityNameMissingException;
import WeatherReporter.WeatherMaster.exception.CityNotFoundException;

import java.io.InputStream;

public class Program {
    private static String fileName = "input.json";

    public static void main(String[] args) throws CityNameMissingException, CityNotFoundException {
        WeatherMaster weatherMaster = new WeatherMaster(new WeatherApi());
        FileMaster fileMaster = new FileMaster();

        InputStream inputStream = Program.class.getClassLoader().getResourceAsStream(fileName);

        CityWrapper cities = fileMaster.getCitiesFromFile(inputStream);

        for (City city : cities.getCities()){
            fileMaster.writeToFile(weatherMaster.getWeatherInformation(city.getName()), "WeatherReportResults");
        }
    }
}
