package WeatherReporter.PageObject;

import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;

public class StudentPage {
    private WebDriver driver;

    private By educationInformationLink = By.xpath("//a[contains(@href, '/tudengile/oppeinfo/') and contains(text(), 'Õppeinfo')]");
    private By educationInformationHeading = By.xpath("//h2[text()='Kvaliteetne haridus']");

    public StudentPage(WebDriver driver) {
        this.driver = driver;
    }

    public void clickEducationInformationLink(){
        driver.findElement(educationInformationLink).click();
    }

    public boolean isEducationInformationHeadingShown(){
        return driver.findElement(educationInformationHeading) != null;
    }
}
