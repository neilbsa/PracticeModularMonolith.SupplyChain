using SupplyChain.Common.Domain;
using System;
using System.Linq;

namespace SupplyChain.Modules.Warehouses.Domain.Warehouses.Events;

public sealed class NewWarehouseCreated(Guid WarehouseId) : DomainEvent
{
    public Guid WarehouseId { get; init; } = WarehouseId;

}

