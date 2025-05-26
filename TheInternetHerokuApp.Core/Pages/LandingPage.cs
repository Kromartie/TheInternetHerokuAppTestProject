using Microsoft.Extensions.Options;
using Microsoft.Playwright;
using TheInternetHerokuApp.Core.Configs;
using TheInternetHerokuApp.Core.Drivers;
using TheInternetHerokuApp.Core.Pages.Base;

namespace TheInternetHerokuApp.Core.Pages;

// Page object representing the main landing page of The Internet Heroku App
public class LandingPage : BasePage
{
    public LandingPage(IOptions<TestSettings> settings, PageProvider pageProvider) 
        : base("", settings, pageProvider) { }

    // --Selectors--
    // Override the header locator from base class to target h1 instead of h3
    public new ILocator Header => _page.Locator("h1");
    // Locator for the page sub-header
    public ILocator SubHeader => _page.Locator("h2");
    // Locator for all available example links on the page
    public ILocator AllLinks => _page.Locator("ul li a");
    // Method to get a link by its text content
    public ILocator GetLinkByText(string linkText) => _page.Locator($"a:has-text('{linkText}')");

    // --Methods--
    // Gets the title text from the header
    public new async Task<string> GetTitleAsync() => await Header.InnerTextAsync();
    // Gets the sub-header text
    public async Task<string> GetSubHeaderAsync() => await SubHeader.InnerTextAsync();
    // Gets all example link texts as a collection
    public async Task<IEnumerable<string>> GetAllLinksAsync() => await _page.Locator("ul li a").AllInnerTextsAsync();
    // Navigates to a specific URL
    public async Task NavigateAsync(string url) => await _page.GotoAsync(url);
    // Clicks on a link with the specified text
    public async Task ClickLinkAsync(string linkText) => await GetLinkByText(linkText).ClickAsync();
}
