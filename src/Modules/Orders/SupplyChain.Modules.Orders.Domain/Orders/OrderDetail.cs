using SupplyChain.Common.Domain;
using System;
using System.Linq;

namespace SupplyChain.Modules.Orders.Domain.Orders;

public sealed class OrderDetail : Entity
{

    private OrderDetail()
    {
        
    }
    private OrderDetail(Guid id, Guid catalogId, Quantity orderQuantity)
    {
        Id = id;
        CatalogId = catalogId;
        OrderQuantity = orderQuantity;
    }

    public Guid Id { get; private set; }
    public Guid CatalogId { get; private set; }
    public Guid OrderId { get; private set; }
    public Quantity OrderQuantity { get; private set; }
    public Order Order { get; private set; }


    public static OrderDetail Create(Order OrderId, Guid CatalogId, decimal Quantity)
    {
        var orderDetail = new OrderDetail(Guid.NewGuid(), CatalogId, new Quantity(Quantity));
        orderDetail.Raise(new NewOrderDetailAddedDomainEvent(CatalogId, OrderId.WarehouseId.Value,Quantity));
        return orderDetail;
    }

    public Result AddQuantity(decimal Quantity,Guid WarehouseId)
    {
        OrderQuantity += new Quantity(Quantity);
        Raise(new NewOrderDetailAddedDomainEvent(CatalogId, WarehouseId, Quantity));
        return Result.Success();
    }

    public Result DeductQuantity(decimal Quantity, Guid WarehouseId)
    {
        try
        {
            OrderQuantity -= new Quantity(Quantity);
            Raise(new NewOrderDetailAddedDomainEvent(CatalogId, WarehouseId, Quantity));
            return Result.Success();
        }
        catch
        {
           return Result.Failure(OrderDetailsErrors.InvalidQuantity());
        }
      
    }
}
