using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Runtime.CompilerServices;

namespace SeleniumAutomation
{
    public class Tests
    {
        IWebDriver driver;
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void LaunchBrowser()
        {
            driver.Url = "https://artoftesting.com";
            //driver.Navigate().GoToUrl("https://artoftesting.com");
            //driver.Manage().Timeouts().PageLoad= TimeSpan.FromSeconds(15);
            driver.Manage().Window.Maximize();  
            //Implicit to give script more time to find element , before throwing element not visible Exception
            driver.Manage().Timeouts().ImplicitWait =TimeSpan.FromSeconds(15);
            //IWebElement a = driver.FindElement(By.Name("Search"));
            //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            //wait.Until(e => a.Displayed);
            //wait.Until
            //driver.Manage().Timeouts().
            Assert.Pass();
        }

        
        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
            
        }
    }
}