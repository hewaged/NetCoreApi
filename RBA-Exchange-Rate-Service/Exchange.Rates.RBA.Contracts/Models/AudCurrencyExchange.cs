using System;
using System.Collections.Generic;

namespace Exchange.Rates.RBA.Contracts.Models;

public class AudCurrencyExchange
{
    public string BaseCurrencyCode { get; set; }

    public DateTime Date { get; set; }

    public Dictionary<string, decimal> ExchangeRates { get; set; }
}