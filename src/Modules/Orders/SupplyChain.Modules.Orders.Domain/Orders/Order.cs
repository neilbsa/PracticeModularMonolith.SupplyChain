using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupplyChain.Common.Domain;

namespace SupplyChain.Modules.Orders.Domain.Orders;
public sealed class Order :Entity
{
   private readonly List<OrderDetail> _orderDetails = new List<OrderDetail>();

   private Order()
    {
       
    }
    private Order(Guid id, CustomerId customerId, WarehouseId warehouseId)
    {
        Id = id;
        CustomerId = customerId;
        WarehouseId = warehouseId;
    }

    public Guid Id { get; private set; }
    public CustomerId CustomerId { get; private set; }
    public WarehouseId WarehouseId { get; private set; }
    public IReadOnlyList<OrderDetail> OrderDetails => _orderDetails.AsReadOnly();
   
    public static Order Create(Guid CustomerId,Guid WarehouseId)
    {
        var order = new Order(Guid.NewGuid(), new CustomerId(CustomerId), new WarehouseId(WarehouseId));

        order.Raise(new NewOrderCreatedDomainEvent(order.Id));

        return order;
    }
    public void ChangeCustomerId(Guid customerId)
    {
        CustomerId = new CustomerId(customerId);
        Raise(new OrderCustomerIdChangedDomainEvent(Id, customerId));
    }

    public void AddOrUpdateDetailsToOrder(Guid CatalogId, decimal Quantity)
    {
        OrderDetail currentDetail = _orderDetails.SingleOrDefault(z => z.CatalogId == CatalogId);
        //if the catalog already exist in this order just alter the quantity
        if (currentDetail != null)
        {
            currentDetail.AddQuantity(Quantity,WarehouseId.Value);
        }
        else
        {
            //create and add it to details as new catalog item
            var newOrderDetail = OrderDetail.Create(this, CatalogId, Quantity);
            _orderDetails.Add(newOrderDetail);
        }
    }
}
