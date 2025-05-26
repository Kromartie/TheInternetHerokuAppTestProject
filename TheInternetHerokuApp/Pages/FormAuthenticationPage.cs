using Microsoft.Playwright;
using TheInternetHerokuApp.Drivers;

namespace TheInternetHerokuApp.Pages;

public class FormAuthenticationPage
{
    private readonly IPage _page;

    public FormAuthenticationPage(PageProvider pageProvider)
    {
        _page = pageProvider.GetPage();
    }

    public ILocator UsernameTextbox => _page.Locator("#username");
    public ILocator PasswordTextbox => _page.Locator("#password");
    public ILocator LoginButton => _page.Locator("button[type='submit']");
    public ILocator ErrorMessage => _page.Locator("#flash");
    public ILocator Header => _page.Locator("h2");
    public ILocator SubHeader => _page.Locator("h4.subheader");
    public ILocator UsernameLabel => _page.GetByText("Username", new() { Exact = true });
    public ILocator PasswordLabel => _page.GetByText("Password", new() { Exact = true });

    public async Task<string> GetHeaderAsync() => await Header.InnerTextAsync();
    public async Task<string> GetSubHeaderAsync() => await SubHeader.InnerTextAsync();

    public async Task Login(string username, string password)
    {
        await UsernameTextbox.FillAsync(username);
        await PasswordTextbox.FillAsync(password);
        await LoginButton.ClickAsync();
    }
}

