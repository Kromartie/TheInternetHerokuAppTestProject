using Microsoft.Extensions.Options;
using Microsoft.Playwright;
using TheInternetHerokuApp.Core.Configs;
using TheInternetHerokuApp.Core.Drivers;
using TheInternetHerokuApp.Core.Pages.Base;

namespace TheInternetHerokuApp.Core.Pages;

// Page object for the Add/Remove Elements example page
public class AddRemoveElementsPage : BasePage
{
    public AddRemoveElementsPage(IOptions<TestSettings> settings, PageProvider pageProvider) 
        : base("add_remove_elements/", settings, pageProvider) { }

    public ILocator Header => _page.Locator("h3");

    // --Selectors--
    // ADD selectors examples
    public ILocator AddElementButton => _page.Locator("button", new() { HasTextString = "Add Element" });
    //public ILocator AddElementButton => _page.Locator("button[onclick='addElement()']");
    //public ILocator AddElementButton => _page.Locator("div.example > button");
    //public ILocator AddElementButton => _page.Locator("div.example > button").Locator("text=Add Element");


    // DELETE selectors examples
    public ILocator DeleteElementButton => _page.Locator("button", new() { HasTextString = "Delete" });
    //public ILocator DeleteElementButton => _page.Locator("button[onclick='deleteElement()']");
    //public ILocator DeleteElementButton => _page.Locator("div#elements > button");
    //public ILocator DeleteElementButton => _page.Locator("div#elements > button").Locator("text=Delete");
    //public ILocator DeleteElementButton => _page.Locator("button.added-manually");

    // --Methods--
    // Clicks the Add Element button
    public async Task ClickAddElementAsync() => await AddElementButton.ClickAsync();
    // Gets the count of Delete buttons on the page
    public async Task<int> GetDeleteButtonCountAsync() => await DeleteElementButton.CountAsync();

    // Clicks a Delete button at the specified index
    public async Task ClickDeleteButtonAsync(int index = 0)
    {
        var deleteButtons = DeleteElementButton;
        if (await deleteButtons.CountAsync() > index)
        {
            await deleteButtons.Nth(index).ClickAsync();
        }
    }
}
