using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using TheInternetHerokuApp.Core;
using TheInternetHerokuApp.Core.Pages;

namespace TheInternetHerokuApp.Tests;

// Tests for Form Authentication page functionality
[TestFixture]
public class FormAuthenticationPageTests
{
    // Page objects
    private readonly FormAuthenticationPage _formAuthenticationPage;

    public FormAuthenticationPageTests()
    {
        _formAuthenticationPage = TestContainer.Host.Services.GetService<FormAuthenticationPage>()!;
    }

    // Navigate to page before tests
    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        await _formAuthenticationPage.NavigateToAsync();
    }

    // Test that verifies the page title
    [Test]
    public async Task ShouldDisplayTitle()
    {
        var header = await _formAuthenticationPage.GetTitleAsync();
        header.Should().Be("Login Page");
    }

    // Test that verifies the sub-header content
    [Test]
    public async Task ShouldDisplaySubHeader()
    {
        var subHeader = await _formAuthenticationPage.GetSubHeaderAsync();
        subHeader.Should().Contain("This is where you can log into the secure area.");
    }

    // Test that verifies username input functionality
    [Test]
    public async Task ShouldFillUsernameTextbox()
    {
        await _formAuthenticationPage.UsernameTextbox.FillAsync("myUsername");
        var value = await _formAuthenticationPage.UsernameTextbox.InputValueAsync();
        value.Should().Be("myUsername");
    }

    // Test that verifies password input functionality
    [Test]
    public async Task ShouldFillPasswordTextbox()
    {
        await _formAuthenticationPage.PasswordTextbox.FillAsync("myPassword");
        var value = await _formAuthenticationPage.PasswordTextbox.InputValueAsync();
        value.Should().Be("myPassword");
    }

    // Test that verifies login error for empty credentials
    [Test]
    public async Task ShouldClickLoginButtonAndSeeError()
    {
        await _formAuthenticationPage.Login("", ""); // Empty fields
        var error = await _formAuthenticationPage.ErrorMessage.InnerTextAsync();
        error.Should().Contain("Your username is invalid!");
    }
}

