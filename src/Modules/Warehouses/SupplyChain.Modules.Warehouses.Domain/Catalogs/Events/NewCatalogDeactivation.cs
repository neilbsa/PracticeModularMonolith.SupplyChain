using SupplyChain.Common.Domain;
using System;
using System.Linq;

namespace SupplyChain.Modules.Warehouses.Domain.Catalogs.Events;


public sealed class NewCatalogDeactivation(Guid CatalogID) : DomainEvent
{
    public Guid CatalogId { get; init; } = CatalogID;
}
