using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SupplyChain.Common.Application.Exceptions;
using SupplyChain.Modules.Warehouses.Application.Abstractions.Data;
using SupplyChain.Modules.Warehouses.Domain.BinLocations;
using SupplyChain.Modules.Warehouses.Domain.CatalogQuantities;
using SupplyChain.Modules.Warehouses.Domain.Catalogs;
using SupplyChain.Modules.Warehouses.Domain.Warehouses;
using SupplyChain.Modules.Warehouses.Infrastructure.BinLocations;
using SupplyChain.Modules.Warehouses.Infrastructure.CatalogQuantities;
using SupplyChain.Modules.Warehouses.Infrastructure.Catalogs;
using SupplyChain.Modules.Warehouses.Infrastructure.Warehouses;

namespace SupplyChain.Modules.Warehouses.Infrastructure.Database;
public sealed class WarehouseDBContext(DbContextOptions<WarehouseDBContext> options) : DbContext(options), IUnitOfWork
{


    internal DbSet<Warehouse> Warehouses { get; set; }
    internal DbSet<BinLocation> BinLocations { get; set; }
    internal DbSet<Catalog> Catalogs { get; set; }
    internal DbSet<CatalogQuantity> CatalogQuantities { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Warehouse);
        modelBuilder.ApplyConfiguration(new BinLocationConfiguration());
        modelBuilder.ApplyConfiguration(new WarehouseConfiguration());
        modelBuilder.ApplyConfiguration(new CatalogConfiguration());
        modelBuilder.ApplyConfiguration(new CatalogQuantityConfiguration());
    }

    public async override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        try
        {
            int result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
         
            return result;
        }
        
        catch (DbUpdateConcurrencyException ex)
        {

            throw new ConcurrencyException("Concurrency exception occured", innerException: ex);

        }
        catch (Exception ex)
        {

            throw new WarehouseException("unhandled exception occured", innerException: ex);

        }

    }
}
