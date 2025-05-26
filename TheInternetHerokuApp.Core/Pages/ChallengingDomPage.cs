using Microsoft.Extensions.Options;
using Microsoft.Playwright;
using TheInternetHerokuApp.Core.Configs;
using TheInternetHerokuApp.Core.Drivers;
using TheInternetHerokuApp.Core.Pages.Base;

namespace TheInternetHerokuApp.Core.Pages;

// Page object for the Challenging DOM example page
public class ChallengingDomPage : BasePage
{
    public ChallengingDomPage(IOptions<TestSettings> settings, PageProvider pageProvider)
        : base("challenging_dom", settings, pageProvider) { }

    // --Selectors--
    // Locator for the sub-header paragraph
    public ILocator SubHeader => _page.Locator("p");
    // Locators for buttons with different colors
    public ILocator BlueButton => _page.Locator("[class='button']");
    public ILocator RedButton => _page.Locator(".button.alert");
    public ILocator GreenButton => _page.Locator(".button.success");
    public ILocator AllButtons => _page.Locator(".button");
    // Locator for the canvas element
    public ILocator Canvas => _page.Locator("canvas");

    // --Methods--
    // Gets the sub-header text content
    public async Task<string> GetSubHeaderAsync() => await SubHeader.InnerTextAsync();

    // Gets all table header texts
    public async Task<IReadOnlyList<string>> GetTableHeadersAsync()
    {
        return await _page.Locator("table thead tr th").AllInnerTextsAsync();
    }

    // Gets the number of rows in the table
    public async Task<int> GetTableRowCountAsync()
    {
        return await _page.Locator("table tbody tr").CountAsync();
    }

    // Gets the text content of a specific cell by row and column
    public async Task<string> GetCellValueAsync(int row, int col)
    {
        var cell = _page.Locator($"table tbody tr:nth-child({row}) td:nth-child({col})");
        return await cell.InnerTextAsync();
    }

    // Gets the canvas element ID
    public async Task<string> GetCanvasId()
    {
        return await Canvas.EvaluateAsync<string>("canvas => canvas.id");
    }

    // Gets the canvas as a data URL
    public async Task<string> GetCanvasDataUrl()
    {
        return await Canvas.EvaluateAsync<string>("canvas => canvas.toDataURL()");
    }

    // Gets the canvas width
    public async Task<int> GetCanvasWidth()
    {
        return await Canvas.EvaluateAsync<int>("canvas => canvas.width");
    }

    // Gets the canvas height
    public async Task<int> GetCanvasHeight()
    {
        return await Canvas.EvaluateAsync<int>("canvas => canvas.height");
    }

    // Gets all link texts in the action column of the last row
    public async Task<IEnumerable<string>> GetActionCellLinksAsync()
    {
        // Select the last row's last cell (Action column)
        var actionCell = _page.Locator("table tbody tr:last-child td:last-child");

        // Find all links in that cell
        var links = actionCell.Locator("a");
        return await links.AllInnerTextsAsync();
    }
}
