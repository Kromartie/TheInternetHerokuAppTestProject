using Microsoft.Playwright;

namespace TheInternetHerokuApp.Drivers;
public class PageProvider
{
    private readonly BrowserDriver _browserDriver;

    public PageProvider(BrowserDriver browserDriver)
    {
        _browserDriver = browserDriver;
    }

    public IPage GetPage()
    {
        return _browserDriver.Page;
    }
}

