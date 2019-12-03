package WeatherReporter.PageObject;

import org.openqa.selenium.WebDriver;

public abstract class Page {
    private WebDriver driver;

    private String urlFromPage;

    public Page(WebDriver driver){
        this.driver = driver;
    }

    public void captureUrl(){
        urlFromPage = driver.getCurrentUrl();
    }

    public boolean doesUrlMatch(String url){
        if (urlFromPage != null){
            return urlFromPage.equals(url);
        }
        return false;
    }
}
