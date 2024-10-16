using System;

namespace Exchange.Rates.RBA.Contracts.Models
{
    public class ExchangeRate
    {
        public string Value { get; set; }
        public string Unit { get; set; }
        public int Decimals { get; set; }
        public string BaseCurrency { get; set; }
        public string TargetCurrency { get; set; }
        public DateTime Date { get; set; }
    }
}