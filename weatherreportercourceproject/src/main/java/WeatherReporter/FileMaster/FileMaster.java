package WeatherReporter.FileMaster;

import WeatherReporter.FileMaster.model.CityWrapper;
import WeatherReporter.WeatherMaster.domain.WeatherReport;
import org.codehaus.jackson.map.ObjectMapper;

import java.io.File;
import java.io.IOException;
import java.io.InputStream;

public class FileMaster {

    private static ObjectMapper objectMapper = new ObjectMapper();

    public CityWrapper getCitiesFromFile(InputStream inputStream){
        try {
            return new ObjectMapper().readValue(inputStream, CityWrapper.class);
        } catch (IOException e) {
            throw new RuntimeException("Unable to read from file " + e);
        }
    }

    public void writeToFile(WeatherReport weatherReport, String directoryName) {
        if (directoryName == null || directoryName.trim().isEmpty()){
            directoryName = "WeatherReport";
        }
        createDirectoryIfNotExists(directoryName);
        String fileName = directoryName.concat("/") + weatherReport.getWeatherReportDetails().getCity().replaceAll("\\s+", "_").concat(".json");
        try {
            objectMapper.writerWithDefaultPrettyPrinter().writeValue(new File(fileName), weatherReport);
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    private void createDirectoryIfNotExists(String directoryName){
        File directory = new File(directoryName);
        if (!directory.exists()){
            directory.mkdirs();
        }
    }
}