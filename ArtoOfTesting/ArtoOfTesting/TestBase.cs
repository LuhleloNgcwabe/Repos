using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ArtoOfTesting
{
    public class TestBase
    {
        public static ExtentReports extent;
        public static ExtentTest testlog;

        [OneTimeSetUp]
        public void StartReport()
        {
            //string path = Assembly.GetCallingAssembly().CodeBase;
            //string actualPath = path.Substring(0, path.LastIndexOf("bin"));
            //string projectPath = new Uri(actualPath).LocalPath;

            //string reportPath = projectPath + "Report.html";

            //System.IO.Directory.CreateDirectory(reportPath);
            ExtentSparkReporter htmlReporter = new ExtentSparkReporter("Report.html");
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);

            extent.AddSystemInfo("Environment", "QA");
            extent.AddSystemInfo("Tester", Environment.UserName);
            extent.AddSystemInfo("MachineName", Environment.MachineName);

        }

        [OneTimeTearDown]
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
                var errorMessage = TestContext.CurrentContext.Result.Message;
                Status logstatus;
                switch (status)
                {
                    case TestStatus.Failed:
                        logstatus = Status.Fail;
                        testlog.Log(Status.Fail, "Test steps NOT Completed for Test case " + testlog.Model.FullName + " ");//testlog.Model.FullName //TestContext.CurrentContext.Test.Name 
                        testlog.Log(Status.Fail, "Test ended with " + Status.Fail + " – " + errorMessage);
                        break;
                    case TestStatus.Skipped:
                        logstatus = Status.Skip;
                        testlog.Log(Status.Skip, "Test ended with " + Status.Skip);
                        break;
                    default:
                        logstatus = Status.Pass;
                        testlog.Log(Status.Pass, "Test steps finished for test case " + testlog.Model.FullName );
                        testlog.Log(Status.Pass, "Test ended with " + Status.Pass);
                        break;
                }
            }
            catch (WebDriverException ex)
            {
                throw ex;
            }

        }

    }

}

