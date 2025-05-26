# TheInternetHerokuAppTestProject

## Overview

This project is an automated testing framework designed for "The Internet Heroku App" ([https://the-internet.herokuapp.com](https://the-internet.herokuapp.com)) - a website containing various web elements and scenarios commonly used for practicing test automation. The framework demonstrates how to automate web UI testing using Microsoft Playwright with C# and the Page Object Model design pattern.

## Features

- **Modern Tech Stack**: Built with .NET 8, C# 12.0, and Microsoft Playwright for modern browser automation
- **Page Object Model**: Maintains clear separation between test logic and page interactions
- **Fluent Assertions**: Uses FluentAssertions for readable test validations
- **Dependency Injection**: Leverages Microsoft's dependency injection for service management
- **Configuration Management**: Supports external configuration via appsettings.json
- **Cross-Browser Testing**: Configurable to run tests across different browsers (Chromium, Firefox, WebKit)
- **Headless Mode Support**: Can run in both headless and headed browser modes

## Automated Test Scenarios

The framework includes automated tests for multiple page examples from the website:

- **A/B Testing**: Verifies content variations
- **Add/Remove Elements**: Tests dynamic element addition and removal
- **Basic Authentication**: Validates authentication flows with valid and invalid credentials
- **Broken Images**: Detects and counts broken images on the page
- **Challenging DOM**: Demonstrates handling of dynamic content and complex DOM structures
- **Checkboxes**: Tests checkbox interactions
- **Context Menu**: Verifies right-click context menu behavior
- **Disappearing Elements**: Tests elements that appear and disappear
- **Form Authentication**: Tests login functionality with various credentials
- *Pending more tests*

## Project Structure

The solution consists of two main projects:

### 1. TheInternetHerokuApp.Core

Contains the core framework components:

- **Pages**: Page objects representing website pages
  - **Base**: Base classes for page objects
  - Page-specific classes (LandingPage, BasicAuthPage, etc.)
- **Configs**: Configuration classes and services
- **Drivers**: Browser and page management
- **Helpers**: Utility classes for common tasks (e.g., DialogHandler)

### 2. TheInternetHerokuApp.Tests

Contains the test cases:

- Test fixtures organized by page/feature
- Global test setup and teardown

## Getting Started

### Prerequisites

- .NET 8 SDK or later
- Visual Studio 2022 or other compatible IDE

### Setup

1. Clone the repository
2. Restore NuGet packages
3. Configure settings in appsettings.json

```json
{
  "TestSettings": {
    "BaseUrl": "https://the-internet.herokuapp.com/",
    "Browser": "chromium",
    "Headless": false
  }
}
```

### Running Tests

From Visual Studio:
- Open Test Explorer and run tests individually or as a group

From command line:
```
dotnet test
```

## Design Patterns and Practices

This framework demonstrates several best practices for UI test automation:

- **Page Object Model**: Each page has its own class to encapsulate its behavior
- **Test Isolation**: Tests don't depend on each other
- **Dependency Injection**: Services are resolved through the container
- **Async/Await**: Modern asynchronous patterns for browser interactions
- **Exception Handling**: Robust handling of errors and edge cases
- **Global Setup/Teardown**: Efficient resource management across tests

## Key Components

- **TestContainer**: Centralized management of test dependencies and browser state
- **BrowserDriver**: Handles browser lifecycle and provides access to Playwright APIs
- **BasePage**: Common functionality shared by all page objects
- **Start.cs**: Global setup and teardown for the test suite

## Authentication Testing Example

The framework demonstrates different approaches to testing authentication:

```csharp
// Testing successful authentication
await _basicAuthPage.NavigateWithCredentialsAsync("admin", "admin");
var header = await _basicAuthPage.GetTitleAsync();
header.Should().Contain("Basic Auth");

// Testing failed authentication with a helper method
bool authSucceeded = await _basicAuthPage.TryAuthenticateAsync("wrong", "wrong");
authSucceeded.Should().BeFalse();

// Testing failed authentication with exception handling
try {
    await _basicAuthPage.NavigateWithCredentialsAsync("wrong", "wrong");
    // This should not execute if authentication fails as expected
} 
catch (PlaywrightException ex) when (ex.Message.Contains("ERR_INVALID_AUTH_CREDENTIALS")) {
    // Expected behavior for invalid credentials
    Assert.Pass("Authentication properly failed with invalid credentials");
}
```

## Extending the Framework

To add tests for new pages:

1. Create a new Page class in Core/Pages
2. Register it in TestContainer.cs
3. Create test fixtures in the Tests project![image](https://github.com/user-attachments/assets/6aa36e2c-ae1e-444a-b7fa-0644fe35b122)


## Contribution
Contributions are welcome! Feel free to fork this repository, add new tests, or improve existing ones, and submit a pull request.

## License
See the repository's license file for more details.
