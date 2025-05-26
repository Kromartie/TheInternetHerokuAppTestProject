using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using TheInternetHerokuApp.Core;
using TheInternetHerokuApp.Core.Pages;

namespace TheInternetHerokuApp.Tests;

// Tests for the A/B Test Variation page
[TestFixture]
public class ABTestVariation1PageTests
{
    // Page objects
    private readonly ABTestVariation1Page _abTestVariation1Page;

    public ABTestVariation1PageTests()
    {
        _abTestVariation1Page = TestContainer.Host.Services.GetService<ABTestVariation1Page>()!;
    }

    // Navigate to page before tests
    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        await _abTestVariation1Page.NavigateToAsync();
    }

    // Test that verifies the page title
    [Test]
    public async Task ShouldDisplayTitle()
    {
        var header = await _abTestVariation1Page.GetTitleAsync();
        TestContext.WriteLine(header);
        header.Should().BeOneOf("A/B Test Variation 1", "A/B Test Control");
    }

    // Test that verifies paragraph content
    [Test]
    public async Task ShouldDisplayParagraphText()
    {
        var paragraphText = await _abTestVariation1Page.GetParagraphTextAsync();
        paragraphText.Should().Contain(_expectedParagraphTexts);
    }

    // Expected text content for verification
    private readonly string _expectedParagraphTexts =
        "Also known as split testing. " +
        "This is a way in which businesses are able to simultaneously test and learn different versions " +
        "of a page to see which text and/or functionality works best towards a desired outcome " +
        "(e.g. a user action such as a click-through)";
}
