# Nikita Kums - IADB179097

#### Project stages:
Each branch represents each stage.

|  Stage  	|                Description                	|
|:-------:	|:-----------------------------------------:	|
| Stage 1 	|      All possible tests to begin TDD.     	|
| Stage 2 	|  Implement application for tests to pass. 	|
| Stage 3 	|            Implement file I/O.            	|
| Stage 4 	|      Mock/Stub tests for API and I/O.     	|
| Stage 5 	| Integration tests for OpenWeatherMap API. 	|
| Stage 6 	|      UI automation tests for ttu.ee.      	|

### Requirements:
- Java 11 or higher

## Testing strategy

The approach to this project is TDD aka Test Driven Development. Before implementing the application, required tests
are created which indicate the functionality that the application must provide.
Given approach has several benefits:
- Development time saving
- Increased confidence in the code
- Provides an overview of code readiness

Also close attention is paid to Clean Code requirements/best practices.

## File I/O | Application exection

The names of cities must be written to file named ```input.json``` which is located in ```resources``` directory/folder.
There is an example provided in the file as how the city names must be entered.

```
{
  "cities" : [ {
    "name" : "Moscow"
  }, {
    "name" : "Tallinn"
  }, {
    "name" : "New York"
  } ]
}
```

Example:
```
fileMaster.writeToFile(weatherMaster.getWeatherInformation("Tallinn"), "WeatherReportResults");
```
File named ```Tallinn.json``` will be added to ```WeatherReportResults``` folder. 
## Domain schema
![alt text](https://i.imgur.com/OgA9PZy.png)
