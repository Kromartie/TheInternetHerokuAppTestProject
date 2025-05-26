using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using TheInternetHerokuApp.Core.Configs;
using TheInternetHerokuApp.Core.Drivers;
using TheInternetHerokuApp.Core.Pages;

namespace TheInternetHerokuApp.Core;

// Static container for test dependencies and configuration
public static class TestContainer
{
    // Dependency injection container
    public static IHost Host { get; set; }
    // Browser driver
    public static BrowserDriver Driver { get; set; }
    // Configuration
    public static TestSettings Settings { get; set; }

    // Initialise the test environment with configuration and services
    public static async Task InitializeAsync(string environment = null)
    {
        // Create and configure the host builder
        Host = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder().ConfigureAppConfiguration((context, config) =>
        {
            // Add configuration from appsettings.json
            config.AddJsonFile("appsettings.json");
        })
        .ConfigureServices((context, services) =>
        {
            // Configure test settings from configuration
            var configuration = context.Configuration.GetSection("TestSettings");
            services.Configure<TestSettings>(configuration);
            services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<TestSettings>>().Value);

            // Register drivers as singletons
            services.AddSingleton<BrowserDriver>();
            services.AddSingleton<PageProvider>();
            
            // Register page objects as transient services
            services.AddTransient<LandingPage>();
            services.AddTransient<FormAuthenticationPage>();
            services.AddTransient<ABTestVariation1Page>();
            services.AddTransient<AddRemoveElementsPage>();
            services.AddTransient<BasicAuthPage>();
            services.AddTransient<BrokenImagesPage>();
            services.AddTransient<ChallengingDomPage>();
            services.AddTransient<CheckBoxesPage>();
            services.AddTransient<ContextMenuPage>();
            services.AddTransient<DisappearingElementsPage>();
        })
        .Build();

        // Get services from DI container
        Settings = Host.Services.GetService<TestSettings>()!;
        Driver = Host.Services.GetService<BrowserDriver>()!;

        // Initialise the browser
        await Driver.InitialiseAsync();
    }
}
