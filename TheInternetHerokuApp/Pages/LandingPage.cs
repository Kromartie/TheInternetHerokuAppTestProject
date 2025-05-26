using Microsoft.Playwright;
using TheInternetHerokuApp.Drivers;

namespace TheInternetHerokuApp.Pages;

public class LandingPage
{
    private readonly IPage _page;

    public LandingPage(PageProvider pageProvider)
    {
        _page = pageProvider.GetPage();
    }

    public ILocator Header => _page.Locator("h1");
    public ILocator SubHeader => _page.Locator("h2");
    public ILocator AllLinks => _page.Locator("ul li a");
    public ILocator GetLinkByText(string linkText) => _page.Locator($"a:has-text('{linkText}')");

    public async Task<string> GetHeaderAsync() => await Header.InnerTextAsync();
    public async Task<string> GetSubHeaderAsync() => await SubHeader.InnerTextAsync();
    public async Task<IEnumerable<string>> GetAllLinksAsync() => await _page.Locator("ul li a").AllInnerTextsAsync();

    public async Task NavigateAsync(string url) => await _page.GotoAsync(url);
    public async Task ClickLinkAsync(string linkText) => await GetLinkByText(linkText).ClickAsync();
}
