using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using TheInternetHerokuApp.Core;
using TheInternetHerokuApp.Core.Pages;

namespace TheInternetHerokuApp.Tests;

// Tests for the main Landing page functionality
[TestFixture]
public class LandingPageTests
{
    // Page objects
    private readonly LandingPage _landingPage = null!;

    public LandingPageTests()
    {
        _landingPage = TestContainer.Host.Services.GetService<LandingPage>()!;
    }

    // Navigate to page before tests
    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        await _landingPage.NavigateAsync(TestContainer.Settings.BaseUrl);
    }

    // Test that verifies all example links are listed
    [Test]
    public async Task ShouldListAllLandingPageLinks()
    {
        // Get all links on the landing page
        var links = await _landingPage.GetAllLinksAsync();
        links.Should().NotBeEmpty();

        // Output each link for debugging
        foreach (var link in links)
        {
            Console.WriteLine($"Found link: {link}");
        }
    }

    // Test that verifies the page title
    [Test]
    public async Task ShouldDisplayTitle()
    {
        var title = await _landingPage.GetTitleAsync();
        title.Should().Contain("Welcome to the-internet");
    }

    // Test that verifies the sub-header content
    [Test]
    public async Task ShouldDisplaySubheader()
    {
        var subheader = await _landingPage.GetSubHeaderAsync();
        subheader.Should().Contain("Available Examples");
    }   
}

