using Exchange.Rates.RBA.Contracts.Models;
using System.Threading.Tasks;

namespace Exchange.Rates.Aud.Polling.Api.Services;

public interface IAudExchangeRatesApi
{
    Task<AudCurrencyExchange> GetLatestRates(string symbols = "USD");
}