using SupplyChain.Common.Domain;
using System;
using System.Linq;

namespace SupplyChain.Modules.Orders.Domain.Orders;


public sealed class NewOrderDetailAddedDomainEvent(Guid catalogId, Guid warehouseId,decimal Quantity) : DomainEvent
{
    public Guid CatalogId { get; init; } = catalogId;
    public Guid WarehouseId { get; init; } = warehouseId;
    public decimal OrderQuantity { get; init; } = Quantity;
}




