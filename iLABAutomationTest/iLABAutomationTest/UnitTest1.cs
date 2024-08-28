using AngleSharp.Dom;
using iLABAutomationTest.Utilities;
using NUnit.Framework;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System.IO;
using System.Reflection;
using System.Web;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;


namespace iLABAutomationTest
{
    public class Tests:TestBase
    {
        IWebDriver driver;
        [SetUp]
        public void Setup()
        {
            StartExtentTest(TestContext.CurrentContext.Test.Name);
            new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            driver.Manage().Window.Maximize();
            driver.Url = "https://www.ilabquality.com/career-opportunities/"; 
        }

        [Test, TestCaseSource(nameof(Tests.UserInfor))]
        public void Test1(string name, string surname, string email)
        {
            //Applies for the job on list of opportunities available at iLAB
            try
            {
                //ElementLocators
                string Career_Area = "(//a[contains(text(),'South Africa')])[1]";
                string Career_lastJob = "(//h3[contains(text(),'Current Openings')]/following::ul/li[last()]/a)[1]";
                string Firstname = "(//span[contains(text(),'First name')]/following::input)[1]";
                string CloseConsentBar = "//div[@class='cky-consent-bar']/button";
                string Lastname = "//*[@name='lastname']";
                string Email = "//*[@name='email']";
                string Phone = "//*[@name='phone']";
                string AnswerWhyAppliedForJob = "//*[@class='hs-input hs-fieldtype-textarea']";
                string uploadFile = "//input[@type='file']";

                WebBrowserUtil Browser = new WebBrowserUtil();
                Thread.Sleep(5000);
                Browser.ClickElementByXpath(driver, CloseConsentBar);
                Browser.WaitforElementToExists(driver, Career_Area, 60);
                Browser.Re_ClickWhileElementExist(driver, Career_Area);
                Browser.Re_ClickWhileElementExist(driver, Career_lastJob);
                driver.SwitchTo().Frame("hs-form-iframe-0");

                Browser.EntertextToElment(driver, Firstname, name);
                Browser.EntertextToElment(driver, Lastname, surname);
                Browser.EntertextToElment(driver, Email, email);
                Browser.EntertextToElment(driver, Phone, "0717525298");
                Browser.EntertextToElment(driver, AnswerWhyAppliedForJob, "The mission and values matches with  my organisational / Proffession mission statement and values. with that said i think i am the employ they want to have. I am ILAB in flesh");
               
                string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\..\\..\\";
                DirectoryInfo ParentDir = new DirectoryInfo(path);
                ParentDir.Attributes = FileAttributes.Normal;
                string[] allFiles = @Directory.GetFiles($@"{ParentDir.FullName}", $"Resume.txt", SearchOption.AllDirectories);
                FileInfo fileToUpLoad = new FileInfo(allFiles[0]);

                Browser.EntertextToElment(driver, uploadFile, fileToUpLoad.FullName);
                string filename =driver.FindElement(By.XPath("//*[@name='resume']")).GetAttribute("name");
                if (name.Contains("ee"))
                {
                    Assert.That(filename.Equals("resume"));
                }
                else
                {
                    Assert.That(filename.Equals("resume2"));
                }
                

            }
            catch (Exception ex)
            { 
                throw new Exception(ex.ToString());
            }
            
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }
        //Data source
        public static object[] UserInfor()
        {
            //Rows - no. of times test has to run
            //Cols - no. of times test has to run
            string[][] userDetails = new string[3][];

            userDetails[0] = new string[3];
            userDetails[0][0] = "Luhlelo";
            userDetails[0][1] = "Ngcwabe";
            userDetails[0][2] = "Dngcwabe@gmail.com";

            userDetails[1] = new string[3];
            userDetails[1][0] = "Sese";
            userDetails[1][1] = "Luhlelo";
            userDetails[1][2] = "Sese@gmail.com";

            userDetails[2] = new string[3];
            userDetails[2][0] = "Dee";
            userDetails[2][1] = "Lalili";
            userDetails[2][2] = "Dee@gmail.com";

            return userDetails;
        }
    }
}