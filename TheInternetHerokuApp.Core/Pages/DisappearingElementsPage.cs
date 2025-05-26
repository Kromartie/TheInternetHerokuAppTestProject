using Microsoft.Extensions.Options;
using Microsoft.Playwright;
using TheInternetHerokuApp.Core.Configs;
using TheInternetHerokuApp.Core.Drivers;
using TheInternetHerokuApp.Core.Pages.Base;

namespace TheInternetHerokuApp.Core.Pages;

// Page object for the Disappearing Elements example page
public class DisappearingElementsPage : BasePage
{
    public DisappearingElementsPage(IOptions<TestSettings> settings, PageProvider pageProvider) 
        : base("disappearing_elements", settings, pageProvider) { }

    // --Selectors--
    // Locator for the page description paragraph
    public ILocator SubHeader => _page.Locator("p");
    // Locators for individual navigation elements that may appear/disappear
    public ILocator Element1 => _page.Locator("#content > div > ul > li:nth-child(1) > a");
    public ILocator Element2 => _page.Locator("#content > div > ul > li:nth-child(2) > a");
    public ILocator Element3 => _page.Locator("#content > div > ul > li:nth-child(3) > a");
    public ILocator Element4 => _page.Locator("#content > div > ul > li:nth-child(4) > a");
    public ILocator Element5 => _page.Locator("#content > div > ul > li:nth-child(5) > a");
    // Locator for all button links on the page
    public ILocator ButtonLinks => _page.Locator("#content > div > ul > li > a");

    // --Methods--
    // Gets the text of all element links
    public async Task<IEnumerable<string>> GetElementsTextAsync() => await ButtonLinks.AllInnerTextsAsync();
    // Gets all element locators as a collection
    public async Task<IReadOnlyList<ILocator>> GetElementsAsync() => await ButtonLinks.AllAsync();
}
