using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SupplyChain.Modules.Orders.Application.Abstractions.Data;
using SupplyChain.Modules.Orders.Domain.Orders;
using SupplyChain.Modules.Orders.Infrastructure.Orders;

namespace SupplyChain.Modules.Orders.Infrastructure.Database.Data;
public sealed class OrdersDbContext(DbContextOptions<OrdersDbContext> opt) : DbContext(opt), IUnitOfWork
{

    public DbSet<Order> Orders { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Orders);

                    modelBuilder.ApplyConfiguration(new OrdersEntityConfigurations());
        modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());

    }


}
