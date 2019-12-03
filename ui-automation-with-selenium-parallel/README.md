# UI Automation with Selenium Parallel
# Nikita Kums
## Note:
Browsers are picked according to system property ```browser```.

If using IntelliJ:
https://stackoverflow.com/a/29454841

To run classes and methods in parallel run ```parallel-tests.xml``` suite.
## 

#### Supported browsers:
- Firefox ```-Dbrowser=Firefox```
- Chrome (default) ```-Dbrowser=Chrome``` or not specified


## Bar chart representing thread count impact on test execution time:
Note: Amount of tests executed in the measurement: 10

<img src="https://i.imgur.com/es9RpP0.jpg" alt="Graph execution time / number of threads">

## Exercise

##### First things first:
- Use your selenium UI tests or use the ones here: https://gitlab.cs.ttu.ee/german.mumma/ui-automation-with-selenium
- Ff you want you can use JUnit4, Junit5, Gradle etc instead of TestNG and Maven, but for this exercise use selenium-java not Selenide. This also means not Cypress
- Add some more tests to your test class so you have at least 10. You can just copy+paste+modify existing ones for example test cases could be: test choosing other options, using other but interesting data, test login with missing credentials, hover and view second, third profile link etc.
- Add two more test classes (just copy+paste existing one: TheInternetAppOneTests, TheInternetAppTwoTests etc)
##### Requirements:
- Make your test classes run in parallel - 2p
- Make your tests (the individual methods inside test classes) run in parallel - 4p
- Add ability to change browser by passing a command line argument 'browser'. Tests should work even if argument is not given. - 2p
- Play with thread counts and measure execution time. NB! This applies to test methods not test classes. Create a bar chart which shows how the execution time relates to the number of threads. Add the barchar as picture to your projects README - 2p
##### Things to consider:
- Currently our Java UI tests are not thread safe, in order to run the all individual tests inside all of our test classes in parallel successfully, you will need to make them thread safe. This mostly applies to managing web driver threads.