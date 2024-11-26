using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Config;

namespace TodoMVC.Utilities
{
    public static class ExtentReportUtil
    {
        private static ExtentReports _extent;
        private static ExtentSparkReporter _sparkReporter;

        public static ExtentReports GetExtentReports(string reportDirectory)
        {
            if (_extent == null)
            {
                // Ensure the report directory exists
                Directory.CreateDirectory(reportDirectory);

                // Initialize the ExtentSparkReporter with the provided report directory
                _sparkReporter = new ExtentSparkReporter(Path.Combine(reportDirectory, "TestReport.html"));

                // Set report configuration
                _sparkReporter.Config.DocumentTitle = "TodoMVC Automation Test Report";
                _sparkReporter.Config.ReportName = "TodoMVC Test Results";
                _sparkReporter.Config.Theme = Theme.Dark;
              
                _extent = new ExtentReports();
                _extent.AttachReporter(_sparkReporter);
                _extent.AddSystemInfo("Tester", "Lawrence Lee");
                _extent.AddSystemInfo("Environment", "Test");
                _extent.AddSystemInfo("Browser", "Chrome");

                // Debug log
                Console.WriteLine("ExtentReports initialized with: " + Path.Combine(reportDirectory, "TestReport.html"));
            }

            return _extent;
        }

        public static void FlushReports()
        {
            if (_extent != null)
            {
                _extent.Flush();

                // Debug log
                Console.WriteLine("ExtentReports flushed.");
            }
        }
    }
}
