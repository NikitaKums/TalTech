package WeatherReporter.PageObject;

import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;

public class SearchPage {

    private WebDriver driver;

    private By emailElement = By.xpath("//a[text()='german.mumma@taltech.ee']");

    public SearchPage(WebDriver driver){
        this.driver = driver;
    }

    public boolean isCorrectEmailShown(){
        return driver.findElement(emailElement).getText() != null;
    }
}
