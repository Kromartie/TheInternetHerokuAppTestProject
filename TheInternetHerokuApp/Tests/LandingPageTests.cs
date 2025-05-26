using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;
using TheInternetHerokuApp.Configs;
using TheInternetHerokuApp.Drivers;
using TheInternetHerokuApp.Pages;

namespace TheInternetHerokuApp.Tests;

[TestFixture]
public class LandingPageTests
{
    private IHost _host;
    private BrowserDriver _driver;
    private LandingPage _landingPage;
    private TestSettings _settings;

    [OneTimeSetUp]
    public async Task GlobalSetup()
    {
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                var settings = ConfigurationService.GetTestSettings();
                services.AddSingleton(settings);
                services.AddSingleton<BrowserDriver>();
                services.AddSingleton<PageProvider>();
                services.AddTransient<LandingPage>();
            })
            .Build();

        _settings = _host.Services.GetService<TestSettings>();
        _driver = _host.Services.GetService<BrowserDriver>();
        await _driver.InitializeAsync();
        _landingPage = _host.Services.GetService<LandingPage>();

        await _landingPage.NavigateAsync(_settings.BaseUrl);
        await _landingPage.ClickLinkAsync("Form Authentication");
    }

    //[Test]
    //public async Task ShouldListAllLandingPageLinks()
    //{
    //    await _landingPage.NavigateAsync(_settings.BaseUrl);
    //    var links = await _landingPage.GetAllLinksAsync();

    //    links.Should().NotBeEmpty();
    //    foreach (var link in links)
    //    {
    //        Console.WriteLine($"Found link: {link}");
    //    }
    //}

    [Test]
    public async Task ShouldClickLink()
    {
        var title = await _driver.Page.TitleAsync();
        title.Should().Contain("The Internet");
    }

    [Test]
    public async Task ShouldDisplayTitle()
    {
        var title = await _driver.Page.Locator("h2").InnerTextAsync();
        title.Should().Contain("Login Page");
    }

    [Test]
    public async Task ShouldDisplaySubheader()
    {
        var subheader = await _driver.Page.Locator("h4.subheader").InnerTextAsync();
        var testString = "This is where you can log into the secure area. Enter tomsmith for the username and SuperSecretPassword! for the password. If the information is wrong you should see error messages.";
        subheader.Should().Contain(testString);
    }

    [Test]
    public async Task ShouldDisplayUsernameLabel()
    {
        var label = await _driver.Page.GetByText("Username", new() { Exact = true }).InnerTextAsync();
        label.Should().Contain("Username");
    }

    [Test]
    public async Task ShouldDisplayUsernameTextbox()
    {
        await _driver.Page.Locator("#username").FillAsync("myUsername");

        var value = await _driver.Page.Locator("#username").InputValueAsync();
        value.Should().Contain("myUsername");
    }

    [Test]
    public async Task ShouldDisplayPasswordLabel()
    {
        var label = await _driver.Page.GetByText("Password", new() { Exact = true }).InnerTextAsync();
        label.Should().Contain("Password");
    }

    [Test]
    public async Task ShouldDisplayPasswordTextbox()
    {
        await _driver.Page.Locator("#password").FillAsync("myPassword");

        var value = await _driver.Page.Locator("#password").InputValueAsync();
        value.Should().Contain("myPassword");
    }

    [Test]
    public async Task ShouldDisplayLoginButton()
    {
        var button = await _driver.Page.Locator("button[type='submit']").InnerTextAsync();
        button.Should().Contain("Login");
    }

    [Test]
    public async Task ShouldDisplayErrorOnFailedLogin()
    {
        await _driver.Page.Locator("button[type='submit']").ClickAsync();
        var errorMessage = await _driver.Page.Locator("#flash").InnerTextAsync();
        errorMessage.Should().Contain("Your username is invalid!");
    }

    [OneTimeTearDown]
    public async Task GlobalTeardown()
    {
        await _driver.DisposeAsync();
        await _host.StopAsync();
    }
}

