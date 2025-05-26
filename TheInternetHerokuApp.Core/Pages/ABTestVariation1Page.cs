using Microsoft.Extensions.Options;
using Microsoft.Playwright;
using TheInternetHerokuApp.Core.Configs;
using TheInternetHerokuApp.Core.Drivers;
using TheInternetHerokuApp.Core.Pages.Base;

namespace TheInternetHerokuApp.Core.Pages;

// Page object for the A/B Test Variation 1 page
public class ABTestVariation1Page : BasePage
{
    public ABTestVariation1Page(IOptions<TestSettings> settings, PageProvider pageProvider)
        : base("abtest", settings, pageProvider) { }

    // --Selectors--
    // Locator for the test variation paragraph
    public ILocator ParagraphText => _page.Locator("div.example > p");

    // --Methods--
    // Gets the paragraph text content
    public async Task<string> GetParagraphTextAsync() => await ParagraphText.InnerTextAsync();
}
