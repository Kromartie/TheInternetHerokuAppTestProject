using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using TheInternetHerokuApp.Core;
using TheInternetHerokuApp.Core.Pages;

namespace TheInternetHerokuApp.Tests;

// Tests for Disappearing Elements page functionality
public class DisappearingElementsPageTests
{
    // Page objects
    private readonly DisappearingElementsPage _disappearingElementsPage = null!;

    public DisappearingElementsPageTests()
    {
        _disappearingElementsPage = TestContainer.Host.Services.GetService<DisappearingElementsPage>()!;
    }

    // Navigate to page before tests
    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        await _disappearingElementsPage.NavigateToAsync();
    }

    // Test that verifies the page title
    [Test]
    public async Task ShouldDisplayTitle()
    {
        var header = await _disappearingElementsPage.GetTitleAsync();
        header.Should().Be("Disappearing Elements");
    }

    // Test that verifies the sub-header text
    [Test]
    public async Task ShouldDisplaySubHeader()
    {
        var subHeader = await _disappearingElementsPage.SubHeader.TextContentAsync();
        subHeader.Should().Be("This example demonstrates when elements on a page change by disappearing/reappearing on each page load.");
    }

    // Test that verifies all navigation elements are present
    [Test]
    public async Task ShouldDisplayAllElements()
    {
        // Get all navigation elements
        var buttons = await _disappearingElementsPage.GetElementsAsync();

        // Verify elements are present
        buttons.Should().NotBeNullOrEmpty();
        buttons.Count.Should().BeGreaterThan(4);

        // Verify the text content of each element
        var buttonTexts = await _disappearingElementsPage.GetElementsTextAsync();
        buttonTexts.Should().Contain(new[]
        {
            "Home",
            "About",
            "Contact Us",
            "Portfolio"
        });
    }
}
