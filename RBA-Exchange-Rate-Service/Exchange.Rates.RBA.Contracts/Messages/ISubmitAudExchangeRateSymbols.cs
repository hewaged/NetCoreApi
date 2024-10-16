using Exchange.Rates.RBA.Contracts.Messages.Base;
using System.Collections.Generic;

namespace Exchange.Rates.RBA.Contracts.Messages;

public interface ISubmitAudExchangeRateSymbols : IBaseContract
{
    List<string> Symbols { get; }
}