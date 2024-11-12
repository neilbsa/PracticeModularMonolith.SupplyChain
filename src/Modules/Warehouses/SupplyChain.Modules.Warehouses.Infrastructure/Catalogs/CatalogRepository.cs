using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SupplyChain.Modules.Warehouses.Domain.Catalogs;
using SupplyChain.Modules.Warehouses.Domain.Catalogs.Repository;
using SupplyChain.Modules.Warehouses.Infrastructure.Database;

namespace SupplyChain.Modules.Warehouses.Infrastructure.Catalogs;
internal sealed class CatalogRepository(WarehouseDBContext context) : ICatalogRepository
{
    public void AddCatalog(Catalog catalog)
    {
       context.Catalogs.Add(catalog);
    }

    public async Task<bool> CatalogIdExists(string catalogId, CancellationToken cancellationToken = default)
    {
        return await context.Catalogs.AnyAsync(z => z.CatalogId == new CatalogId(catalogId), cancellationToken);
    }

    public async Task<Catalog> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Catalogs.SingleOrDefaultAsync(z => z.Id == id, cancellationToken);
    }
}
