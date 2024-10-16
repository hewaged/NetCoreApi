using Exchange.Rates.Aud.OpenApi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Exchange.Rates.RBA.Tests.Services.Startups;

public class TestAudStartup : Startup
{
    public TestAudStartup(IConfiguration configuration)
        : base(configuration)
    {
    }

    public override void ConfigureServices(IServiceCollection services)
    {
        // Build new configuration from test settings file
        var testConfiguration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.aud.json")
            .Build();
        // Override base configuration
        Configuration = testConfiguration;
        base.ConfigureServices(services);
    }
}