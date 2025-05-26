using Microsoft.Extensions.Options;
using Microsoft.Playwright;
using TheInternetHerokuApp.Core.Configs;
using TheInternetHerokuApp.Core.Drivers;
using TheInternetHerokuApp.Core.Pages.Base;

namespace TheInternetHerokuApp.Core.Pages;

// Page object for the Context Menu example page
public class ContextMenuPage : BasePage
{
    public ContextMenuPage(IOptions<TestSettings> settings, PageProvider pageProvider) 
        : base("context_menu", settings, pageProvider) { }
    
    // --Selectors--
    // Locator for the page header
    //public ILocator Header => _page.Locator("h3");
    // Locator for the sub-header paragraph
    public ILocator SubHeader => _page.Locator("p");
    // Locator for the right-click target box
    public ILocator Box => _page.Locator("#hot-spot");

    // --Methods--
    // Gets the header text content
    //public async Task<string> GetHeaderTextAsync() => await Header.InnerTextAsync();
    // Gets all sub-header text contents
    public async Task<IEnumerable<string>> GetSubHeaderTextAsync() => await SubHeader.AllInnerTextsAsync();
    // Gets the text of the context menu box
    public async Task<string> GetBoxTextAsync() => await Box.InnerTextAsync();
    // Performs a right-click action on the box
    public async Task RightClickOnBoxAsync() => await Box.ClickAsync(new LocatorClickOptions { Button = MouseButton.Right });
    public async Task LeftClickOnBoxAsync() => await Box.ClickAsync(new LocatorClickOptions { Button = MouseButton.Left });
}

