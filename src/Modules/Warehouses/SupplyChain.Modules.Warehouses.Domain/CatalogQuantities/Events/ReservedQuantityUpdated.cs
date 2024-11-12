using SupplyChain.Common.Domain;

namespace SupplyChain.Modules.Warehouses.Domain.CatalogQuantities.Events;

public sealed class ReservedQuantityUpdated(Guid id) : DomainEvent
{
    public Guid QuantityId { get; init; } = id;
}
