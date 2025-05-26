using Microsoft.Playwright;
using TheInternetHerokuApp.Core.Configs;

namespace TheInternetHerokuApp.Core.Drivers;

// Manages browser instance lifecycle using Playwright
public class BrowserDriver : IAsyncDisposable
{
    // Core Playwright components
    public IPlaywright Playwright { get; private set; }
    public IBrowser Browser { get; private set; }
    public IBrowserContext Context { get; private set; }
    public IPage Page { get; private set; }

    private readonly TestSettings _settings;

    // Initializes driver with configured settings
    public BrowserDriver(TestSettings settings)
    {
        _settings = settings;
    }

    // Sets up the browser, context and page
    public async Task InitialiseAsync()
    {
        Playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        Browser = await Playwright[_settings.Browser].LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = _settings.Headless
        });
        Context = await Browser.NewContextAsync();
        Page = await Context.NewPageAsync();
    }

    // Cleanup resources when done
    public async ValueTask DisposeAsync()
    {
        await Browser?.CloseAsync()!;
        Playwright?.Dispose();
    }
}
