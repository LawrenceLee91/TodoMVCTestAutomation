

namespace TodoMVC.Utilities
{
    public static class PathUtil
    {
        public static string GetReportDirectory(ref string baseReportDirectory)
        {
            //string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string baseDirectory = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName, "Reports");

            baseReportDirectory = Path.Combine(baseDirectory, "Report_" + DateTime.Now.ToString("yyyyMMdd_HHmmss"));

            if (!Directory.Exists(baseReportDirectory))
            {
                Directory.CreateDirectory(baseReportDirectory);
                Console.WriteLine("Report directory created: " + baseReportDirectory);
            }
            else
            {
                Console.WriteLine("Report directory already exists: " + baseReportDirectory);
            }

            return baseReportDirectory;
        }

        public static string GetScreenshotDirectory(string reportDirectory)
        {
            //string reportDirectory = GetReportDirectory();
            //string screenshotDirectory = Path.Combine(reportDirectory, "Screenshots");
            string screenshotDirectory = Path.Combine(reportDirectory);
            if (!Directory.Exists(screenshotDirectory))
            {
                Directory.CreateDirectory(screenshotDirectory);
                Console.WriteLine("Screenshot directory created: " + screenshotDirectory);
            }
            else
            {
                Console.WriteLine("Screenshot directory already exists: " + screenshotDirectory);
            }

            return screenshotDirectory;
        }
    }
}
