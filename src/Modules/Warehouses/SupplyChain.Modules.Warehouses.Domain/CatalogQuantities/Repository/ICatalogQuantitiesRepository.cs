using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyChain.Modules.Warehouses.Domain.CatalogQuantities.Repository;
public interface ICatalogQuantitiesRepository
{
    Task<CatalogQuantity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<CatalogQuantity?> GetByCatalogAndBinLocationIdsAsync(Guid catalogId,Guid BinLocationId, CancellationToken cancellationToken = default);
    Task<CatalogQuantity?> GetByCatalogIdAndWarehouseIdAsync(Guid CatalogId, Guid WarehouseId, CancellationToken cancellationToken);
    void Add(CatalogQuantity entity);
}
