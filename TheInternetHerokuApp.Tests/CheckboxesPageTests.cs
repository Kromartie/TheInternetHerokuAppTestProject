using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using TheInternetHerokuApp.Core;
using TheInternetHerokuApp.Core.Pages;

namespace TheInternetHerokuApp.Tests;

// Tests for Checkboxes page functionality
public class CheckboxesPageTests
{
    // Page objects
    private CheckBoxesPage _checkBoxesPage = null!;

    public CheckboxesPageTests()
    {
        _checkBoxesPage = TestContainer.Host.Services.GetService<CheckBoxesPage>()!;
    }

    // Navigate to page before tests
    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        await _checkBoxesPage.NavigateToAsync();
    }
    
    // Test that verifies the page title
    [Test]
    public async Task ShouldDisplayTitle()
    {
        var header = await _checkBoxesPage.GetHeaderTextAsync();
        header.Should().Be("Checkboxes");
    }

    // Test that checks and verifies the first checkbox
    [Test]
    public async Task ShouldCheckFirstCheckBox()
    {
        await _checkBoxesPage.ToggleCheckBoxAsync(0);
        Thread.Sleep(1000); // Wait for the checkbox to be checked
        var isChecked = await _checkBoxesPage.IsCheckBoxCheckedAsync(0);
        isChecked.Should().BeTrue();
    }

    // Test that unchecks and verifies the second checkbox
    [Test]
    public async Task ShouldUncheckSecondCheckBox()
    {
        await _checkBoxesPage.ToggleCheckBoxAsync(1);
        Thread.Sleep(1000); // Wait for the checkbox to be checked
        var isChecked = await _checkBoxesPage.IsCheckBoxCheckedAsync(1);
        isChecked.Should().BeFalse();
    }
}
