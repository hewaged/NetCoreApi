using Exchange.Rates.RBA.Tests.Services;
using Exchange.Rates.RBA.Tests.Services.Factories;
using Exchange.Rates.RBA.Tests.Services.Fixtures;
using System.Threading.Tasks;
using Xunit;

namespace Exchange.Rates.RBA.Tests.IntegrationTests;

public class ExchangeRatesAudControllerTests : ControllerAudTestsBase
{
    private const string BASE_URL = "/api/exchangeratesaud/";
    private readonly HttpClientHelper _httpClientHelper;

    public ExchangeRatesAudControllerTests(WebApiAudTestFactory factory)
        : base(factory)
    {
        _httpClientHelper = new HttpClientHelper(Client);
    }

    [Fact]
    public async Task AudbaseRatesTest()
    {
        //TODO
    }
}