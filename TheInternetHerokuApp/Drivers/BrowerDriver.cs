using Microsoft.Playwright;
using TheInternetHerokuApp.Configs;

namespace TheInternetHerokuApp.Drivers;


public class BrowserDriver : IAsyncDisposable
{
    public IPlaywright Playwright { get; private set; }
    public IBrowser Browser { get; private set; }
    public IBrowserContext Context { get; private set; }
    public IPage Page { get; private set; }

    private readonly TestSettings _settings;

    public BrowserDriver(TestSettings settings)
    {
        _settings = settings;
    }

    public async Task InitializeAsync()
    {
        Playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        Browser = await Playwright[_settings.Browser].LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = _settings.Headless
        });
        Context = await Browser.NewContextAsync();
        Page = await Context.NewPageAsync();
    }

    public async ValueTask DisposeAsync()
    {
        await Browser?.CloseAsync();
        Playwright?.Dispose();
    }
}
