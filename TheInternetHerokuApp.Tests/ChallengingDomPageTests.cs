using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using NUnit.Framework.Internal;
using TheInternetHerokuApp.Core;
using TheInternetHerokuApp.Core.Pages;

namespace TheInternetHerokuApp.Tests;

// Tests for Challenging DOM page functionality
[TestFixture]
public class ChallengingDomPageTests
{
    // Page objects
    private readonly ChallengingDomPage _challengingDomPage = null!;

    public ChallengingDomPageTests()
    {
        _challengingDomPage = TestContainer.Host.Services.GetService<ChallengingDomPage>()!;
    }

    // Navigate to page before tests
    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        await _challengingDomPage.NavigateToAsync();
    }

    // Test that verifies the page title
    [Test]
    public async Task ShouldDisplayTitle()
    {
        var header = await _challengingDomPage.GetTitleAsync();
        header.Should().Be("Challenging DOM");
    }

    // Test that verifies the sub-header content
    [Test]
    public async Task ShouldDisplaySubHeader()
    {
        var subHeader = await _challengingDomPage.GetSubHeaderAsync();
        subHeader.Should().Contain(_expectedSubheading);
    }

    // Expected sub-header text for verification
    private readonly string _expectedSubheading =
                "The hardest part in automated web testing is finding the best locators (e.g., " +
                "ones that well named, unique, and unlikely to change). It's more often than not that the " +
                "application you're testing was not built with this concept in mind. This example demonstrates " +
                "that with unique IDs, a table with no helpful locators, and a canvas element.";

    // Test that checks the number of buttons
    [Test]
    public async Task ShouldDisplayButtonsCount()
    {
        var buttons = await _challengingDomPage.AllButtons.CountAsync();
        buttons.Should().Be(3);
    }

    // Test that checks if blue button is visible
    [Test]
    public async Task ShouldDisplayBlueButton()
    {
        var blueButton = await _challengingDomPage.BlueButton.IsVisibleAsync();
        blueButton.Should().BeTrue();
    }

    // Test that checks if red button is visible
    [Test]
    public async Task ShouldDisplayRedButton()
    {
        var redButton = await _challengingDomPage.RedButton.IsVisibleAsync();
        redButton.Should().BeTrue();
    }

    // Test that checks if green button is visible
    [Test]
    public async Task ShouldDisplayGreenButton()
    {
        var greenButton = await _challengingDomPage.GreenButton.IsVisibleAsync();
        greenButton.Should().BeTrue();
    }

    // Test that verifies table headers
    [Test]
    public async Task ShouldDisplayTableHeaders()
    {
        var headers = await _challengingDomPage.GetTableHeadersAsync();
        headers.Should().Contain(new[] { "Lorem", "Ipsum", "Dolor", "Sit", "Amet", "Diceret", "Action"});
    }

    // Test that checks the number of table rows
    [Test]
    public async Task ShouldDisplayTableRowCount()
    {
        var rowCount = await _challengingDomPage.GetTableRowCountAsync();
        rowCount.Should().Be(10);
    }

    // Test that verifies a specific cell value
    [Test]
    public async Task ShouldDisplayCellValue()
    {
        var cellValue = await _challengingDomPage.GetCellValueAsync(1, 1);
        cellValue.Should().Be("Iuvaret0");
    }

    // Test that checks canvas dimensions
    [Test]
    public async Task ShouldHaveCanvasDimensions()
    {
        var canvas = await _challengingDomPage.Canvas.IsVisibleAsync();
        canvas.Should().BeTrue();

        _challengingDomPage.GetCanvasWidth().Result.Should().BeGreaterThan(0);
        _challengingDomPage.GetCanvasHeight().Result.Should().BeGreaterThan(0);
    }

    // Test that verifies the canvas ID
    [Test]
    public async Task ShouldHaveCanvasId()
    {
        var canvasId = await _challengingDomPage.GetCanvasId();
        canvasId.Should().BeEquivalentTo("canvas");
    }

    // Test that verifies action cell links
    [Test]
    public async Task ShouldDisplayActionCellLinks()
    {
        var actionCellLinks = await _challengingDomPage.GetActionCellLinksAsync();
        actionCellLinks.Should().NotBeEmpty();
        actionCellLinks.Should().Contain("edit");
        actionCellLinks.Should().Contain("delete");
    }
}
