using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using TheInternetHerokuApp.Core;
using TheInternetHerokuApp.Core.Helpers;
using TheInternetHerokuApp.Core.Pages;

namespace TheInternetHerokuApp.Tests;

// Tests for Context Menu page functionality
public class ContextMenuPageTests
{
    // Page objects
    private ContextMenuPage _contextMenuPage = null!;

    public ContextMenuPageTests()
    {
        _contextMenuPage = TestContainer.Host.Services.GetService<ContextMenuPage>()!;
    }

    // Navigate to page before tests
    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        await _contextMenuPage.NavigateToAsync();
    }

    // Test that verifies the page title
    [Test]
    public async Task ShouldDisplayTitle()
    {
        var header = await _contextMenuPage.GetTitleAsync();
        header.Should().Be("Context Menu");
    }

    // Test that verifies the instructional text
    [Test]
    public async Task ShouldDisplayBoxText()
    {
        var boxText = await _contextMenuPage.GetSubHeaderTextAsync();
        boxText.ElementAt(0).Should().BeEquivalentTo("Context menu items are custom additions that appear in the right-click menu.");
        boxText.ElementAt(1).Should().BeEquivalentTo("Right-click in the box below to see one called 'the-internet'. When you click it, it will trigger a JavaScript alert.");
    }

    // Test that verifies right-clicking triggers a JavaScript alert
    [Test]
    public async Task ShouldRightClickOnBox()
    {
        // Variable to store dialog message
        string dialogMessage = "";

        // Register dialog handler to capture alert text
        DialogHandler.RegisterDialogHandler(TestContainer.Driver.Page, msg => dialogMessage = msg);

        // Perform right-click action
        await _contextMenuPage.RightClickOnBoxAsync();
        await _contextMenuPage.LeftClickOnBoxAsync();

        // Verify the alert message
        dialogMessage.Should().Be("You selected a context menu");

        

    }
}
