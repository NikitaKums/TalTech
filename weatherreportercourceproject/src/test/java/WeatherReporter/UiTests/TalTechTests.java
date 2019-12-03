package WeatherReporter.UiTests;

import WeatherReporter.PageObject.*;
import io.github.bonigarcia.wdm.WebDriverManager;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.chrome.ChromeDriver;
import org.testng.annotations.AfterClass;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.BeforeMethod;
import org.testng.annotations.Test;


import java.util.List;
import java.util.Set;

import static org.hamcrest.CoreMatchers.is;
import static org.hamcrest.MatcherAssert.assertThat;

public class TalTechTests {

    private WebDriver driver;

    @BeforeClass
    public void setupDriver() {
        WebDriverManager.chromedriver().setup();
        driver = new ChromeDriver();
    }

    @BeforeMethod
    public void openDriver() {
        driver.get("https://ttu.ee/");
    }

    @AfterClass
    public void closeDriver() {
        if (driver != null) {
            driver.close();
        }
    }

    @Test
    public void shouldSeeQualityEducationUnderEducationInformationSection(){
        HomePage homePage = new HomePage(driver);
        StudentPage studentPage = new StudentPage(driver);

        homePage.clickStudentLink();
        studentPage.clickEducationInformationLink();

        assertThat(studentPage.isEducationInformationHeadingShown(), is(true));
    }

    @Test
    public void shouldHaveCorrectEmailInEmployeeSearch(){
        HomePage homePage = new HomePage(driver);
        SearchPage searchPage = new SearchPage(driver);

        homePage.clickSearchField();
        homePage.selectSecondRadioButton();
        homePage.enterSearchText("German Mumma");
        homePage.clickSubmitButton();

        assertThat(searchPage.isCorrectEmailShown(), is(true));
    }

    @Test
    public void shouldHaveCorrectSisUrlInLoginSection(){
        HomePage homePage = new HomePage(driver);
        SisPage sisPage = new SisPage(driver);

        homePage.hoverLoginDiv();
        homePage.clickSisLink();
        sisPage.captureUrl();

        assertThat(sisPage.doesUrlMatch("https://ois2.ttu.ee/uusois/uus_ois2.tud_leht"), is(true));
    }

    @Test
    public void shouldHaveCorrectMoodleUrlInLoginSection(){
        HomePage homePage = new HomePage(driver);
        MoodlePage moodlePage = new MoodlePage(driver);

        homePage.hoverLoginDiv();
        homePage.clickMoodleLink();
        moodlePage.captureUrl();

        assertThat(moodlePage.doesUrlMatch("https://moodle.taltech.ee/login/index.php"), is(true));
    }

    @Test
    public void shouldHaveCorrectIntranetUrlInLoginSection(){
        HomePage homePage = new HomePage(driver);
        IntranetPage intranetPage = new IntranetPage(driver);

        homePage.hoverLoginDiv();
        homePage.clickIntranetLink();
        intranetPage.captureUrl();

        assertThat(intranetPage.doesUrlMatch("https://auth.ttu.ee/login/et/portal"), is(true));
    }
}
