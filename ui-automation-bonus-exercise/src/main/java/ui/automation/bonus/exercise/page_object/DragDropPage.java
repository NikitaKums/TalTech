package ui.automation.bonus.exercise.page_object;

import com.google.common.base.Charsets;
import org.apache.commons.io.IOUtils;
import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.JavascriptExecutor;

import java.io.IOException;
import java.net.URL;

public class DragDropPage {
    private WebDriver driver;

    private By columnA = By.id("column-a");
    private By columnB = By.id("column-b");
    private By footerDiv = By.id("page-footer");

    public DragDropPage(WebDriver driver){
        this.driver = driver;
    }

    public void dragAndDropOnTop(){
        dragAndDrop(columnA, columnB);
    }

    public void dragAndDropNotOnTop(){
        dragAndDrop(columnA, footerDiv);
    }

    private void dragAndDrop(By source, By target){
        URL url = Thread.currentThread().getContextClassLoader().getResource("DragAndDrop.js");
        try {
            String script = IOUtils.toString(url, Charsets.UTF_8);
            script += "simulateHTML5DragAndDrop(arguments[0], arguments[1])";
            JavascriptExecutor executor = (JavascriptExecutor) driver;
            executor.executeScript(script, driver.findElement(source), driver.findElement(target));
        } catch (IOException e) {
            throw new RuntimeException(e);
        }
    }

    public boolean didColumnsSwitchPositions(){
        return driver.findElement(columnA).getText().equals("B") &&
                driver.findElement(columnB).getText().equals("A");
    }

    public boolean didColumnsRemainAtInitialPositions(){
        return driver.findElement(columnA).getText().equals("A") &&
                driver.findElement(columnB).getText().equals("B");
    }
}
