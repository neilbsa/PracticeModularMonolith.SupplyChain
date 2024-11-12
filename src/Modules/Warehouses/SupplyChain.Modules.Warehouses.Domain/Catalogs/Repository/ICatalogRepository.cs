using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyChain.Modules.Warehouses.Domain.Catalogs.Repository;
public interface ICatalogRepository
{
   void AddCatalog(Catalog catalog);
   Task<Catalog> GetByIdAsync(Guid id,CancellationToken cancellationToken=default);
    Task<bool> CatalogIdExists(string catalogId, CancellationToken cancellationToken = default);
}
