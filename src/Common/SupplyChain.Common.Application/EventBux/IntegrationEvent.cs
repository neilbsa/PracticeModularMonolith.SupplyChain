using System;
using System.Linq;

namespace SupplyChain.Common.Application.EventBux;


public abstract class IntegrationEvent : IIntegrationEvent
{
    protected IntegrationEvent(DateTime occurredOnUtc, Guid id)
    {
        OccurredOnUtc = occurredOnUtc;
        Id = id;
    }

    public DateTime OccurredOnUtc { get; init; }

    public Guid Id { get; init; }

}
