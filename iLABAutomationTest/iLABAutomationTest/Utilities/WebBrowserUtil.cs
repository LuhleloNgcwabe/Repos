using OpenQA.Selenium;
using OpenQA.Selenium.Html5;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLABAutomationTest.Utilities
{
    public  class WebBrowserUtil
    {
        IWebElement  element = null;
        public  void ClickElementByXpath(IWebDriver driver, string Locator)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
                wait.PollingInterval = TimeSpan.FromSeconds(3);
                wait.IgnoreExceptionTypes(typeof(ElementClickInterceptedException), typeof(NoSuchElementException));
                element = wait.Until((driver) => driver.FindElement(By.XPath(Locator)));
                //element = driver.FindElement(By.XPath(Locator));
                //scrolls element to view
                //Actions action = new Actions(driver);
                //action.MoveToElement(element).Perform();

                element.Click();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public  void EntertextToElment(IWebDriver driver,string Locator,string text)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException),typeof(ElementClickInterceptedException));
                element = wait.Until((driver) => driver.FindElement(By.XPath(Locator)));
                element.SendKeys(text);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public void Re_ClickWhileElementExist(IWebDriver driver, String XpathLocator)
        {
            int count = driver.FindElements(By.XPath(XpathLocator)).Count;
            while (count > 0)
            {
                ClickElementByXpath(driver, XpathLocator);
                count = driver.FindElements(By.XPath(XpathLocator)).Count;
            }
        }

        public void WaitforElementToExists(IWebDriver driver, string XpathLocator, int timeout)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            wait.PollingInterval = TimeSpan.FromSeconds(3);
            wait.IgnoreExceptionTypes(typeof(ElementClickInterceptedException), typeof(NoSuchElementException));
            element = wait.Until((driver) => driver.FindElement(By.XPath(XpathLocator)));

            Actions action = new Actions(driver);
            action.MoveToElement(element).Perform();
            Thread.Sleep(1000);
        }

    }
}
