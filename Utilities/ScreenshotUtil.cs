using OpenQA.Selenium;

namespace TodoMVC.Utilities
{
    public static class ScreenshotUtil
    {
        public static string TakeScreenshot(IWebDriver driver, string screenshotPath1)
        {
            try
            {
                string screenshotDirectory = PathUtil.GetScreenshotDirectory(screenshotPath1);
                string fileName = $"Screenshot_{DateTime.Now:yyyyMMdd_HHmmss}.png";
                string screenshotPath = Path.Combine(screenshotDirectory, fileName);

                ITakesScreenshot screenshotDriver = (ITakesScreenshot)driver;
                Screenshot screenshot = screenshotDriver.GetScreenshot();

                screenshot.SaveAsFile(screenshotPath);
                Console.WriteLine("Screenshot saved: " + screenshotPath);

                return screenshotPath; // Return the full path to the saved screenshot
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error capturing screenshot: " + ex.Message);
                throw;
            }
        }
    }
}
