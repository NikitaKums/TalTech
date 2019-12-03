package ui.automation.bonus.exercise.page_object;

import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;

public class HomePage {
    private WebDriver driver;

    private By formAuthenticationLink = By.linkText("Form Authentication");
    private By newWindowLink = By.linkText("Multiple Windows");
    private By dragAndDropLink = By.linkText("Drag and Drop");

    public HomePage(WebDriver driver){
        this.driver = driver;
    }

    public void clickFormAuthenticationLink() {
        driver.findElement(formAuthenticationLink).click();
    }

    public void clickNewWindowLink(){
        driver.findElement(newWindowLink).click();
    }

    public void clickDragAndDropLink(){
        driver.findElement(dragAndDropLink).click();
    }
}
