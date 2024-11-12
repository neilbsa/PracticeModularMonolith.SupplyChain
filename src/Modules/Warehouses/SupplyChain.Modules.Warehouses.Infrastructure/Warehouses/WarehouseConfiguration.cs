using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupplyChain.Modules.Warehouses.Domain.BinLocations;
using SupplyChain.Modules.Warehouses.Domain.Warehouses;

namespace SupplyChain.Modules.Warehouses.Infrastructure.Warehouses;
internal sealed class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
{
    public void Configure(EntityTypeBuilder<Warehouse> builder)
    {

        builder.HasKey(x => x.Id);

        builder.HasMany(z=>z.BinLocations)
            .WithOne(z=>z.Warehouse)
            .HasForeignKey(z => z.WarehouseId);
        
        builder.Property(z => z.Code)
            .HasConversion(z => z.value, value => new WarehouseCode(value));
        builder.Property(z => z.Description)
            .HasConversion(z => z.value, value => new WarehouseDescription(value));
        
        builder.OwnsOne(x => x.Address);
        builder.HasIndex(x => x.Code);
    }
}
