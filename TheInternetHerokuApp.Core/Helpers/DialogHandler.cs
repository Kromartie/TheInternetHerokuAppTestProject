using Microsoft.Playwright;

namespace TheInternetHerokuApp.Core.Helpers;

// Utility class to handle JavaScript dialogs in Playwright tests
public static class DialogHandler
{
    // Registers a synchronous handler for dialog events
    public static void RegisterDialogHandler(IPage page, Action<string> onDialogMessage)
    {
        page.Dialog += async (_, dialog) =>
        {
            onDialogMessage(dialog.Message);
            await dialog.AcceptAsync();
        };
    }

    // Registers an asynchronous handler for dialog events
    public static void RegisterDialogHandler(IPage page, Func<string, Task> onDialogMessageAsync)
    {
        page.Dialog += async (_, dialog) =>
        {
            await onDialogMessageAsync(dialog.Message);
            await dialog.AcceptAsync();
        };
    }
}
