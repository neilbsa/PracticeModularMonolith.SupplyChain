using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using SupplyChain.Modules.Orders.Domain.Orders;
using SupplyChain.Modules.Orders.Domain.Orders.Repository;
using SupplyChain.Modules.Orders.Infrastructure.Database.Data;

namespace SupplyChain.Modules.Orders.Infrastructure.Orders;
internal sealed class OrderRepository(OrdersDbContext context) : IOrderRepository
{
    public void Add(Order order)
    {
        context.Orders.Add(order);
    }

    public async Task<Order?> GetByIdAsync(Guid Id, CancellationToken cancellationToken)
    {
        Order? order = await context
            .Orders
            .Include(z=>z.OrderDetails)
            .SingleOrDefaultAsync(z => z.Id == Id, cancellationToken);
      

        return order;
    }

    public async Task<int> SaveOrderChangesAsync(CancellationToken cancellationToken)
    {
           return await context.SaveChangesAsync(cancellationToken);
    }

    public void Update(Order order)
    {

        context.Orders.Update(order);


     

    }
}
