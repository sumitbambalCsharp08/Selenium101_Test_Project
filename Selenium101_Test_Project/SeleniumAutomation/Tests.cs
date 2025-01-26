using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Interactions;

namespace Selenium101_Test_Project.SeleniumAutomation
{
    public class Tests
    {
        IWebDriver driver;
        TestLocators testLocator;

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void TestScenario1()
        {

        }



        [TearDown]
        public void TearDown()
        {
            driver.Close();
        }
    }
}