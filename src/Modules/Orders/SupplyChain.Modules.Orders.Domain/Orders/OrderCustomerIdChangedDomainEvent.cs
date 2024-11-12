using SupplyChain.Common.Domain;
using System;
using System.Linq;

namespace SupplyChain.Modules.Orders.Domain.Orders;


internal sealed class OrderCustomerIdChangedDomainEvent(Guid _orderId, Guid newCustomerId) : DomainEvent
{
    public Guid OrderId { get; init; } = _orderId;
    public Guid CustomerId { get; init; } = newCustomerId;
}
