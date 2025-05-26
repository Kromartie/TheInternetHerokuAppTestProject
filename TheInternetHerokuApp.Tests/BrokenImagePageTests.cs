using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using TheInternetHerokuApp.Core;
using TheInternetHerokuApp.Core.Pages;

namespace TheInternetHerokuApp.Tests;

// Tests for Broken Images page functionality
[TestFixture]
public class BrokenImagePageTests
{
    // Page objects
    private readonly BrokenImagesPage _brokenImagesPage = null!;

    public BrokenImagePageTests()
    {
        _brokenImagesPage = TestContainer.Host.Services.GetService<BrokenImagesPage>()!;
    }

    // Navigate to page before tests
    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        await _brokenImagesPage.NavigateToAsync();
    }
    
    // Test that verifies the page title
    [Test]
    public async Task ShouldDisplayTitle()
    {
        var header = await _brokenImagesPage.GetTitleAsync();
        header.Should().Be("Broken Images");
    }
    
    // Test that counts broken images on the page
    [Test]
    public async Task ShouldCountBrokenImages()
    {
        var brokenCount = await _brokenImagesPage.CountBrokenImagesAsync();
        brokenCount.Should().Be(2);
    }
}
