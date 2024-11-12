using Microsoft.EntityFrameworkCore;
using SupplyChain.Modules.Warehouses.Domain.CatalogQuantities;
using SupplyChain.Modules.Warehouses.Domain.CatalogQuantities.Repository;
using SupplyChain.Modules.Warehouses.Infrastructure.Database;

namespace SupplyChain.Modules.Warehouses.Infrastructure.CatalogQuantities;
internal sealed class CatalogQuantitiesRepository(WarehouseDBContext context) : ICatalogQuantitiesRepository
{
    public void Add(CatalogQuantity entity)
    {
       context.CatalogQuantities.Add(entity);
    }

    public async Task<CatalogQuantity?> GetByCatalogAndBinLocationIdsAsync(Guid catalogId, Guid BinLocationId, CancellationToken cancellationToken = default)
    {
        return await context.CatalogQuantities.
            SingleOrDefaultAsync(z => z.CatalogId == catalogId && z.BinLocationId == BinLocationId, cancellationToken);
    }

    public async Task<CatalogQuantity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.CatalogQuantities.SingleOrDefaultAsync(z => z.Id == id, cancellationToken);
    }


    public async Task<CatalogQuantity?> GetByCatalogIdAndWarehouseIdAsync(
        Guid CatalogId,
        Guid WarehouseId,
        CancellationToken cancellationToken)
    {
        return await context.CatalogQuantities.Include(z=>z.Location)
            .SingleOrDefaultAsync(z=>z.CatalogId == CatalogId
                                     && z.Location.WarehouseId == WarehouseId, cancellationToken);
    }

}
