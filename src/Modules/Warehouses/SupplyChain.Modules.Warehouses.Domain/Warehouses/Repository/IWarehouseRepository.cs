using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyChain.Modules.Warehouses.Domain.Warehouses.Repository;
public interface IWarehouseRepository
{
    void Add(Warehouse warehouse);

    Task<Warehouse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Warehouse?> GetByWarehouseCodeAsync(string WarehouseCode, CancellationToken cancellationToken = default);
}
