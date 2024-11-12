using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupplyChain.Modules.Warehouses.Domain.BinLocations;
using SupplyChain.Modules.Warehouses.Domain.CatalogQuantities;
using SupplyChain.Modules.Warehouses.Domain.Warehouses;

namespace SupplyChain.Modules.Warehouses.Infrastructure.BinLocations;
internal sealed class BinLocationConfiguration : IEntityTypeConfiguration<BinLocation>
{
    public void Configure(EntityTypeBuilder<BinLocation> builder)
    {
     
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Code)
            .HasConversion(z => z.value, value => new BinLocationCode(value));
        builder.Property(x => x.Name)
            .HasConversion(z => z.value, value => new BinLocationName(value));
      
        builder.HasOne(z=>z.Warehouse)
            .WithMany(z=>z.BinLocations)
            .HasForeignKey(z => z.WarehouseId);
        


        builder.HasMany(z=>z.Quantities)
            .WithOne(z=>z.Location)
            .HasForeignKey(z => z.BinLocationId);


        builder.HasIndex(x => x.Code);

    }
}
