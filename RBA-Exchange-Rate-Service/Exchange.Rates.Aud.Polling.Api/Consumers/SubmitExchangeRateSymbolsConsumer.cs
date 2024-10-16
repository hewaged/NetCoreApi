using Exchange.Rates.Aud.Polling.Api.Services;
using Exchange.Rates.RBA.Contracts.Messages;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace Exchange.Rates.Aud.Polling.Api.Consumers;

public sealed class SubmitExchangeRateSymbolsConsumer : IConsumer<ISubmitAudExchangeRateSymbols>
{
    private readonly ILogger<SubmitExchangeRateSymbolsConsumer> _logger;
    private readonly IAudExchangeRatesApi _audExchangeRatesApi;

    public SubmitExchangeRateSymbolsConsumer(ILogger<SubmitExchangeRateSymbolsConsumer> logger,
        IAudExchangeRatesApi ecbExchangeRatesApi)
    {
        _logger = logger;
        _audExchangeRatesApi = ecbExchangeRatesApi;
    }

    public async Task Consume(ConsumeContext<ISubmitAudExchangeRateSymbols> context)
    {
        if (context.RequestId != null)
        {
            if (context.Message.Symbols?.Any() == true)
            {
                var symbols = string.Join(",", context.Message.Symbols);
                var result = await _audExchangeRatesApi.GetLatestRates(symbols.Trim().ToUpper()).ConfigureAwait(false);
                if (result.ExchangeRates == null)
                {
                    await context.RespondAsync<IAudExchangeRatesRejected>(new
                    {
                        context.Message.EventId,
                        InVar.Timestamp,
                        context.Message.Symbols,
                        Reason = $"Exchange Rates for a {symbols} are not available"
                    });
                }
                else
                {
                    await context.RespondAsync<IAudExchangeRatesAccepted>(new
                    {
                        context.Message.EventId,
                        InVar.Timestamp,
                        context.Message.Symbols,
                        CurrencyExchange = result,
                        Message = "Exchange Rates Symbols"
                    });
                }
            }
        }
    }
}