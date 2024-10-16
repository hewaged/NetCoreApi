using Exchange.Rates.RBA.Contracts.Messages.Base;
using Exchange.Rates.RBA.Contracts.Models;

namespace Exchange.Rates.RBA.Contracts.Messages;

public interface IAudExchangeRatesAccepted : IBaseContract
{
    string Symbols { get; }

    AudCurrencyExchange CurrencyExchange { get; }

    string Message { get; }
}