using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupplyChain.Common.Application.EventBux;
using SupplyChain.Common.Application.Messaging;
using SupplyChain.Modules.Orders.Domain.Orders;
using SupplyChain.Modules.Orders.IntegrationEvents;


namespace SupplyChain.Modules.Orders.Application.Orders.AddItemsToOrder;
internal sealed class NewOrderDetailAddedDomainEventHandler(IEventBus eventBus) : IDomainEventHandler<NewOrderDetailAddedDomainEvent>
{
    public  async Task Handle(NewOrderDetailAddedDomainEvent notification, CancellationToken cancellationToken)
    {

      await  eventBus.PublishAsync(new NewCatalogOrderUpdateIntegrationEvent(
            notification.Id,
            notification.OccurredOnUtc,
          
            notification.CatalogId,
            notification.WarehouseId,
            notification.OrderQuantity),
            cancellationToken);
    }
}
