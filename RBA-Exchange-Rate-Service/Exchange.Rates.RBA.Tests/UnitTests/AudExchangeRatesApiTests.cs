using Exchange.Rates.Aud.Polling.Api;
using Exchange.Rates.Aud.Polling.Api.Services;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;

namespace Exchange.Rates.RBA.Tests.UnitTests;

public class AudExchangeRatesApiTests(
  WebApplicationFactory<Startup> factory)
  : IClassFixture<WebApplicationFactory<Startup>>
{
    [Fact]
    public async Task GetLatestRatesTest_Returns_Valid_ExchangeRates()
    {
        // Arrange
        var service = factory.Services.GetRequiredService<IAudExchangeRatesApi>();
        // Act
        var result = await service.GetLatestRates();
        // Assert
        Assert.NotNull(result.ExchangeRates);
        Assert.NotEmpty(result.ExchangeRates);
    }

    [Fact]
    public async Task GetLatestRatesTest_Returns_Invalid_ExxchangRates()
    {
        // Arrange
        var service = factory.Services.GetRequiredService<IAudExchangeRatesApi>();
        // Act
        var result = await service.GetLatestRates("XYZ");
        // Assert
        Assert.Empty(result.ExchangeRates);
    }
}