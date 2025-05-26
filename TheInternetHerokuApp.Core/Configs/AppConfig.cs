namespace TheInternetHerokuApp.Core.Configs;

// Configuration class for test settings
public class TestSettings
{
    // Base URL
    public string BaseUrl { get; set; }
    
    // Browser type
    public string Browser { get; set; }
    
    // Headless mode flag
    public bool Headless { get; set; }
}