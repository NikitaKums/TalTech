package gm.taltech.ee.page_object;

import org.openqa.selenium.By;
import org.openqa.selenium.ElementNotVisibleException;
import org.openqa.selenium.WebDriver;

public class FormAuthenticationPage {

    private By usernameField = By.id("username");
    private By passwordField = By.id("password");
    private By submitButton = By.xpath("//button[@type='submit']");
    private By successNotification = By.cssSelector(".flash.success");
    private By errorNotification = By.cssSelector(".flash.error");

    private WebDriver driver;

    public FormAuthenticationPage(WebDriver driver) {
        this.driver = driver;
    }

    public void enterUsername(String username) {
        driver.findElement(usernameField).sendKeys(username);
    }

    public void enterPassword(String password) {
        driver.findElement(passwordField).sendKeys(password);
    }

    public void clickSubmit() {
        driver.findElement(submitButton).click();
    }

    public boolean isSuccessNotificationDisplayed() {
        try {
            driver.findElement(successNotification);
            return true;
        } catch (ElementNotVisibleException e) {
            // log exception
            return false;
        }
    }

    public boolean isErrorNotificationDisplayed() {
        try {
            driver.findElement(errorNotification);
            return true;
        } catch (ElementNotVisibleException e) {
            // log exception
            return false;
        }
    }
}
