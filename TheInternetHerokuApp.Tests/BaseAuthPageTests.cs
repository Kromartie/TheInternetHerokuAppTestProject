using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Playwright;
using NUnit.Framework;
using TheInternetHerokuApp.Core;
using TheInternetHerokuApp.Core.Pages;

namespace TheInternetHerokuApp.Tests;

// Tests for Basic Authentication functionality
[TestFixture]
public class BaseAuthPageTests
{
    // Page objects
    private readonly BasicAuthPage _basicAuthPage = null!;

    // Constructor gets page from DI container
    public BaseAuthPageTests()
    {
        _basicAuthPage = TestContainer.Host.Services.GetService<BasicAuthPage>()!;
    }

    // Test successful login with valid credentials
    [Test]
    public async Task ShouldLoginSuccessfullyWithValidCredentials()
    {
        // Navigate with valid credentials
        await _basicAuthPage.NavigateWithCredentialsAsync("admin", "admin");

        // Verify success elements are present
        var header = await _basicAuthPage.GetTitleAsync();
        var message = await _basicAuthPage.GetMessageAsync();

        header.Should().Contain("Basic Auth");
        message.Should().Contain("Congratulations");
    }

    // Test login failure with invalid credentials 
    [Test]
    public async Task ShouldFailWithInvalidCredentials()
    {
        // Use the helper method to check authentication result
        bool authSucceeded = await _basicAuthPage.TryAuthenticateAsync("wrong", "wrong");

        // Authentication should fail with invalid credentials
        authSucceeded.Should().BeFalse("Authentication should fail with invalid credentials");
    }

    // Test login failure with invalid credentials 2
    [Test]
    public async Task ShouldFailWithInvalidCredentials2()
    {
        try
        {
            // Attempt to navigate with invalid credentials
            await _basicAuthPage.NavigateWithCredentialsAsync("wrong", "wrong");

            // If we get here (no exception thrown), it means authentication somehow succeeded,
            // which would be unexpected. Still check if there's an unauthorized message.
            var bodyText = await _basicAuthPage.NotAuthorizedMessage.InnerTextAsync();
            bodyText.Should().Contain("Not authorized", "When authentication fails but navigation succeeds, page should show 'Not authorized'");

            // This would be unusual - we should log this case
            TestContext.WriteLine("WARNING: Navigation with invalid credentials succeeded but showed unauthorized message");
        }
        catch (PlaywrightException ex) when (ex.Message.Contains("ERR_INVALID_AUTH_CREDENTIALS"))
        {
            // This is the EXPECTED outcome - authentication was rejected at the browser level
            TestContext.WriteLine("Test PASSED: Browser properly rejected invalid credentials with error: " + ex.Message);
            Assert.Pass("Authentication properly failed with invalid credentials as expected");
        }

    }
}
