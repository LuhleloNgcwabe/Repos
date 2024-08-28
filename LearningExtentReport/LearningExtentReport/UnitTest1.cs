using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Gherkin;
using AventStack.ExtentReports.MarkupUtils;

namespace LearningExtentReport
{
    public class Tests
    {

        private const string CODE1 = "{\n    \"theme\": \"standard\",\n    \"encoding\": \"utf-8\n}";
        private const string CODE2 = "{\n    \"protocol\": \"HTTPS\",\n    \"timelineEnabled\": false\n}";

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            ExtentReports extent = new ExtentReports();
            // will only contain failures
            var sparkFail = new ExtentSparkReporter("SparkFail.html")
              .Filter
                .StatusFilter
                  .As(new Status[] { Status.Fail })
                  .Apply();
            var sparkFailError = new ExtentSparkReporter("SparkFailError.html")
              .Filter
                .StatusFilter
                  .As(new Status[] { Status.Error })
                  .Apply();

            // will contain all tests
            var sparkAll = new ExtentSparkReporter("SparkAll.html");
            extent.AttachReporter(sparkFail, sparkFailError,sparkAll);
            //extent.AttachReporter(spark);
            //spark = spark.Filter.StatusFilter
            
            extent.CreateTest("MyFirstTest")
                .CreateNode("try").Fail("llllll")
              .Log(Status.Fail, "This is a logging event for MyFirstTest, and it failed!");
            extent.Flush();

            extent.CreateTest("MyFirstTest2")
                .CreateNode("try").Pass()
              .Log(Status.Pass, "This is a logging event for MyFirstTest, and it passed!");
            extent.Flush();

            extent.CreateTest("MyFirstTest3")
             .Log(Status.Error, "System is not stable, and test is marked as not Tested");
            extent.Flush();

            extent.CreateTest("MyFirstTest4")
             .Log(Status.Warning, "Warning");
            extent.Flush();

            extent.CreateTest("MyFirstTest5")
            .Log(Status.Info, Status.Info.ToString());
            extent.Flush();
            //Assert.Pass();
        }
        [Test]
        public void test2()
        {
            ExtentReports extent = new ExtentReports();
            ExtentSparkReporter sparkAll = new ExtentSparkReporter("BBD.html");
            extent.AttachReporter(sparkAll);
            var feature = extent.CreateTest<Feature>("Refund item");
            var scenario = feature.CreateNode<Scenario>("Jeff returns a faulty microwave");
            scenario.CreateNode<Given>("Jeff has bought a microwave for $100").Pass("");
            scenario.CreateNode<And>("he has a receipt").Pass("");
            scenario.CreateNode<When>("he returns the microwave").Pass("");
            scenario.CreateNode<Then>("Jeff should be refunded $100").Fail("fail");

            //var feature = extent.CreateTest(new GherkinKeyword("Feature"), "Refund item");
            //var scenario = feature.CreateNode(new GherkinKeyword("Scenario"), "Jeff returns a faulty microwave");
            //scenario.CreateNode(new GherkinKeyword("Given"), "Jeff has bought a microwave for $100").Pass("");
            //scenario.CreateNode(new GherkinKeyword("And"), "he has a receipt").Pass("");
            //scenario.CreateNode(new GherkinKeyword("When"), "he returns the microwave").Pass("");
            //scenario.CreateNode(new GherkinKeyword("Then"), "Jeff should be refunded $100").Fail("");
            extent.Flush();
        }


        [Test]
        public void Test3 ()
        {

//            using AventStack.ExtentReports;
//            using AventStack.ExtentReports.MarkupUtils;
//            using AventStack.ExtentReports.Reporter;

//public class Program
//        {
            //static void Main(string[] args)
            //{
                var extent = new ExtentReports();
                var spark = new ExtentSparkReporter("Spark.html");
                extent.AttachReporter(spark);

                extent.CreateTest("ScreenCapture")
                        .AddScreenCaptureFromPath("extent.png")
                        .Pass(MediaEntityBuilder.CreateScreenCaptureFromPath("extent.png").Build());

                extent.CreateTest("LogLevels")
                        .Info("info")
                        .Pass("pass")
                        .Warning("warn")
                        .Skip("skip")
                        .Fail("fail");

                extent.CreateTest("CodeBlock").GenerateLog(
                        Status.Pass,
                        MarkupHelper.CreateCodeBlock(new string[] { CODE1, CODE2 }));

                extent.CreateTest("ParentWithChild")
                        .CreateNode("Child")
                        .Pass("This test is created as a toggle as part of a child test of 'ParentWithChild'");

                extent.CreateTest("Tags")
                        .AssignCategory("MyTag")
                        .Pass("The test 'Tags' was assigned by the tag <span class='badge badge-primary'>MyTag</span>");

                extent.CreateTest("Authors")
                        .AssignAuthor("TheAuthor")
                        .Pass("This test 'Authors' was assigned by a special kind of author tag.");

                extent.CreateTest("Devices")
                        .AssignDevice("TheDevice")
                        .Pass("This test 'Devices' was assigned by a special kind of devices tag.");

                extent.CreateTest("Exception! <i class='fa fa-frown-o'></i>")
                        .Fail(new Exception("A runtime exception occurred!"));

                extent.Flush();
            //}
        }


    //}
    }
}