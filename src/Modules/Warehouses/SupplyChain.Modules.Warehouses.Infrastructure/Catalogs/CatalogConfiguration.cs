using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupplyChain.Modules.Warehouses.Domain.CatalogQuantities;
using SupplyChain.Modules.Warehouses.Domain.Catalogs;

namespace SupplyChain.Modules.Warehouses.Infrastructure.Catalogs;
internal sealed class CatalogConfiguration : IEntityTypeConfiguration<Catalog>
{
    public void Configure(EntityTypeBuilder<Catalog> builder)
    {
   
        builder.HasKey(c => c.Id);
        builder.Property(z => z.CatalogId)
            .HasConversion(z => z.value, value => new CatalogId(value));
        builder.Property(z => z.Description)
            .HasConversion(z => z.value, value => new CatalogDescription(value));
        builder.Property(z => z.Category)
            .HasConversion(z => z.value, value => new CatalogCategory(value));
        builder.HasMany(z=>z.Quantities)
            .WithOne(z=>z.Catalog).HasForeignKey(z=>z.CatalogId);
        builder.HasIndex(c => c.CatalogId);
    }
}
