using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupplyChain.Common.Domain;
using SupplyChain.Modules.Orders.Domain.Orders;

namespace SupplyChain.Modules.Orders.Infrastructure.Orders;

internal sealed class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {

        builder.ToTable("order_details");
        builder.HasKey(z => z.Id);
        builder.Property(c => c.Id).ValueGeneratedNever();
        builder.HasOne(z=>z.Order)
            .WithMany(z=>z.OrderDetails)
            .HasForeignKey(z => z.OrderId);
        builder.Property(z => z.OrderQuantity).HasConversion(z => z.Value, value => new Quantity(value));


    }
}
