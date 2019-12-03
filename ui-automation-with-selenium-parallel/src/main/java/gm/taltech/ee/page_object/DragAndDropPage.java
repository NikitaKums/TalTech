package gm.taltech.ee.page_object;

import org.apache.commons.io.IOUtils;
import org.openqa.selenium.By;
import org.openqa.selenium.JavascriptExecutor;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;

import java.io.IOException;
import java.net.URL;

import static java.lang.Thread.currentThread;
import static java.nio.charset.Charset.defaultCharset;
import static java.util.Objects.requireNonNull;

public class DragAndDropPage {

    private WebDriver driver;
    private By blockA = By.id("column-a");
    private By blockB = By.id("column-b");
    private By footerDiv = By.id("page-footer");

    public DragAndDropPage(WebDriver driver) {
        this.driver = driver;
    }

    public void dragAonTopOfB() {
        WebElement blockAElement = driver.findElement(blockA);
        WebElement blockBElement = driver.findElement(blockB);

        dragAndDrop(blockAElement, blockBElement);
    }

    private void dragAndDrop(WebElement source, WebElement target) {
        URL url = currentThread().getContextClassLoader().getResource("DragAndDrop.js");
        try {
            String script = IOUtils.toString(requireNonNull(url), defaultCharset());
            script += "simulateHTML5DragAndDrop(arguments[0], arguments[1])";
            JavascriptExecutor executor = (JavascriptExecutor) driver;
            executor.executeScript(script, source, target);
        } catch (IOException e) {
            throw new RuntimeException(e);
        }
    }


    public boolean isTheFirstElementsHeaderText(String headerText) {
        System.out.println(driver.findElement(blockA).findElement(By.tagName("header")).getText());
        return driver.findElement(blockA).findElement(By.tagName("header")).getText().equals(headerText);
    }

    public void dragAonTopOfFooter() {
        WebElement blockAElement = driver.findElement(blockA);
        WebElement footerElement = driver.findElement(footerDiv);

        dragAndDrop(blockAElement, footerElement);
    }
}
