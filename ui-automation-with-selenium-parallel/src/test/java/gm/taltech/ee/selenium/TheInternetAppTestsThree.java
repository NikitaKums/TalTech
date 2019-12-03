package gm.taltech.ee.selenium;

import gm.taltech.ee.page_object.*;
import org.openqa.selenium.WebDriver;
import org.testng.annotations.*;

import static org.hamcrest.CoreMatchers.is;
import static org.hamcrest.MatcherAssert.assertThat;

public class TheInternetAppTestsThree {

    private static ThreadLocal<WebDriver> threadLocalDriver = new ThreadLocal<>();

    @BeforeMethod
    public void open_driver() {
        threadLocalDriver.set(DriverPicker.getDriver(System.getProperty("browser")));
    }

    @Test
    public void can_go_to_home_page() {
        HomePage homePage = new HomePage(get_driver());
        get_driver().get("https://the-internet.herokuapp.com/");
        assertThat(homePage.isAt(), is(true));
    }

    @Test
    public void should_login_to_secure_area_with_valid_credentials() {
        HomePage homePage = new HomePage(get_driver());
        get_driver().get("https://the-internet.herokuapp.com/");
        FormAuthenticationPage formAuthenticationPage = new FormAuthenticationPage(get_driver());

        homePage.clickFormAuthenticationLink();
        formAuthenticationPage.enterUsername("tomsmith");
        formAuthenticationPage.enterPassword("SuperSecretPassword!");
        formAuthenticationPage.clickSubmit();

        assertThat(formAuthenticationPage.isSuccessNotificationDisplayed(), is(true));
    }

    @Test
    public void should_not_login_to_secure_area_with_invalid_credentials() {
        HomePage homePage = new HomePage(get_driver());
        get_driver().get("https://the-internet.herokuapp.com/");
        FormAuthenticationPage formAuthenticationPage = new FormAuthenticationPage(get_driver());

        homePage.clickFormAuthenticationLink();
        formAuthenticationPage.enterUsername("nublugameboytetris");
        formAuthenticationPage.enterPassword("oksana");
        formAuthenticationPage.clickSubmit();

        assertThat(formAuthenticationPage.isErrorNotificationDisplayed(), is(true));
    }

    @Test
    public void should_not_login_to_secure_area_with_empty_fields() {
        HomePage homePage = new HomePage(get_driver());
        get_driver().get("https://the-internet.herokuapp.com/");
        FormAuthenticationPage formAuthenticationPage = new FormAuthenticationPage(get_driver());

        homePage.clickFormAuthenticationLink();
        formAuthenticationPage.enterUsername("");
        formAuthenticationPage.enterPassword("");
        formAuthenticationPage.clickSubmit();

        assertThat(formAuthenticationPage.isErrorNotificationDisplayed(), is(true));
    }

    @Test
    public void should_see_hello_world_after_loading() {
        HomePage homePage = new HomePage(get_driver());
        get_driver().get("https://the-internet.herokuapp.com/");
        DynamicLoadingPage dynamicLoadingPage = new DynamicLoadingPage(get_driver());

        homePage.clickDynamicLoadingLink();
        dynamicLoadingPage.clickExampleOne();
        dynamicLoadingPage.clickStart();

        assertThat(dynamicLoadingPage.isHelloWorldVisible(), is(true));
    }

    @Test
    public void should_select_option_one() {
        HomePage homePage = new HomePage(get_driver());
        get_driver().get("https://the-internet.herokuapp.com/");
        DropdownPage dropdownPage = new DropdownPage(get_driver());

        homePage.clickDropdownLink();
        dropdownPage.selectOptionOne();

        assertThat(dropdownPage.isOptionOneSelected(), is(true));
    }

    @Test
    public void should_see_view_profile_link_on_hover_first_profile() {
        HomePage homePage = new HomePage(get_driver());
        get_driver().get("https://the-internet.herokuapp.com/");
        HoversPage hoversPage = new HoversPage(get_driver());

        homePage.clickHoverLink();
        hoversPage.hoverOverFirstProfile();

        assertThat(hoversPage.isViewProfileLinkVisible(), is(true));
    }

    @Test
    public void should_see_view_profile_link_on_hover_second_profile() {
        HomePage homePage = new HomePage(get_driver());
        get_driver().get("https://the-internet.herokuapp.com/");
        HoversPage hoversPage = new HoversPage(get_driver());

        homePage.clickHoverLink();
        hoversPage.hoverOverSecondProfile();

        assertThat(hoversPage.isViewProfileLinkVisible(), is(true));
    }

    @Test
    public void should_change_location_of_blocks_after_drag_and_drop_to_BA() {
        HomePage homePage = new HomePage(get_driver());
        get_driver().get("https://the-internet.herokuapp.com/");
        DragAndDropPage dragAndDropPage = new DragAndDropPage(get_driver());

        homePage.clickDragAndDropLink();
        dragAndDropPage.dragAonTopOfB();

        assertThat(dragAndDropPage.isTheFirstElementsHeaderText("B"), is(true));
    }

    @Test
    public void should_not_change_location_of_blocks_after_drag_and_drop_to_random_element() {
        HomePage homePage = new HomePage(get_driver());
        get_driver().get("https://the-internet.herokuapp.com/");
        DragAndDropPage dragAndDropPage = new DragAndDropPage(get_driver());

        homePage.clickDragAndDropLink();
        dragAndDropPage.dragAonTopOfFooter();

        assertThat(dragAndDropPage.isTheFirstElementsHeaderText("A"), is(true));
    }

    private WebDriver get_driver() {
        return threadLocalDriver.get();
    }

    @AfterMethod
    public void tear_down() {
        get_driver().quit();
    }

    @AfterClass
    public void remove_thread() {
        threadLocalDriver.remove();
    }
}
