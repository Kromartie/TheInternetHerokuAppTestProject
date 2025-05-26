using Microsoft.Extensions.Options;
using Microsoft.Playwright;
using TheInternetHerokuApp.Core.Configs;
using TheInternetHerokuApp.Core.Drivers;
using TheInternetHerokuApp.Core.Pages.Base;

namespace TheInternetHerokuApp.Core.Pages;

// Page object for the Broken Images example page
public class BrokenImagesPage: BasePage
{
    public BrokenImagesPage(IOptions<TestSettings> settings, PageProvider pageProvider) 
        : base("broken_images", settings, pageProvider) { }

    // --Selectors--
    // Locator for the sub-header paragraph
    public ILocator SubHeader => _page.Locator("p");

    // --Methods--
    // Gets all image elements on the page
    public async Task<IEnumerable<ILocator>> GetAllImagesAsync() => await _page.Locator("img").AllAsync();

    // Counts the number of broken images on the page
    public async Task<int> CountBrokenImagesAsync()
    {
        // Get all image locators
        var imageLocators = _page.Locator("img");
        int count = await imageLocators.CountAsync();
        int brokenCount = 0;

        // Check each image
        for (int i = 0; i < count; i++)
        {
            var elementHandle = await imageLocators.Nth(i).ElementHandleAsync();

            if (elementHandle is not null)
            {
                // JavaScript to check if image is broken (incomplete or has zero width)
                var isBroken = await _page.EvaluateAsync<bool>(
                    @"(img) => !img.complete || img.naturalWidth === 0",
                    elementHandle
                );

                if (isBroken)
                    brokenCount++;
            }
        }
        return brokenCount;
    }
}
