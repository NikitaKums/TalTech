package ui.automation.bonus.exercise.page_object;

import org.openqa.selenium.By;
import org.openqa.selenium.ElementNotVisibleException;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;

public class FormAuthenticationPage {
    private WebDriver driver;

    private By usernameField = By.id("username");
    private By passwordField = By.id("password");
    private By submitButton = By.xpath("//button[@type='submit']");
    private By logoutButton = By.xpath("//a[@href='/logout']");
    private By successNotification = By.cssSelector(".flash.success");
    private By errorNotification = By.cssSelector(".flash.error");

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

    public void clickLogout() {
        driver.findElement(logoutButton).click();
    }

    public boolean isSuccessLogoutNotificationDisplayed() {
        try {
            WebElement element = driver.findElement(successNotification);
            return element.getText().contains("You logged out of the secure area!");
        } catch (ElementNotVisibleException e) {
            // log exception
            return false;
        }
    }

    public boolean isSuccessLoginNotificationDisplayed() {
        try {
            WebElement element = driver.findElement(successNotification);
            return element.getText().contains("You logged into a secure area!");
        } catch (ElementNotVisibleException e) {
            // log exception
            return false;
        }
    }

    public boolean isErrorForInvalidUsernameNotificationDisplayed() {
        try {
            WebElement element = driver.findElement(errorNotification);
            return element.getText().contains("Your username is invalid!");
        } catch (ElementNotVisibleException e) {
            // log exception
            return false;
        }
    }

    public boolean isErrorForInvalidPasswordNotificationDisplayed() {
        try {
            WebElement element = driver.findElement(errorNotification);
            return element.getText().contains("Your password is invalid!");
        } catch (ElementNotVisibleException e) {
            // log exception
            return false;
        }
    }
}
