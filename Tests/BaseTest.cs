using AventStack.ExtentReports;
using OpenQA.Selenium;
using TodoMVC.Utilities;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;


namespace TodoMVC.Tests
{
    public class BaseTest
    {
        protected IWebDriver _driver;
        protected static ExtentReports _extent;
        protected ExtentTest _test;

        protected string baseUrl;
        protected string browser;
        protected string pageLoadTimeout;
        protected string baseReportDirectory;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            string reportDirectory = PathUtil.GetReportDirectory(ref baseReportDirectory);
            _extent = ExtentReportUtil.GetExtentReports(reportDirectory);

            Console.WriteLine("ExtentReports initialized with directory: " + reportDirectory);
        }

        [SetUp]
        public void Setup()
        {
            baseUrl = ConfigUtil.GetBaseUrl();
            browser = ConfigUtil.GetBrowser();
            pageLoadTimeout = ConfigUtil.GetPageLoadTimeout();

            if (browser.Equals("Chrome", StringComparison.OrdinalIgnoreCase))
            {
                _driver = new ChromeDriver();
            }
            else if (browser == "Edge")
            {
                _driver = new EdgeDriver();
            }
            else if (browser == "Firefox")
            {
                _driver = new FirefoxDriver();
            }     
            else
            {
                throw new Exception("Browser not supported in configuration.");
            }

            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(int.Parse(pageLoadTimeout));
            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl(baseUrl);

            _test = _extent.CreateTest(TestContext.CurrentContext.Test.Name);
            Console.WriteLine("Test started: " + TestContext.CurrentContext.Test.Name);
           
        }

        [TearDown]
        public void TearDown()
        {
            //Check test status and log results
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var errorMessage = TestContext.CurrentContext.Result.Message;

            if (status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                string screenshotPath = ScreenshotUtil.TakeScreenshot(_driver, baseReportDirectory);
                _test.Fail("Test Failed" + TestContext.CurrentContext.Result.Message, MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());

            }
            else if (status == NUnit.Framework.Interfaces.TestStatus.Passed)
            {
                _test.Pass("Test Passed");
            }
            else
            {
                _test.Skip("Test Skipped: " + TestContext.CurrentContext.Result.Message);
            }

            _driver.Quit();

            Console.WriteLine("Test completed: " + TestContext.CurrentContext.Test.Name);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            ExtentReportUtil.FlushReports();
        }
    }
}
