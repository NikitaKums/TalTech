package WeatherReporter.WeatherMaster.api;

import WeatherReporter.WeatherMaster.api.response.ApiWeatherReport;
import WeatherReporter.WeatherMaster.exception.CityNameMissingException;
import com.sun.jersey.api.client.Client;
import com.sun.jersey.api.client.ClientResponse;
import com.sun.jersey.api.client.config.ClientConfig;
import com.sun.jersey.api.client.config.DefaultClientConfig;
import org.codehaus.jackson.jaxrs.JacksonJaxbJsonProvider;

import static com.sun.jersey.api.client.Client.create;
import static com.sun.jersey.api.json.JSONConfiguration.FEATURE_POJO_MAPPING;
import static java.lang.Boolean.TRUE;

public class WeatherApi {
    private static final String BASE_URL = "http://api.openweathermap.org/data/2.5";
    private static final String APPID = "59a7bcfbd8be3e2b49e2be4278e31a47";
    private static Client client = getConfiguredClient();

    public ApiWeatherReport getWeatherInformationFromApi(String city) throws CityNameMissingException {
        if (city.isEmpty()){
            throw new CityNameMissingException("City/town name must be provided");
        } else {
            return getClientResponse(city).getEntity(ApiWeatherReport.class);
        }
    }

    private ClientResponse getClientResponse(String cityName) {
        String resourceUrl = BASE_URL + "/forecast";
        return client.resource(resourceUrl)
                .queryParam("q", cityName)
                .queryParam("APPID", APPID)
                .queryParam("units", "metric")
                .get(ClientResponse.class);
    }

    private static Client getConfiguredClient() {
        ClientConfig config = new DefaultClientConfig();
        config.getClasses().add(JacksonJaxbJsonProvider.class);
        config.getFeatures().put(FEATURE_POJO_MAPPING, TRUE);
        return create(config);
    }
}
