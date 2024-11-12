using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using SupplyChain.Common.Application.EventBux;

namespace SupplyChain.Common.Infrastructure.EventBux;
internal sealed class EventBus(IBus bus) : IEventBus
{
    public async Task PublishAsync<T>(T IntegrationEvent, CancellationToken cancellationToken) where T : IIntegrationEvent
    {
        await bus.Publish(IntegrationEvent, cancellationToken);
    }
}
