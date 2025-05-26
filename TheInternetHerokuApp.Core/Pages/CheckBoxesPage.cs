using Microsoft.Extensions.Options;
using Microsoft.Playwright;
using TheInternetHerokuApp.Core.Configs;
using TheInternetHerokuApp.Core.Drivers;
using TheInternetHerokuApp.Core.Pages.Base;

namespace TheInternetHerokuApp.Core.Pages;

// Page object for the Checkboxes example page
public class CheckBoxesPage : BasePage
{
    public CheckBoxesPage(IOptions<TestSettings> settings, PageProvider pageProvider) 
        : base("checkboxes", settings, pageProvider) { }

    // --Selectors--
    // Locator for the first checkbox using XPath
    public ILocator CheckBox1 => _page.Locator("(//input[@type='checkbox'])[1]");
    // Locator for the second checkbox using XPath
    public ILocator CheckBox2 => _page.Locator("(//input[@type='checkbox'])[2]");
    // Locator for all checkboxes using XPath
    public ILocator AllCheckBoxes => _page.Locator("//input[@type='checkbox']");
    
    // --Methods--
    // Gets the header text content
    public async Task<string> GetHeaderTextAsync() => await Header.InnerTextAsync();
    
    // Checks if a checkbox at the specified index is checked
    public async Task<bool> IsCheckBoxCheckedAsync(int index)
    {
        var checkBox = AllCheckBoxes.Nth(index);
        return await checkBox.IsCheckedAsync();
    }

    // Toggles a checkbox at the specified index
    public async Task ToggleCheckBoxAsync(int index)
    {
        var checkBox = AllCheckBoxes.Nth(index);
        await (await checkBox.IsCheckedAsync() ? checkBox.UncheckAsync() : checkBox.CheckAsync());
    }
}
