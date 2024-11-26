using System.Text.RegularExpressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;


namespace TodoMVC.Pages
{
    public class TodoPage
    {
        private readonly IWebDriver _driver;

        public TodoPage(IWebDriver driver)
        {
            _driver = driver;
        }

        // Locators
        public By TxtNewTodo_Locator => By.XPath("//input[@placeholder='What needs to be done?']");
        public IWebElement TxtNewTodo => _driver.FindElement(TxtNewTodo_Locator);
        public IList<IWebElement> TodoListItems => _driver.FindElements(By.XPath("//ul[@class='todo-list']/li")); 
        public IList<IWebElement> TodoListItemsCompleted => _driver.FindElements(By.XPath("//ul[@class='todo-list']/li[@class='completed']"));
        public By ClearCompleteButton_Locator => By.XPath("//button[text()='Clear completed']");
        public IWebElement ClearCompletedButton => _driver.FindElement(ClearCompleteButton_Locator);
        public IWebElement ActiveFilterButton => _driver.FindElement(By.XPath("//a[text()='Active']"));
        public IWebElement CompletedFilterButton => _driver.FindElement(By.XPath("//a[text()='Completed']"));
        public IWebElement AllFilterButton => _driver.FindElement(By.XPath("//a[text()='All']"));
        public IWebElement ActiveTodoCount => _driver.FindElement(By.XPath("//span[@class='todo-count']"));
        public IWebElement ToggleButton => _driver.FindElement(By.XPath("//input[@class='toggle-all']"));
        public IWebElement GetTodoCheckbox(string todoItem) => _driver.FindElement(By.XPath($"//label[text()='{todoItem}']/preceding-sibling::input"));
        public IWebElement GetTodoDeleteButton(string todoItem) => _driver.FindElement(By.XPath($"//label[text()='{todoItem}']/following-sibling::button"));
        public By GetTodoLabel_Locator(string todoItem) => By.XPath($"//div[@class='view']//label[@data-testid='todo-item-label'][text()='{todoItem}']");
        public IWebElement GetTodoLabel(string todoItem) => _driver.FindElement(GetTodoLabel_Locator(todoItem));
        public By GetEditingTodoLabel_Locator(string todoItem) => By.XPath($"//div[@class='view']/div[@class='input-container']/input[contains(text,'{todoItem}')]");
        public IWebElement GetEditingTodoLabel(string todoItem) => _driver.FindElement(GetEditingTodoLabel_Locator(todoItem));
        public By Footer_locator => By.XPath("//footer[@class='footer']");

    }
}
