using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupplyChain.Common.Domain;
using SupplyChain.Modules.Warehouses.Domain.BinLocations;
using SupplyChain.Modules.Warehouses.Domain.CatalogQuantities;
using SupplyChain.Modules.Warehouses.Domain.Catalogs;

namespace SupplyChain.Modules.Warehouses.Infrastructure.CatalogQuantities;
internal sealed class CatalogQuantityConfiguration : IEntityTypeConfiguration<CatalogQuantity>
{
    public void Configure(EntityTypeBuilder<CatalogQuantity> builder)
    {
    
        builder.HasKey(z=>z.Id);
        builder.HasOne(z=>z.Catalog).WithMany(z=>z.Quantities).HasForeignKey(z=>z.CatalogId);
        builder.HasOne(z=>z.Location).WithMany(z=>z.Quantities).HasForeignKey(z => z.BinLocationId);
        builder.Property(g => g.OnHand).HasConversion(z => z.Value, value => new Quantity(value));
        builder.Property(g => g.Reserved).HasConversion(z => z.Value, value => new Quantity(value));
        builder.HasIndex(z => z.CatalogId);
        builder.HasIndex(z => z.BinLocationId);
        builder.Property<uint>("Version").IsRowVersion();
    }
}
