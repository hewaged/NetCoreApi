using System.Diagnostics.CodeAnalysis;

namespace Exchange.Rates.Aud.Polling.Api.Options;

[ExcludeFromCodeCoverage]
public class ExchangeratesApiOptions
{
    public int HandlerLifetimeMinutes { get; set; } = 5;

    public string Name { get; set; }

    public string Url { get; set; }
}