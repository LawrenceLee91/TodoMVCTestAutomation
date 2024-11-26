using System.Text.RegularExpressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using TodoMVC.Pages;


namespace TodoMVC.Functions
{
    public class TodoFunction
    {
        private readonly IWebDriver _driver;
        private readonly TodoPage _todoPage;

        public TodoFunction(IWebDriver driver)
        {
            _driver = driver;
            _todoPage = new TodoPage(driver);
        }

        public void AddTodoItem(string todoItem)
        {
            _todoPage.TxtNewTodo.SendKeys(todoItem);
            _todoPage.TxtNewTodo.SendKeys(Keys.Enter);
        }

        public void CompleteTodoItem(string todoItem)
        {
            var checkbox = _todoPage.GetTodoCheckbox(todoItem);
            if (!checkbox.Selected)
                checkbox.Click();
        }

        public void DeleteTodoItem(string todoItem)
        {
            var todoLabel = _todoPage.GetTodoLabel(todoItem);
            var deleteButton = _todoPage.GetTodoDeleteButton(todoItem);
            todoLabel.Click();
            deleteButton.Click();
        }

        public void EditTodoItem(string oldTodo, string newTodo)
        {
            var todoLabel = _todoPage.GetTodoLabel(oldTodo);

            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)_driver;
            jsExecutor.ExecuteScript("arguments[0].scrollIntoView(true);", todoLabel);

            // Instantiate Actions class
            Actions actions = new Actions(_driver);
            actions.DoubleClick(todoLabel).Perform();
            actions.KeyDown(Keys.Control).SendKeys("a").KeyUp(Keys.Control).Perform();
            actions.KeyDown(Keys.Delete).Perform();
            var todoLabel2 = _todoPage.GetEditingTodoLabel(string.Empty);

            todoLabel2.SendKeys(newTodo);
            todoLabel2.SendKeys(Keys.Enter);
        }

        public List<string> GetTodoListTexts() => _todoPage.TodoListItems.Select(item => item.Text).ToList();

        public List<string> GetCompletedTodoListTexts() => _todoPage.TodoListItemsCompleted.Select(item => item.Text).ToList();

        public int GetActiveTodoCount()
        {
            string CountMessage = _todoPage.ActiveTodoCount.Text;
            Match match = Regex.Match(CountMessage, @"\d+");


            if (match.Success)
            {
                return int.Parse(match.Value);
            }
            else
            {
                Console.WriteLine("No digit found in the string.");
                return 0;
            }
        }

        public void ClearCompletedTodos() => _todoPage.ClearCompletedButton.Click();

        public void FilterActiveTodos() => _todoPage.ActiveFilterButton.Click();
      
        public void FilterCompletedTodos() => _todoPage.CompletedFilterButton.Click();

        public void FilterAllTodos() => _todoPage.AllFilterButton.Click();

        public void ClickToggleButton() => _todoPage.ToggleButton.Click();
        
        public bool VerifyTodoItemExists(string todoItem)
        {
            var todoList = GetTodoListTexts();
            return todoList.Contains(todoItem);
        }

        public bool VerifyCompletedTodoItemExists(string todoItem)
        {
            var todoList = GetCompletedTodoListTexts();
            return todoList.Contains(todoItem);
        }
    }
}
