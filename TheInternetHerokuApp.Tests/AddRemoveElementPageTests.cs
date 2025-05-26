using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using TheInternetHerokuApp.Core;
using TheInternetHerokuApp.Core.Pages;

namespace TheInternetHerokuApp.Tests;

// Tests for Add/Remove Element page functionality
public class AddRemoveElementPageTests
{
    // Page objects
    private readonly AddRemoveElementsPage _addRemovePage;

    public AddRemoveElementPageTests()
    {
        _addRemovePage = TestContainer.Host.Services.GetService<AddRemoveElementsPage>()!;
    }

    // Navigate to page before tests
    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        await _addRemovePage.NavigateToAsync();
    }

    // Test that verifies adding and removing elements
    [Test]
    public async Task ShouldAddAndRemoveElements()
    {
        // Add two elements
        await _addRemovePage.ClickAddElementAsync();
        await _addRemovePage.ClickAddElementAsync();

        // Check that two elements were added
        var countAfterAdd = await _addRemovePage.GetDeleteButtonCountAsync();
        countAfterAdd.Should().Be(2);

        // Remove one element
        await _addRemovePage.ClickDeleteButtonAsync();

        // Check that one element remains
        var countAfterDelete = await _addRemovePage.GetDeleteButtonCountAsync();
        countAfterDelete.Should().Be(1);
    }
}
