using Microsoft.Extensions.Options;
using Microsoft.Playwright;
using TheInternetHerokuApp.Core.Configs;
using TheInternetHerokuApp.Core.Drivers;

namespace TheInternetHerokuApp.Core.Pages.Base;

// Base class for all page objects in the application
public abstract class BasePage
{
    // Url path
    public readonly string Url;
    // Playwright page object
    protected readonly IPage _page;
    
    public BasePage(string urlPath, IOptions<TestSettings> settings, PageProvider pageProvider)
    {
        Url = settings.Value.BaseUrl + urlPath;
        _page = pageProvider.GetPage();
    }

    // --Selectors--
    // Default header locator (h3) that can be overridden by derived classes
    public ILocator Header => _page.Locator("h3");

    // --Methods--
    // Gets the page title text from header
    public async Task<string> GetTitleAsync() => await Header.InnerTextAsync();
    // Navigates to the page URL
    public async Task NavigateToAsync() => await _page.GotoAsync(Url);
}
