using SupplyChain.Common.Domain;
using System;
using System.Linq;

namespace SupplyChain.Modules.Warehouses.Domain.BinLocations.Events;



public sealed class NewBinLocationCreated(Guid id) : DomainEvent
{
    public Guid BinLocationId { get; init; } = id;

}
