package WeatherReporter.Integration;

import io.restassured.RestAssured;
import io.restassured.specification.RequestSpecification;
import org.junit.Before;
import org.junit.Test;

import static io.restassured.RestAssured.given;
import static org.hamcrest.CoreMatchers.*;
import static org.hamcrest.Matchers.greaterThan;

public class OpenWeatherMapTest {
    private static final String APPID = "59a7bcfbd8be3e2b49e2be4278e31a47";
    private RequestSpecification requestSpecification;
    private String city;

    @Before
    public void setUp() {
        city = "Moscow";
        RestAssured.baseURI = "http://api.openweathermap.org/data/2.5";
        requestSpecification = given()
                .param("units", "metric")
                .param("APPID", APPID);
    }

    @Test
    public void shouldReturnStatusCode200ForValidRequest() {
        requestSpecification
                .param("q", city)
                .when()
                .get("/forecast")
                .then()
                .statusCode(200);
    }

    @Test
    public void shouldReturnStatusCode404ForInvalidRequest() {
        requestSpecification
                .param("q", "this city exists 100%")
                .when()
                .get("/forecast")
                .then()
                .statusCode(404)
                .body("message", equalTo("city not found"));
    }

    @Test
    public void shouldHaveCityNameAndCoordinatesInResponse(){
        requestSpecification
                .param("q", city)
                .when()
                .get("/forecast")
                .then()
                .body("city.name", equalTo(city))
                .body("city.coord.lat", equalTo(55.7507f))
                .body("city.coord.lon", equalTo(37.6177f));
    }

    @Test
    public void shouldHaveTemperatureInResponse() {
        requestSpecification
                .param("q", city)
                .when()
                .get("/forecast")
                .then()
                .assertThat()
                .body("list[0].main.temp", instanceOf(Number.class))
                .body("list[0].main.temp_max", instanceOf(Number.class))
                .body("list[0].main.temp_min", instanceOf(Number.class));
    }

    @Test
    public void shouldHaveHumidityInResponse() {
        requestSpecification
                .param("q", city)
                .when()
                .get("/forecast")
                .then()
                .assertThat()
                .body("list[0].main.humidity", instanceOf(Number.class));
    }

    @Test
    public void shouldHavePressureInResponse() {
        requestSpecification
                .param("q", city)
                .when()
                .get("/forecast")
                .then()
                .assertThat()
                .body("list[0].main.pressure", instanceOf(Number.class));
    }


    @Test
    public void shouldHaveNotOlderThanTodayDateInResponse(){
        long time = System.currentTimeMillis() / 1000L;
        requestSpecification
                .param("q", city)
                .when()
                .get("/forecast")
                .then()
                .assertThat()
                .body("list[0].dt", greaterThan((int)time));
    }


}
