using Microsoft.Extensions.Options;
using Microsoft.Playwright;
using TheInternetHerokuApp.Core.Configs;
using TheInternetHerokuApp.Core.Drivers;
using TheInternetHerokuApp.Core.Pages.Base;

namespace TheInternetHerokuApp.Core.Pages;

// Page object for the Form Authentication example page
public class FormAuthenticationPage : BasePage
{
    // Initialize with the login path
    public FormAuthenticationPage(IOptions<TestSettings> settings, PageProvider pageProvider)
        : base("login", settings, pageProvider) { }

    // Locator for the username input field
    public ILocator UsernameTextbox => _page.Locator("#username");
    // Locator for the password input field
    public ILocator PasswordTextbox => _page.Locator("#password");
    // Locator for the login submit button
    public ILocator LoginButton => _page.Locator("button[type='submit']");
    // Locator for the error/success flash message
    public ILocator ErrorMessage => _page.Locator("#flash");
    // Override the header locator from base class
    public new ILocator Header => _page.Locator("h2");
    // Locator for the sub-header
    public ILocator SubHeader => _page.Locator("h4.subheader");
    // Locator for the username label text
    public ILocator UsernameLabel => _page.GetByText("Username", new() { Exact = true });
    // Locator for the password label text
    public ILocator PasswordLabel => _page.GetByText("Password", new() { Exact = true });

    // --Methods--
    // Gets the title text from the header
    public new async Task<string> GetTitleAsync() => await Header.InnerTextAsync();
    // Gets the sub-header text
    public async Task<string> GetSubHeaderAsync() => await SubHeader.InnerTextAsync();

    // Performs login operation with provided credentials
    public async Task Login(string username, string password)
    {
        await UsernameTextbox.FillAsync(username);
        await PasswordTextbox.FillAsync(password);
        await LoginButton.ClickAsync();
    }
}

