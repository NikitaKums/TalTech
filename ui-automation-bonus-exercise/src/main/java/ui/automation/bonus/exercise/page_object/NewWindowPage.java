package ui.automation.bonus.exercise.page_object;

import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;

public class NewWindowPage {
    private WebDriver driver;

    private By clickHereLink = By.linkText("Click Here");
    private String textToVerify = "";

    public NewWindowPage(WebDriver driver){
        this.driver = driver;
    }

    public void clickClickHereLink() {
        driver.findElement(clickHereLink).click();
    }

    public void switchToWindow(String window) {
        driver.switchTo().window(window);
    }

    public void closeWindow(){
        driver.close();
    }

    public void captureTextToVerify(){
        textToVerify = driver.findElement(By.tagName("h3")).getText();
    }

    public boolean isResultTextValid(){
        return textToVerify.equals("New Window");
    }
}
