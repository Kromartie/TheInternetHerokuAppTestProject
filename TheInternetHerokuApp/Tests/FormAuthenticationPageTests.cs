using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;
using TheInternetHerokuApp.Configs;
using TheInternetHerokuApp.Drivers;
using TheInternetHerokuApp.Pages;

namespace TheInternetHerokuApp.Tests;

[TestFixture]
public class FormAuthenticationPageTests
{
    private IHost _host;
    private BrowserDriver _driver;
    private TestSettings _settings;
    private FormAuthenticationPage _formAuthenticationPage;

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
                services.AddTransient<FormAuthenticationPage>();
            })
            .Build();

        _settings = _host.Services.GetService<TestSettings>();
        _driver = _host.Services.GetService<BrowserDriver>();
        await _driver.InitializeAsync();

        var landingPage = _host.Services.GetService<LandingPage>();
        await landingPage.NavigateAsync(_settings.BaseUrl);
        await landingPage.ClickLinkAsync("Form Authentication");

        _formAuthenticationPage = _host.Services.GetService<FormAuthenticationPage>();
    }

    [Test]
    public async Task ShouldDisplayTitle()
    {
        var header = await _formAuthenticationPage.GetHeaderAsync();
        header.Should().Be("Login Page");
    }

    [Test]
    public async Task ShouldDisplaySubHeader()
    {
        var subHeader = await _formAuthenticationPage.GetSubHeaderAsync();
        subHeader.Should().Contain("This is where you can log into the secure area.");
    }

    [Test]
    public async Task ShouldFillUsernameTextbox()
    {
        await _formAuthenticationPage.UsernameTextbox.FillAsync("myUsername");
        var value = await _formAuthenticationPage.UsernameTextbox.InputValueAsync();
        value.Should().Be("myUsername");
    }

    [Test]
    public async Task ShouldFillPasswordTextbox()
    {
        await _formAuthenticationPage.PasswordTextbox.FillAsync("myPassword");
        var value = await _formAuthenticationPage.PasswordTextbox.InputValueAsync();
        value.Should().Be("myPassword");
    }

    [Test]
    public async Task ShouldClickLoginButtonAndSeeError()
    {
        await _formAuthenticationPage.Login("", ""); // Empty fields
        var error = await _formAuthenticationPage.ErrorMessage.InnerTextAsync();
        error.Should().Contain("Your username is invalid!");
    }

    [OneTimeTearDown]
    public async Task GlobalTeardown()
    {
        await _driver.DisposeAsync();
        await _host.StopAsync();
    }
}

