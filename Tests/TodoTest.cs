using NUnit.Framework.Internal.Execution;
using OpenQA.Selenium;
using TodoMVC.Functions;
using TodoMVC.Pages;


namespace TodoMVC.Tests
{
    [TestFixture]
    public class TodoTest : BaseTest
    {
        private TodoPage _todoPage;
        private TodoFunction _todoFunction;
        private GenericFunction _genericFunction;

        [SetUp]
        public void InitialisePageObjects()
        {
            _todoPage = new TodoPage(_driver);
            _todoFunction = new TodoFunction(_driver);
            _genericFunction = new GenericFunction(_driver);
        }

        [Test]
        public void TC001_AddTodoItem()
        {
            string todoItem = "Task 1";

            // Add a Todo item
            _todoFunction.AddTodoItem(todoItem);

            // Assert if the item added
            Assert.That(_todoFunction.VerifyTodoItemExists(todoItem), Is.True, "Todo item was not added successfully.");

            var isFooterPresent = _genericFunction.IsElementPresent(_todoPage.Footer_locator);
            Assert.That(isFooterPresent, "Footer is missing");

            int activeCount = _todoFunction.GetActiveTodoCount();
            Assert.That(activeCount, Is.EqualTo(1), $"Expected 1 active todos, but got {activeCount}.");

            _test.Pass("Test case: Add Todo Item passed successfully.");
        }

        [Test]
        public void TC002_CompleteTodoItem()
        {
            string todoItem = "Task 2";

            // Add and complete a Todo item
            _todoFunction.AddTodoItem(todoItem);
            _todoFunction.CompleteTodoItem(todoItem);

            // Assert if the item exists
            Assert.That(_todoFunction.VerifyCompletedTodoItemExists(todoItem), Is.True, "Todo item was not marked as complete.");

            int activeCount = _todoFunction.GetActiveTodoCount();
            Assert.That(activeCount, Is.EqualTo(0), $"Expected 0 active todos, but got {activeCount}.");

            _test.Pass("Test case: Complete Todo Item passed successfully.");
        }

        [Test]
        public void TC003_ClearCompletedTodos()
        {
            string todoItem = "Task 3";
         
            // Add and complete a Todo item
            _todoFunction.AddTodoItem(todoItem);
            _todoFunction.CompleteTodoItem(todoItem);

            // Clear completed Todos
            _todoFunction.ClearCompletedTodos();

            // Assert the item no longer exists
            Assert.That(_todoFunction.VerifyTodoItemExists(todoItem), Is.False, "Completed Todo item was not cleared.");

            _test.Pass("Test case: Clear Completed Todos passed successfully.");
        }

        [Test]
        public void TC004_AddMultipleTodoItems()
        {
            List<string> todoItems = new List<string> { "Todo Item 1", "Todo Item 2", "Todo Item 3" };

            // Add multiple Todo items
            foreach (var item in todoItems)
            {
                _todoFunction.AddTodoItem(item);
            }

            // Assert all items are added
            var todoList = _todoFunction.GetTodoListTexts();
            foreach (var item in todoItems)
            {
                Assert.That(todoList.Contains(item), $"Todo item '{item}' was not added successfully.");
            }

            _test.Pass("Test case: Add Multiple Todo Items passed successfully.");
        }

        [Test]
        public void TC005_MarkMultipleTodoItemsAsCompleted()
        {
            List<string> todoItems = new List<string> { "Task 1", "Task 2", "Task 3" };

            // Add multiple items
            foreach (var item in todoItems)
            {
                _todoFunction.AddTodoItem(item);
            }

            // Mark all items as completed
            foreach (var item in todoItems)
            {
                _todoFunction.CompleteTodoItem(item);
            }

            // Assert all items are completed
            var completedItems = _todoFunction.GetCompletedTodoListTexts();
            foreach (var item in todoItems)
            {
                Assert.That(completedItems.Contains(item), $"Todo item '{item}' was not marked as completed.");
            }

            int activeCount = _todoFunction.GetActiveTodoCount();
            Assert.That(activeCount, Is.EqualTo(0), $"Expected 0 active todos, but got {activeCount}.");

            _test.Pass("Test case: Mark All Todo Items as Completed passed successfully.");
        }

        [Test]
        public void TC006_MarkAndUmarkWithToggleAllButton()
        {
            List<string> todoItems = new List<string> { "Task 1", "Task 2", "Task 3" };

            // Add multiple items
            foreach (var item in todoItems)
            {
                _todoFunction.AddTodoItem(item);
            }

            // Mark all items as completed
            _todoFunction.ClickToggleButton();

            // Assert all items are completed
            var completedItems = _todoFunction.GetCompletedTodoListTexts();
            foreach (var item in todoItems)
            {
                Assert.That(completedItems.Contains(item), $"Todo item '{item}' was not marked as completed.");
            }

            int activeCount = _todoFunction.GetActiveTodoCount();
            Assert.That(activeCount, Is.EqualTo(0), $"Expected 0 active todos, but got {activeCount}.");

            //Umark all items as active
            _todoFunction.ClickToggleButton();

            var activeItems = _todoFunction.GetTodoListTexts();
            foreach (var item in todoItems)
            {
                Assert.That(activeItems.Contains(item), $"Todo item '{item}' was not found.");
            }

            int activeCount2 = _todoFunction.GetActiveTodoCount();
            Assert.That(activeCount2, Is.EqualTo(3), $"Expected 3 active todos, but got {activeCount}.");

            _test.Pass("Test case: mark and unmark todos item with toggle button passed successfully.");
        }

        [Test]
        public void TC007_ClearAllCompletedTodos()
        {
            List<string> todoItems = new List<string> { "Todo A", "Todo B", "Todo C" };

            // Add and complete items
            foreach (var item in todoItems)
            {
                _todoFunction.AddTodoItem(item);
                _todoFunction.CompleteTodoItem(item);
            }

            // Clear all completed items
            _todoFunction.ClearCompletedTodos();

            // Assert no completed items remain
            var remainingItems = _todoFunction.GetTodoListTexts();
            foreach (var item in todoItems)
            {
                Assert.That(!remainingItems.Contains(item), $"Todo item '{item}' was not cleared.");
            }

            var isFooterPresent = _genericFunction.IsElementPresent(_todoPage.Footer_locator);
            Assert.That(!isFooterPresent, "Footer is not hidden");

            _test.Pass("Test case: Clear All Completed Todos passed successfully.");
        }

        [Test]
        public void TC008_EditTodoItem()
        {
            string originalTodo = "Old Todo";
            string updatedTodo = "Updated Todo";

            // Add a Todo item
            _todoFunction.AddTodoItem(originalTodo);

            // Edit the Todo item
            _todoFunction.EditTodoItem(originalTodo, updatedTodo);

            // Assert the updated item exists
            Assert.That(_todoFunction.VerifyTodoItemExists(updatedTodo), "Updated Todo item was not saved.");
            Assert.That(!_todoFunction.VerifyTodoItemExists(originalTodo), "Original Todo item still exists after update.");

            _test.Pass("Test case: Edit Todo Item passed successfully.");
        }

        [Test]
        public void TC009_DeleteTodoItem()
        {
            string todoItem = "Todo to Delete";

            // Add a Todo item
            _todoFunction.AddTodoItem(todoItem);

            // Delete the Todo item
            _todoFunction.DeleteTodoItem(todoItem);

            // Assert the item no longer exists
            Assert.That(!_todoFunction.VerifyTodoItemExists(todoItem), "Todo item was not deleted successfully.");

            var isFooterPresent = _genericFunction.IsElementPresent(_todoPage.Footer_locator);
            Assert.That(!isFooterPresent, "Footer is not hidden");

            _test.Pass("Test case: Delete Todo Item passed successfully.");
        }

        [Test]
        public void TC010_VerifyActiveTodoCounter()
        {
            List<string> todoItems = new List<string> { "Task 1", "Task 2", "Task 3" };

            // Add multiple items
            foreach (var item in todoItems)
            {
                _todoFunction.AddTodoItem(item);
            }

            // Mark one item as completed
            _todoFunction.CompleteTodoItem("Task 1");

            // Assert the active counter displays correct count
            int activeCount = _todoFunction.GetActiveTodoCount();
            Assert.That(activeCount, Is.EqualTo(2), $"Expected 2 active todos, but got {activeCount}.");

            _test.Pass("Test case: Verify Active Todo Counter passed successfully.");
        }

        [Test]
        public void TC011_FilterActiveTodos()
        {
            List<string> todoItems = new List<string> { "Task A", "Task B", "Task C" };

            // Add and complete one item
            foreach (var item in todoItems)
            {
                _todoFunction.AddTodoItem(item);
            }
            _todoFunction.CompleteTodoItem("Task A");

            // Filter by active items
            _todoFunction.FilterActiveTodos();

            // Assert only active items are displayed
            var activeItems = _todoFunction.GetTodoListTexts();
            Assert.That(activeItems.Contains("Task B"), "Active Todo 'Task B' is not displayed.");
            Assert.That(activeItems.Contains("Task C"), "Active Todo 'Task C' is not displayed.");
            Assert.That(!activeItems.Contains("Task A"), "Completed Todo 'Task A' is displayed in active filter.");

            _test.Pass("Test case: Filter Active Todos passed successfully.");
        }

        [Test]
        public void TC012_FilterCompletedTodos()
        {
            List<string> todoItems = new List<string> { "Task A", "Task B", "Task C" };

            // Add and complete one item
            foreach (var item in todoItems)
            {
                _todoFunction.AddTodoItem(item);
            }
            _todoFunction.CompleteTodoItem("Task A");

            // Filter by Completed items
            _todoFunction.FilterCompletedTodos();

            // Assert only completed items are displayed
            var completedItems = _todoFunction.GetTodoListTexts();

            Assert.That(!completedItems.Contains("Task B"), "Active Todo 'Task B' is displayed.");
            Assert.That(!completedItems.Contains("Task C"), "Active Todo 'Task C' is displayed.");
            Assert.That(completedItems.Contains("Task A"), "Completed Todo 'Task A' is not displayed in Completed filter.");

            _test.Pass("Test case: Filter Completed Todos passed successfully.");
        }

        [Test]
        public void TC013_FilterAllTodos()
        {
            List<string> todoItems = new List<string> { "Task A", "Task B", "Task C" };

            // Add and complete one item
            foreach (var item in todoItems)
            {
                _todoFunction.AddTodoItem(item);
            }
            _todoFunction.CompleteTodoItem("Task A");

            // Filter by active items
            _todoFunction.FilterActiveTodos();

            // Assert only active items are displayed
            var activeItems = _todoFunction.GetTodoListTexts();
            Assert.That(activeItems.Contains("Task B"), "Active Todo 'Task B' is not displayed.");
            Assert.That(activeItems.Contains("Task C"), "Active Todo 'Task C' is not displayed.");
            Assert.That(!activeItems.Contains("Task A"), "Completed Todo 'Task A' is displayed in active filter.");

            // Filter by Completed items
            _todoFunction.FilterCompletedTodos();

            // Assert only completed items are displayed
            var completedItems = _todoFunction.GetTodoListTexts();

            Assert.That(!completedItems.Contains("Task B"), "Active Todo 'Task B' is displayed.");
            Assert.That(!completedItems.Contains("Task C"), "Active Todo 'Task C' is displayed.");
            Assert.That(completedItems.Contains("Task A"), "Completed Todo 'Task A' is not displayed in Completed filter.");

            // Filter by Completed items
            _todoFunction.FilterAllTodos();

            // Assert all items are displayed
            var allItems = _todoFunction.GetTodoListTexts();

            Assert.That(allItems.Contains("Task A"), "Active Todo 'Task B' is not displayed.");
            Assert.That(allItems.Contains("Task B"), "Active Todo 'Task C' is not displayed.");
            Assert.That(allItems.Contains("Task C"), "Active Todo 'Task A' is not displayed.");

            _test.Pass("Test case: Filter Completed Todos passed successfully.");
        }

        [Test]
        public void TC014_AddEmptyTodo()
        {
            _todoFunction.AddTodoItem(string.Empty);

            bool isInputFieldCleared = _genericFunction.IsElementVisible(_todoPage.TxtNewTodo_Locator);
            Assert.That(isInputFieldCleared, "An empty Todo is added.");

            _test.Pass("Test case: Filter Completed Todos passed successfully.");
        }

        [Test]
        public void TC015_VerifyClearCompletedButtonVisibility()
        {
            string todoItem = "Clear This Task";
            _todoFunction.AddTodoItem(todoItem);
            _todoFunction.CompleteTodoItem(todoItem);
            
            bool isButtonVisible = _genericFunction.IsElementVisible(_todoPage.ClearCompleteButton_Locator);
            Assert.That(isButtonVisible, "'Clear completed' button is not visible for completed tasks.");

            _test.Pass("Test case: Verify Clear Completed Button passed successfully.");
        }

        [Test]
        public void TC016_InvalidEditTodoItemEmpty()
        {
            string todoItem = "Invalid Edit Test";
            string todoItemInvalid = "";

            _todoFunction.AddTodoItem(todoItem);
            _todoFunction.EditTodoItem(todoItem, todoItemInvalid); 

            bool isAbsent = _genericFunction.IsElementAbsent(_todoPage.GetTodoLabel_Locator(todoItemInvalid));
            Assert.That(isAbsent, $"Todo item '{todoItem}' was incorrectly edited to invalid value.");

            _test.Pass("Test case: Edit todo Item to Empty passed successfully.");
        }

        [Test]
        public void TC017_InvalidEditTodoItemSingleChar()
        {
            string todoItem = "Invalid Edit Test";
            string todoItemInvalid = "A";

            _todoFunction.AddTodoItem(todoItem);
            _todoFunction.EditTodoItem(todoItem, todoItemInvalid);

            bool isAbsent = _genericFunction.IsElementAbsent(_todoPage.GetTodoLabel_Locator(todoItemInvalid));
            Assert.That(isAbsent, $"Todo item '{todoItem}' was incorrectly edited to invalid value.");

            _test.Pass("Test case: Edit todo Item to Single Character passed successfully.");
        }

    }
}
