using Microsoft.Playwright;

namespace TheInternetHerokuApp.Core.Drivers;

// Provides access to the current Playwright page
public class PageProvider
{
    private readonly BrowserDriver _browserDriver;

    // Initialises with browser driver dependency
    public PageProvider(BrowserDriver browserDriver)
    {
        _browserDriver = browserDriver;
    }

    // Returns the current active page
    public IPage GetPage()
    {
        return _browserDriver.Page;
    }
}

