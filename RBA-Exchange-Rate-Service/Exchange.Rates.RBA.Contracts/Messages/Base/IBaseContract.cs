using System;

namespace Exchange.Rates.RBA.Contracts.Messages.Base;

public interface IBaseContract
{
    Guid EventId { get; }

    DateTime Timestamp { get; }
}