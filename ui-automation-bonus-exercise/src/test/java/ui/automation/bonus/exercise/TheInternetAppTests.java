package ui.automation.bonus.exercise;

import io.github.bonigarcia.wdm.WebDriverManager;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.chrome.ChromeDriver;
import org.testng.annotations.AfterClass;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.BeforeMethod;
import org.testng.annotations.Test;
import ui.automation.bonus.exercise.page_object.DragDropPage;
import ui.automation.bonus.exercise.page_object.FormAuthenticationPage;
import ui.automation.bonus.exercise.page_object.HomePage;
import ui.automation.bonus.exercise.page_object.NewWindowPage;

import static org.hamcrest.CoreMatchers.is;
import static org.hamcrest.MatcherAssert.assertThat;

public class TheInternetAppTests
{
    private WebDriver driver;

    @BeforeClass
    public void set_up_driver(){
        WebDriverManager.chromedriver().setup();
        driver = new ChromeDriver();
    }

    @BeforeMethod
    public void open_driver(){
        driver.get("https://the-internet.herokuapp.com/");
    }

    @AfterClass
    public void close_driver(){
        if(driver != null){
            driver.close();
        }
    }

    //<editor-fold desc="Exercise 1. Form Authentication">
    @Test
    public void shouldDisplayErrorNotificationForInvalidUsername(){
        HomePage homePage = new HomePage(driver);
        FormAuthenticationPage formAuthenticationPage = new FormAuthenticationPage(driver);

        homePage.clickFormAuthenticationLink();
        formAuthenticationPage.enterUsername("100% correct username");
        formAuthenticationPage.clickSubmit();

        assertThat(formAuthenticationPage.isErrorForInvalidUsernameNotificationDisplayed(), is(true));
    }

    @Test
    public void shouldDisplayErrorNotificationForInvalidPassword(){
        HomePage homePage = new HomePage(driver);
        FormAuthenticationPage formAuthenticationPage = new FormAuthenticationPage(driver);

        homePage.clickFormAuthenticationLink();
        formAuthenticationPage.enterUsername("tomsmith");
        formAuthenticationPage.enterPassword("100% correct username");
        formAuthenticationPage.clickSubmit();

        assertThat(formAuthenticationPage.isErrorForInvalidPasswordNotificationDisplayed(), is(true));
    }

    @Test
    public void shouldDisplaySuccessForSuccessfulLogin(){
        HomePage homePage = new HomePage(driver);
        FormAuthenticationPage formAuthenticationPage = new FormAuthenticationPage(driver);

        homePage.clickFormAuthenticationLink();
        formAuthenticationPage.enterUsername("tomsmith");
        formAuthenticationPage.enterPassword("SuperSecretPassword!");
        formAuthenticationPage.clickSubmit();

        assertThat(formAuthenticationPage.isSuccessLoginNotificationDisplayed(), is(true));
    }

    @Test
    public void shouldDisplaySuccessForSuccessfulLogout(){
        HomePage homePage = new HomePage(driver);
        FormAuthenticationPage formAuthenticationPage = new FormAuthenticationPage(driver);

        homePage.clickFormAuthenticationLink();
        formAuthenticationPage.enterUsername("tomsmith");
        formAuthenticationPage.enterPassword("SuperSecretPassword!");
        formAuthenticationPage.clickSubmit();
        formAuthenticationPage.clickLogout();

        assertThat(formAuthenticationPage.isSuccessLogoutNotificationDisplayed(), is(true));
    }
    //</editor-fold>

    //<editor-fold desc="Exercise 2. Drag and drop">
    @Test
    public void shouldChangeOrderWhenDraggedOnTop(){
        HomePage homePage = new HomePage(driver);
        DragDropPage dragDropPage = new DragDropPage(driver);

        homePage.clickDragAndDropLink();
        dragDropPage.dragAndDropOnTop();

        assertThat(dragDropPage.didColumnsSwitchPositions(), is(true));
    }

    @Test
    public void shouldNotChangeOrderWhenDraggedNotOnTop(){
        HomePage homePage = new HomePage(driver);
        DragDropPage dragDropPage = new DragDropPage(driver);

        homePage.clickDragAndDropLink();
        dragDropPage.dragAndDropNotOnTop();

        assertThat(dragDropPage.didColumnsRemainAtInitialPositions(), is(true));
    }
    //</editor-fold>

    //<editor-fold desc="Exercise 3. New Window">
    @Test
    public void shouldSeeTextInNewWindowAfterClickingLink(){
        HomePage homePage = new HomePage(driver);
        NewWindowPage newWindowPage = new NewWindowPage(driver);
        String winHandleBefore = driver.getWindowHandle();

        homePage.clickNewWindowLink();
        newWindowPage.clickClickHereLink();

        for(String winHandle : driver.getWindowHandles()){
            if (!winHandle.equals(winHandleBefore)){
                newWindowPage.switchToWindow(winHandle);
            }
        }

        newWindowPage.captureTextToVerify();
        newWindowPage.closeWindow();
        newWindowPage.switchToWindow(winHandleBefore);

        assertThat(newWindowPage.isResultTextValid(), is(true));
    }
    //</editor-fold>
}