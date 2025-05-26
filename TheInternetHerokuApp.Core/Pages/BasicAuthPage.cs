using Microsoft.Extensions.Options;
using Microsoft.Playwright;
using TheInternetHerokuApp.Core.Configs;
using TheInternetHerokuApp.Core.Drivers;
using TheInternetHerokuApp.Core.Pages.Base;

namespace TheInternetHerokuApp.Core.Pages;

// Page object for the Basic Auth example page
public class BasicAuthPage : BasePage
{
    public BasicAuthPage(IOptions<TestSettings> settings, PageProvider pageProvider)
        : base("basic_auth", settings, pageProvider) { }

    // --Selectors--
    // Locator for the sub-header paragraph
    public ILocator SubHeader => _page.Locator("p");
    // Locator for the unauthorized message
    public ILocator NotAuthorizedMessage => _page.Locator("Body");

    // --Methods--  
    // Gets the message text content
    public async Task<string> GetMessageAsync() => await SubHeader.InnerTextAsync();
    // Gets the unauthorized message text
    public async Task<string> GetNotAuthorizedMessageAsync() => await NotAuthorizedMessage.InnerTextAsync();

    // Navigate with HTTP Basic Authentication credentials in URL
    public async Task NavigateWithCredentialsAsync(string username, string password)
    {
        // Extract the host and path from the full URL
        var uri = new Uri(Url);
        var authUrl = $"{uri.Scheme}://{username}:{password}@{uri.Host}{uri.PathAndQuery}";
        await _page.GotoAsync(authUrl);
    }

    public async Task<bool> TryAuthenticateAsync(string username, string password)
    {
        try
        {
            // Attempt navigation with credentials
            await NavigateWithCredentialsAsync(username, password);

            // Check if we have access to the protected content
            try
            {
                var header = await Header.InnerTextAsync();
                var message = await SubHeader.InnerTextAsync();

                // If both elements have the expected content, authentication succeeded
                return header.Contains("Basic Auth") && message.Contains("Congratulations");
            }
            catch
            {
                // Couldn't access content - authentication failed
                return false;
            }
        }
        catch
        {
            // Navigation exception - authentication failed
            return false;
        }
    }
}
