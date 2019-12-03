package WeatherReporter.PageObject;

import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.interactions.Actions;

public class HomePage {
    private WebDriver driver;
    private Actions actions;

    private By studentLink = By.linkText("Tudeng");
    private By searchField = By.cssSelector("#search-text");
    private By searchRadioButton = By.xpath("//label[input[@value='30052']]");
    private By loginHover = By.xpath("//li[contains(@class, 'active_language')]");
    private By submitButton = By.xpath("//button[@type='submit']");
    private By sisLink = By.linkText("ÕIS");
    private By moodleLink = By.linkText("Moodle");
    private By intranetLink = By.linkText("Siseveeb");

    public HomePage(WebDriver driver) {
        this.driver = driver;
    }

    public void clickStudentLink(){
        driver.findElement(studentLink).click();
    }

    public void clickSearchField(){
        driver.findElement(searchField).click();
    }

    public void selectSecondRadioButton() {
        driver.findElement(searchRadioButton).click();
    }

    public void enterSearchText(String text) {
        driver.findElement(searchField).sendKeys(text);
    }

    public void clickSubmitButton() {
        driver.findElement(submitButton).click();
    }

    public void hoverLoginDiv(){
        WebElement loginHoverDiv = driver.findElements(loginHover).get(1);
        actions = new Actions(driver);
        actions.moveToElement(loginHoverDiv).perform();
    }

    public void clickSisLink() {
        driver.findElement(sisLink).click();
    }

    public void clickMoodleLink() {
        driver.findElement(moodleLink).click();
    }

    public void clickIntranetLink() {
        driver.findElement(intranetLink).click();
    }
}
