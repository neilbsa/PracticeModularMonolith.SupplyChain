using SupplyChain.Common.Application.EventBux;
using SupplyChain.Common.Domain;

namespace SupplyChain.Modules.Orders.IntegrationEvents;

public class NewCatalogOrderUpdateIntegrationEvent : IntegrationEvent
{
    public NewCatalogOrderUpdateIntegrationEvent(
            Guid id,
        DateTime occurredOnUtc,
        Guid catalogId,
        Guid warehouseId,
        decimal orderQuantity

        ) : base(occurredOnUtc, id)
    {
        CatalogId = catalogId;
        WarehouseId = warehouseId;
        OrderQuantity = orderQuantity;


    }

    public Guid CatalogId { get; init; }
    public Guid WarehouseId { get; init; } 
    public decimal OrderQuantity { get; init; }



}

