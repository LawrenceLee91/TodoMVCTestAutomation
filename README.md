# TodoMVC Test Automation

## Project Overview

This project provides automated testing for the TodoMVC application. The tests cover various scenarios such as adding, editing, completing, and deleting todos, as well as filtering and validating application behaviour.
The automation is implemented using Selenium WebDriver in C# with NUnit as the test framework.

------

## Table of Contents
1. [User Story](#User-Story)
2. [Acceptance Criteria](#Acceptance-Criteria)
3. [Technology Stack](#Technology-Stack)
4. [Project Structure](#Project-Structure)
5. [Installation and Setup](#Installation-and-Setup)
6. [Running the Tests](#Running-the-Tests)
7. [Test Cases](#Test-Cases)
8. [Reporting](#Reporting)
9. [Implementation Strategy](#Implementation-Strategy)
10. [Contact](#Contact)

------

## User Story
As a user, I want to manage my todos effectively so that I can stay organised. 

I should be able to:
- Add, delete, and edit todos.
- Mark todos as complete or incomplete.
- Filter todos by their status (All, Active, Completed).

------

## Acceptance Criteria

### High-Level Criteria

- **Add Todo**: Given the user is on the TodoMVC app, when they add a new todo item, then the item should appear in the list.
- **Edit Todo**: Given an existing todo, when the user edits the text, then the updated text should replace the old text.
- **Delete Todo**: Given an existing todo, when the user deletes it, then the item should no longer appear in the list.
- **Mark Todo as Complete**: Given an existing todo, when the user marks it as complete, then the item should appear as crossed out.
- **Filter Todos**: Given multiple todos with varying statuses, when the user applies a filter (Active, Completed, All), then only the relevant todos should appear.

### Detailed Criteria

#### TC001_AddTodoItem
- **Scenario**: Add a single todo item
  - **Given** the user is on the TodoMVC page 
  - **When** the user enters a todo item and presses "Enter" 
  - **Then** the new todo item should appear in the list 
  - **And** the active todo counter should increase by 1 
  - **And** the footer should display 

#### TC002_CompleteTodoItem
- **Scenario**: Mark a todo item as completed 
  - **Given** there is 1 active todo item in the list 
  - **When** the user clicks the checkbox next to the todo item 
  - **Then** the item should be marked as completed with strikethrough styling 
  - **And** the active todo counter should decrease by 1 

#### TC003_ClearCompletedTodos
- **Scenario**: Clear completed todo item 
  - **Given** there is 1 completed todo item in the list 
  - **When** the user clicks the "Clear completed" button 
  - **Then** all completed todo item(s) should be removed from the list 

#### TC004_AddMultipleTodoItems
- **Scenario**: Add multiple todo items 
  - **Given** the user is on the TodoMVC page 
  - **When** the user adds multiple todo items by entering text and pressing "Enter" for each 
  - **Then** all added todo items should appear in the list in the entered order 
  - **And** the active todo counter should increase by the number of added items 
 
#### TC005_MarkMultipleTodoItemsAsCompleted
- **Scenario**: Mark all todo items as completed 
  - **Given** there are multiple active todo items in the list 
  - **When** the user clicks the checkbox next to the todo item for each todo item 
  - **Then** all todo items should be marked as completed with strikethrough styling 
  - **And** the active todo counter should display 0 

#### TC006_MarkAndUmarkWithToggleAllButton
- **Scenario**: Mark all todo items as completed and unmark them using "Toggle All" button 
  - **Given** there are multiple active todo items in the list 
  - **When** the user clicks the "Toggle All" button 
  - **Then** all todo items should be marked as completed with strikethrough styling 
  - **And** the active todo counter should display 0
  - **When** the user clicks the "Toggle All" button 
  - **Then** all todo items should be unmarked with strikethrough styling removed
  - **And** the active todo counter should display the number of active todo items

#### TC007_ClearAllCompletedTodos
- **Scenario**: Clear all completed todo items 
  - **Given** all todo items are marked as completed 
  - **When** the user clicks the "Clear completed" button 
  - **Then** the list should be empty 
  - **And** the active todo counter should display 0 

#### TC008_EditTodoItem 
- **Scenario**: Edit a todo item 
  - **Given** a todo item is present in the list 
  - **When** the user double-clicks the item and edits the text with valid input 
  - **And** presses "Enter" to save the changes 
  - **Then** the updated todo item should reflect the new text in the list 

#### TC009_DeleteTodoItem
- **Scenario**: Delete a todo item 
  - **Given** a todo item is present in the list 
  - **When** the user clicks the delete button next to the item 
  - **Then** the item should be removed from the list 

#### TC010_VerifyActiveTodoCounter
- **Scenario**: Verify the active todo counter 
  - **Given** there are multiple active todo items in the list 
  - **When** the user deletes 1 todo and views the active counter 
  - **Then** the counter should display the correct number of active items 

#### TC011_FilterActiveTodos
- **Scenario**: Filter active todos 
  - **Given** there are both active and completed todo items in the list 
  - **When** the user clicks the "Active" filter 
  - **Then** only the active todo items should be displayed 

#### TC012_FilterCompletedTodos
- **Scenario**: Filter completed todos 
  - **Given** there are both active and completed todo items in the list 
  - **When** the user clicks the "Completed" filter 
  - **Then** only the completed todo items should be displayed

#### TC013_FilterAllTodos
- **Scenario**: Filter all todos 
  - **Given** there are both active and completed todo items in the list 
  - **When** the user clicks the "All" filter 
  - **Then** All todo items should be displayed 

#### TC014_AddEmptyTodo
- **Scenario**: Attempt to add an empty todo item 
  - **Given** the user is on the TodoMVC page 
  - **When** the user presses "Enter" with an empty input field 
  - **Then** no new todo item should be added to the list 

#### TC015_VerifyClearCompletedButtonVisibility
- **Scenario**: Verify visibility of the "Clear completed" button 
  - **Given** there is completed todo item in the list 
  - **When** the user looks for the "Clear completed" button 
  - **Then** the button should be displayed 

#### TC016_InvalidEditTodoItemEmpty
- **Scenario**: Perform invalid edit (edit a todo item to an empty string) 
  - **Given** a todo item is present in the list 
  - **When** the user edits the item to an empty string and presses "Enter" 
  - **Then** the todo item should not be updated 

#### TC017_InvalidEditTodoItemSingleChar
- **Scenario**: Perform invalid edit (edit a todo item to a single character) 
  - **Given** a todo item is present in the list 
  - **When** the user edits the item to a single character and presses "Enter" 
  - **Then** the todo item should not be updated 

------

## Technology Stack

- **IDE**: Visual Studio 2022
- **Programming Language**: C#
- **Test Framework**: NUnit
- **Automation Tool**: Selenium WebDriver
- **Reporting**: ExtentReports
- **Configuration Management**: appsettings.json

------
## Project Structure

```bash
TodoMVCTestAutomation/
├── Functions/
│   ├── GenericFunction.cs        # Generic reusable methods
│   ├── TodoFunction.cs           # Application-specific reusable methods
├── Pages/
│   ├── TodoPage.cs               # Page Object Model for TodoMVC
├── Tests/
│   ├── BaseTest.cs               # Test initialisation and teardown
│   ├── TodoTests.cs              # Test cases for TodoMVC
├── Utilities/
│   ├── ExtentReportUtil.cs       # Reporting utilities
│   ├── PathUtil.cs               # Directory and file path helpers
│   ├── ScreenshotUtil.cs         # Screenshot helper
├── Reports/                      # Generated reports and screenshots
│   ├── [Generated dynamically]
├── AppSetting.json               # Configuration file (e.g., BaseUrl, Browser)
├── README.txt                    # Documentation for the project
```

------

## Installation and Setup

### 1. Clone the Repository
Clone the repository from GitHub to your local machine:
```bash
git clone https://github.com/LawrenceLee91/law-ta-todomvc
```
### 2. Install Prerequisites

#### Required Tools
- Visual Studio 2022
- .Net SDK (9.0)

#### NuGet Packages
Use Visual Studion's **Nuget Package Manage** to install the following dependencies:
- <code>ExtentReport</code> (5.0.4)
- <code>Microsoft.Extensions.Configuration</code> (9.0.0)
- <code>Microsoft.Extensions.Configuration.Json</code> (9.0.0)
- <code>Microsoft.NET.Test.Sdk</code> (17.12.0)
- <code>NUnit</code> (4.2.2)
- <code>NUnit3TestAdapter</code> (4.6.0)
- <code>Selenium WebDriver</code> (4.27.0)

### 3. Install Dependencies via NuGet in Visual Studio
1. Open the solution in Visual Studio.
2. Navigate to **Tools** > **NuGet Package Manager** > **Manage NuGet Packages for Solution**.
3. In the NuGet Package Manager:
    - Select the **Browse** tab.
    - Search for the required package (e.g., ExtentReport).
    - Click Install for each package listed above.

4. Confirm installation for each dependency.

### 4. Update <code>appsettings.json</code>
```json
{
    "BaseUrl": "https://todomvc.com/examples/react/dist/",
    "Browser": "Chrome",
    "PageLoadTimeout": 0
}
```
Note: "Browser" can be "Chrome", "Edge" or "Firefox"

### 5. Build the Solution
Build the solution in Visual Studio.

------

## Running the Tests

1. Open the Test Explorer in Visual Studio (<code>Test > Test Explorer</code>).
2. Run all tests by clicking **Run** on TodoTest or select specifuc test to execute.
   
------

## Test Cases

The suite covers the following test cases:
- Add a single todo item.
- Mark a todo item as completed.
- Clear completed todo item.
- Add multiple todo items.
- Mark multiple todo items as completed.
- Mark all todo items as completed and unmark them using "Toggle All" button 
- Clear all completed todo items.
- Edit a todo item.
- Delete a todo item.
- Verify the active todo counter.
- Filter active todos.
- Filter completed todos.
- Attempt to add an empty todo item.
- Verify visibility of the "Clear completed" button.
- Perform invalid edit (edit a todo item to an empty string)
- Perform invalid edit (edit a todo item to a single character)

------

## Reporting

Test results are saved in the <code>Reports/</code> directory:
- **HTML Report**: Provides a detailed execution report.
- **Screenshots**: Captured for failed tests.

------

## Implementation Strategy

### 1. User Story Analysis
Each functionality (add, edit, filter, mark...) is mapped to individual test cases.
### 2.  Test Design
- **Page Object Model (POM)**:
Centralised element locators for the TodoMVC page in the <code>TodoPage.cs</code>. This ensures maintainability, as UI changes can be managed in a single file. 
- **Functions Layer**: Implemente actions, UI interactions and functions in <code>TodoFunction.cs</code>for operations like adding, deleting, and editing todos. This abstraction allows for reusability across different test cases and ensures that the code remains DRY (Don’t Repeat Yourself).
- **Test Layer**: Write separate test methods for each acceptance criterion in <code>TodoTest.cs</code>, ensuring that tests are independent and cover all the critical aspects of the application.
### 3. Assertions: 
Used NUnit's Assert to validate functionality and handle test results.
### 4. Test Execution:
- **Cross-Browser Compatibility**: The test suite is designed to run on multiple browsers (Chrome, Firefox, Edge) using Selenium WebDriver. 
- **Reporting**: Integrated ExtentReports for detailed test execution reports.

------

## Contact

**Name**: Lawrence Lee

**Email**: lee.lawrence0120@gmail.com

**GitHub**: https://github.com/LawrenceLee91

------








