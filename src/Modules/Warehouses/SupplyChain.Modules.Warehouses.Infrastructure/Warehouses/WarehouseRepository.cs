using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SupplyChain.Modules.Warehouses.Domain.Warehouses;
using SupplyChain.Modules.Warehouses.Domain.Warehouses.Repository;
using SupplyChain.Modules.Warehouses.Infrastructure.Database;

namespace SupplyChain.Modules.Warehouses.Infrastructure.Warehouses;
internal sealed class WarehouseRepository(WarehouseDBContext context) : IWarehouseRepository
{
    public void Add(Warehouse warehouse)
    {
       context.Add(warehouse);
    }

    public Task<Warehouse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return context.Warehouses.SingleOrDefaultAsync(z => z.Id == id, cancellationToken);
    }

    public async Task<Warehouse?> GetByWarehouseCodeAsync(string WarehouseCode, CancellationToken cancellationToken = default)
    {
    
        return await context.Warehouses.SingleOrDefaultAsync(z => z.Code == new WarehouseCode(WarehouseCode), cancellationToken);
    }
}
