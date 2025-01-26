using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace Selenium101_Test_Project.SeleniumAutomation
{
    public class Tests
    {
        IWebDriver ?driver;
        TestLocators testLocator;

        [SetUp]
        public void Setup()
        {
            EdgeOptions capabilities = new EdgeOptions();
     //       ChromeOptions capabilities = new ChromeOptions();
            capabilities.BrowserVersion = "dev";
            Dictionary<string, object> ltOptions = new Dictionary<string, object>();
            ltOptions.Add("username", "sumitbambal123");
            ltOptions.Add("accessKey", "m6uRYZfckcMaesux5Iyya9RR9x9NO1ToWrNZO6m1XAmvu1dSD8");
            ltOptions.Add("platformName", "Windows 10");
            ltOptions.Add("project", "Untitled");
            ltOptions.Add("w3c", true);
            ltOptions.Add("plugin", "c#-nunit");
            capabilities.AddAdditionalOption("LT:Options", ltOptions);
            driver = new EdgeDriver(capabilities);
            testLocator = new TestLocators(driver);
            driver.Navigate().GoToUrl("https://www.lambdatest.com/selenium-playground/");
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void TestScenario1()
        {
            //Arrange
            //Act
            testLocator.Click(testLocator.SimpleFrmDemo);
            testLocator.EnterText(testLocator.SimplefrmIp, "Welcome to LambdaTest");
            testLocator.Click(testLocator.GetCheckedValue);
            //Assert
            Assert.That(driver.Url, Does.Contain("simple-form-demo"));
            Assert.That(testLocator.SampleMsg.Text, Is.EqualTo("Welcome to LambdaTest"));
        }

        [Test]
        public void TestScenario2()
        {
            //Act
            testLocator.Click(testLocator.DragAndDrop);
            Actions action = new Actions(driver);
            // Move slider to the right on x-axis by 212 pixels
            action.DragAndDropToOffset(testLocator.Slider, 212, 0).Perform();
            //Assert
            Assert.That(testLocator.SliderValue.GetAttribute("value"), Is.EqualTo("95"));
        }

        [Test]
        public void TestScenario3()
        {
            //Act
            testLocator.Click(testLocator.InputForm);
            testLocator.Click(testLocator.SubmitForm);

            // Switch to active element
            IWebElement activeElement = driver.SwitchTo().ActiveElement();
            var validationMessage = activeElement.GetAttribute("validationMessage");

            //Assert
            Assert.That(validationMessage, Is.EqualTo("Please fill out this field."));

            //act
            testLocator.EnterText(testLocator.Name, "Sumit");
            testLocator.EnterText(testLocator.Email, "sumit@email.com");
            testLocator.EnterText(testLocator.Password, "Password");
            testLocator.EnterText(testLocator.Company, "LambdaTest");
            testLocator.EnterText(testLocator.Website, "https://www.lambdatest.com");
            //Select Country from drop down list
            SelectElement s = new SelectElement(testLocator.Country);
            s.SelectByText("United States");
            testLocator.EnterText(testLocator.City, "Albuquerque");
            testLocator.EnterText(testLocator.Address1, "123 Street");
            testLocator.EnterText(testLocator.Address2, "123 Street");
            testLocator.EnterText(testLocator.State, "New Maxico");
            testLocator.EnterText(testLocator.Zip, "19025");
            testLocator.Click(testLocator.SubmitForm);
            //Assert
            Assert.True(testLocator.SuccessMsg.Displayed);
            Assert.That(testLocator.SuccessMsg.Text, Is.EqualTo("Thanks for contacting us, we will get back to you shortly."));

        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
        }
    }
}