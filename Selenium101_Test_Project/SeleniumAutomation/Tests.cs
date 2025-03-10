using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace Selenium101_Test_Project.SeleniumAutomation
{
    [TestFixture("chrome")]
    [TestFixture("firefox")]
    [TestFixture("edge")]
    [Parallelizable(ParallelScope.All)]
    public class Tests
    {
        public static string LT_USERNAME = Environment.GetEnvironmentVariable("LT_USERNAME") ?? "sumitbambal123";
        public static string LT_ACCESS_KEY = Environment.GetEnvironmentVariable("LT_ACCESS_KEY") ?? "m6uRYZfckcMaesux5Iyya9RR9x9NO1ToWrNZO6m1XAmvu1dSD8";
        public static bool tunnel = bool.Parse(Environment.GetEnvironmentVariable("LT_TUNNEL") ?? "false");
        public static string build = Environment.GetEnvironmentVariable("LT_BUILD") ?? "LambdatestBuildChrome";
        public static string seleniumUri = "https://hub.lambdatest.com:443/wd/hub";
 
        IWebDriver? driver;
        TestLocators? testLocator;
        private string currentBrowser = "chrome";
        public Tests(string currentBrowser)
        {
            this.currentBrowser = currentBrowser;
        }

        [SetUp]
        public void Init()
        {
            DriverOptions capabilities;
            switch (currentBrowser?.ToLower())
            {
                case "chrome":
                    capabilities = new ChromeOptions();
                    break;
                case "firefox":
                    capabilities = new FirefoxOptions();
                    break;
                case "edge":
                    capabilities = new EdgeOptions();
                    break;
                default:
                    throw new ArgumentException("Browser not supported");
            }

            capabilities.BrowserVersion = "dev";
            Dictionary<string, object> ltOptions = new Dictionary<string, object>
            {
                { "username", LT_USERNAME },
                { "accessKey", LT_ACCESS_KEY },
                { "platformName", "Windows 10" },
                { "project", "Selenium101_Test_Project" },
                { "w3c", true },
                { "plugin", "c#-nunit" }
            };

            if (tunnel)
            {
                ltOptions.Add("tunnel", tunnel);
            }
            if (build != null)
            {
                ltOptions.Add("build", build);
            }

            capabilities.AddAdditionalOption("lt:options", ltOptions);
            capabilities.AddAdditionalOption("name",
                string.Format("{0}:{1}",
                TestContext.CurrentContext.Test.ClassName,
                TestContext.CurrentContext.Test.MethodName));

            driver = new RemoteWebDriver(new Uri(seleniumUri), capabilities.ToCapabilities(), TimeSpan.FromSeconds(600));
            Console.Out.WriteLine(driver);
        }

        [Test]
        public void TestScenario1()
        {
            // Arrange
            testLocator = new TestLocators(driver);
            driver.Navigate().GoToUrl("https://www.lambdatest.com/selenium-playground/");
            driver.Manage().Window.Maximize();
            // Act
            testLocator.Click(testLocator.SimpleFrmDemo);
            testLocator.EnterText(testLocator.SimplefrmIp, "Welcome to LambdaTest");
            testLocator.Click(testLocator.GetCheckedValue);
            // Assert
            Assert.That(driver.Url, Does.Contain("simple-form-demo"));
            Assert.That(testLocator.SampleMsg.Text, Is.EqualTo("Welcome to LambdaTest"));
        }
        [Test]
        public void TestScenario2()
        {
            // Arrange
            testLocator = new TestLocators(driver);
            driver.Navigate().GoToUrl("https://www.lambdatest.com/selenium-playground/");
            driver.Manage().Window.Maximize();
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
            // Arrange
            testLocator = new TestLocators(driver);
            driver.Navigate().GoToUrl("https://www.lambdatest.com/selenium-playground/");
            driver.Manage().Window.Maximize();
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
        public void Close()
        {
            driver?.Close();
        }
    }
}