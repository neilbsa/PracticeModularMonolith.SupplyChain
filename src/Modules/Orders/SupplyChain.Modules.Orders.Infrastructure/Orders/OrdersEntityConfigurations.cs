using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;
using SupplyChain.Common.Domain;
using SupplyChain.Modules.Orders.Domain.Orders;
using static Dapper.SqlMapper;

namespace SupplyChain.Modules.Orders.Infrastructure.Orders;
internal sealed class OrdersEntityConfigurations : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("orders");
        builder.HasKey(z => z.Id);
        builder.Property(z => z.CustomerId)
            .HasConversion(z => z.Value, value => new CustomerId(value));
        builder.Property(z => z.WarehouseId)
            .HasConversion(z => z.Value, value => new WarehouseId(value));
        builder
            .HasMany(z=>z.OrderDetails)
            .WithOne()
            .HasForeignKey(z=>z.OrderId)
            .OnDelete(DeleteBehavior.Cascade);




    }
}


internal sealed class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {

        builder.ToTable("order_details");
        builder.HasKey(z => z.Id);
        builder.HasOne(z=>z.Order)
            .WithMany(z=>z.OrderDetails)
            .HasForeignKey(z => z.OrderId);
        builder.Property(z => z.OrderQuantity).HasConversion(z => z.Value, value => new Quantity(value));


    }
}
