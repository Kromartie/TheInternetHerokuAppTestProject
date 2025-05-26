using NUnit.Framework;
using TheInternetHerokuApp.Core;

namespace TheInternetHerokuApp.Tests;

// Global setup fixture that runs once before all tests
[SetUpFixture]
public class Start
{
    // Initialise test environment before any tests run
    [OneTimeSetUp]
    public async Task GlobalSetup()
    {
        await TestContainer.InitializeAsync();
    }

    // Clean up resources after all tests complete
    [OneTimeTearDown]
    public async Task GlobalTeardown()
    {
        await TestContainer.Driver.DisposeAsync();
        await TestContainer.Host.StopAsync();
    }
}
