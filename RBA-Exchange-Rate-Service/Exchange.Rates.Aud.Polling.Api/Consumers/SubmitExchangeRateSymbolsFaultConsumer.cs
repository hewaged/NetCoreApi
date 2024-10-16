using Exchange.Rates.RBA.Contracts.Messages;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace Exchange.Rates.Aud.Polling.Api.Consumers;

public class SubmitExchangeRateSymbolsFaultConsumer : IConsumer<Fault<ISubmitAudExchangeRateSymbols>>
{
    public Task Consume(ConsumeContext<Fault<ISubmitAudExchangeRateSymbols>> context)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine();
        Console.WriteLine($"There was an error with requesting a SubmitExchangeRateSymbols");
        Console.ResetColor();
        return Task.CompletedTask;
    }
}