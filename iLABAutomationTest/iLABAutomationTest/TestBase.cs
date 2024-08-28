using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLABAutomationTest
{
    public class TestBase
    {
        public static ExtentReports extent;
        public static ExtentTest testlog;


        [ OneTimeSetUp]
        public void StartReport()
        {
            ExtentSparkReporter htmlReporter = new ExtentSparkReporter("Report.html");
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);

            extent.AddSystemInfo("Environment", "QA");
            extent.AddSystemInfo("Tester", Environment.UserName);
            extent.AddSystemInfo("MachineName", Environment.MachineName);

        }


        [TearDown]
        public void EndReport()
        {
            LoggingTestStatusExtentReport();
            extent.Flush();
        }
        public static void StartExtentTest(string testsToStart)
        {
            testlog = extent.CreateTest(testsToStart);
        }
        public static void LoggingTestStatusExtentReport()
        {

            try
            {
                var status = TestContext.CurrentContext.Result.Outcome.Status;
                var stacktrace = string.Empty + TestContext.CurrentContext.Result.StackTrace + string.Empty;
                var errorMessage = TestContext.CurrentContext.Result.Message.Replace("System.Exception : NUnit.Framework.AssertionException:  ","");
                //var st = NUnit.Framework.TestContext.CurrentContext.Result.
                Status logstatus;

                switch (status)
                {
                    case TestStatus.Failed:
                        logstatus = Status.Fail;
                        testlog.Log(Status.Fail, "Test steps NOT Completed for Test case " + TestContext.CurrentContext.Test.Name + " ");//testlog.Model.FullName //TestContext.CurrentContext.Test.Name 
                        testlog.Log(Status.Fail, "Test ended with " + Status.Fail + " – " + errorMessage);
                        //testlog.Log(Status.Fail, "Stacktrace" + stacktrace);
                        break;
                    case TestStatus.Skipped:
                        logstatus = Status.Skip;
                        testlog.Log(Status.Skip, "Test ended with " + Status.Skip);
                        break;
                    default:
                        logstatus = Status.Pass;
                        testlog.Log(Status.Pass, "Test steps finished for test case " + testlog.Model.FullName);
                        testlog.Log(Status.Pass, "Test ended with " + Status.Pass);
                        break;
                }
            }
            catch(WebDriverException ex)
            {

            }
        }
    }
}
