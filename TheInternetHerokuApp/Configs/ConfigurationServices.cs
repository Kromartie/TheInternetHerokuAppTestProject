using Microsoft.Extensions.Configuration;

namespace TheInternetHerokuApp.Configs;
public static class ConfigurationService
{
    public static TestSettings GetTestSettings()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        var settings = new TestSettings();
        config.GetSection("TestSettings").Bind(settings);
        return settings;
    }
}