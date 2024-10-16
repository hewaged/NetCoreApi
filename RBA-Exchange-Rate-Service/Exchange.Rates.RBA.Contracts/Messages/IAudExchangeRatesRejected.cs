using Exchange.Rates.RBA.Contracts.Messages.Base;

namespace Exchange.Rates.RBA.Contracts.Messages;

public interface IAudExchangeRatesRejected : IBaseContract
{
    string Symbols { get; }

    string Reason { get; }
}