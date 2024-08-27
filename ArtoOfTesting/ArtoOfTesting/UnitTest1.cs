using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.Runtime.Intrinsics.X86;
using static System.Net.Mime.MediaTypeNames;

namespace ArtoOfTesting
{
    public class Tests:TestBase
    {
        IWebDriver driver;
        private string testName;
        [SetUp]
        public void Setup()
        {
            
            StartExtentTest(TestContext.CurrentContext.Test.Name);
            driver = new ChromeDriver(testName);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            driver.Url = "https://artoftesting.com/samplesiteforselenium";
            driver.Manage().Window.Maximize();
            Thread.Sleep(5000);
        }
        [Test]
        public void ClickLintText()
        {
            IWebElement link = driver.FindElement(By.LinkText("This is a link"));
            link.Click();
            Thread.Sleep(5000);
            TestContext.WriteLine("ClickLintText() passed");
        }
        [Test]
        public void SendingTextToAnElement()
        {
            String Name = "fname";
            IWebElement textBoxLocator = driver.FindElement(By.Id(Name));
            textBoxLocator.SendKeys("Luhlelo Ngcwabe");
            string EleText = textBoxLocator.GetAttribute("value");
            Assert.AreEqual(EleText,"add");
            //TestContext.WriteLine("SendingTextToAnElement() passed");
        }
        [Test]
        public void ButtonClick()
        {
            IWebElement ButtonLocator = driver.FindElement(By.Id("idOfButton"));
            ButtonLocator.Click();
            throw new NotImplementedException();
            //TestContext.WriteLine("ButtonClick() passed");
        }

        [Test]
        public void DoubleClickElement()
        {
            IWebElement btnDoubleClick = driver.FindElement(By.Id("dblClkBtn"));
            new Actions(driver)
                .DoubleClick(btnDoubleClick)
                .Perform();
            //pass if alert is displayed
            IAlert alert = driver.SwitchTo().Alert();
            string text = alert.Text;
            alert.Accept();
            Assert.Pass();
            TestContext.WriteLine("DoubleClickElement() passed");
        }

        [Test]
        public void selectRadioButton()
        {
            IWebElement RadioButton = driver.FindElement(By.Id("female"));
            RadioButton.Click();
            Assert.Fail("selectRadioButton");
            TestContext.WriteLine("selectRadioButton() passed");
        }

        [Test]
        public void CheckboxOption()
        {
            IWebElement CheckboxOptionAut = driver.FindElement(By.ClassName("Automation"));
            CheckboxOptionAut.Click();
            IWebElement cbPerformance = driver.FindElement(By.ClassName("Performance"));
            cbPerformance.Click();
            Assert.Fail();
            TestContext.WriteLine("CheckboxOption() passed");
        }


        [Test]
        public void DropdownList()
        {
            IWebElement DropdownList = driver.FindElement(By.Id("manual"));
            DropdownList.Click();
            Assert.Pass();
            TestContext.WriteLine("DropdownList() passed");

        }

        [Test]
        public void GenerateAlert()
        {
          IWebElement DropdownList = driver.FindElement(By.XPath("//*[contains(text(),'Generate Alert Box')]"));
          DropdownList.Click();
          IAlert alert = driver.SwitchTo().Alert();
          string text = alert.Text;
            alert.Accept();
            if (text.ToString() == "Hi! Art Of Testing, Here!")
            {
                Assert.Pass();
            }
            TestContext.WriteLine("GenerateAlert() passed");
        }

        [Test]
        public void GenerateConfirmationBox()
        {
            IWebElement DropdownList = driver.FindElement(By.XPath("//button[contains(text(),'Generate Confirm Box')]"));
            DropdownList.Click();
            IAlert alert = driver.SwitchTo().Alert();
            alert.Dismiss();

            string outcome = driver.FindElement(By.Id("demo")).Text;
            if (outcome.ToString() == "You pressed Cancel!")
            {
                Assert.Pass();
                TestContext.WriteLine("GenerateConfirmationBox() passed");
            }
        }
        [Test]
        public void DragAndDrop()
        {
            IWebElement Draggable = driver.FindElement(By.Id("myImage"));
            IWebElement Droppable = driver.FindElement(By.Id("targetDiv"));
            new Actions(driver).DragAndDrop(Draggable, Droppable)
                .Perform();
            if (driver.FindElements(By.XPath("//div[@id='targetDiv']/descendant::img")).Count() > 0)
            {
                Assert.Pass();
                TestContext.WriteLine("DragAndDrop()");
            }

        }

        [Test]
        public void Explicit()
        {
            driver.Url = "https://www.selenium.dev/selenium/web/dynamic.html";
            IWebElement revealed = driver.FindElement(By.Id("revealed"));
            driver.FindElement(By.Id("reveal")).Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
            wait.Until(d => revealed.Displayed);

            revealed.SendKeys("Displayed");
            Assert.AreEqual("Displayed", revealed.GetDomProperty("value"));
        }
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {

            //for sake of testing;
        }
        [TearDown]
        public void TearDown()
        {
            driver.Dispose();
            
        }
    }
}