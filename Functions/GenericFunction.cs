using OpenQA.Selenium;


namespace TodoMVC.Functions
{
    public class GenericFunction
    {
        private readonly IWebDriver _driver;

        public GenericFunction(IWebDriver driver)
        {
            _driver = driver;
        }
       
        public bool IsElementVisible(By locator)
        {
            try
            {
                return _driver.FindElement(locator).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public bool IsElementPresent(By locator)
        {
            try
            {
                _driver.FindElement(locator);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public bool IsElementAbsent(By locator)
        {
            try
            {
                _driver.FindElement(locator);
                return false;
            }
            catch (NoSuchElementException)
            {
                return true;
            }
        }

        public bool VerifyElementIsDisplayed(IWebElement element)
        {
            return element.Displayed;
        }
    }
}
