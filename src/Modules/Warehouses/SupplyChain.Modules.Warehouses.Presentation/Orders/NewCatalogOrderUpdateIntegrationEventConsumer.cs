
using MassTransit;
using MediatR;
using SupplyChain.Common.Application.Exceptions;
using SupplyChain.Common.Domain;
using SupplyChain.Modules.Orders.IntegrationEvents;
using SupplyChain.Modules.Warehouses.Application.CatalogQuantities.AddOnhandQuantity;
using SupplyChain.Modules.Warehouses.Application.CatalogQuantities.GetByCatalogIdAndWarehouseId;

namespace SupplyChain.Modules.Warehouses.Presentation.Orders;
public sealed class NewCatalogOrderUpdateIntegrationEventConsumer(ISender sender) : IConsumer<NewCatalogOrderUpdateIntegrationEvent>
{
    public async Task Consume(ConsumeContext<NewCatalogOrderUpdateIntegrationEvent> context)
    {
        Common.Domain.Result<CatalogQuantityResponse?> location = await sender.Send(new GetByCatalogIdAndWarehouseIdQuery(context.Message.WarehouseId, context.Message.CatalogId));

        if (location.IsFailure)
        {
            throw new WarehouseException(nameof(GetByCatalogIdAndWarehouseIdQuery), location.Error);
        }


        Result addingQuantityResult = await sender.Send(new AddOnHandCommand(context.Message.CatalogId, location.Value.BinLocationId, context.Message.OrderQuantity));


        if (addingQuantityResult.IsFailure)
        {
            throw new WarehouseException(nameof(GetByCatalogIdAndWarehouseIdQuery), addingQuantityResult.Error);
        }



    }
}
