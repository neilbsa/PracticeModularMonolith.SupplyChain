using System;
using System.Linq;

namespace SupplyChain.Common.Application.EventBux;

public interface IEventBus
{
    Task PublishAsync<T>(T IntegrationEvent, CancellationToken cancellationToken) where T : IIntegrationEvent;
}
