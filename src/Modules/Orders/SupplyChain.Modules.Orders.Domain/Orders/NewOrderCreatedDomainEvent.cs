using SupplyChain.Common.Domain;
using System;
using System.Linq;

namespace SupplyChain.Modules.Orders.Domain.Orders;


internal sealed class NewOrderCreatedDomainEvent(Guid orderId) : DomainEvent
{
    public Guid OrderId { get; init; } = orderId;
}
