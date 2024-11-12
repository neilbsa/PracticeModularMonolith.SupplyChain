using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SupplyChain.Modules.Warehouses.Domain.BinLocations;
using SupplyChain.Modules.Warehouses.Domain.BinLocations.Repository;
using SupplyChain.Modules.Warehouses.Infrastructure.Database;

namespace SupplyChain.Modules.Warehouses.Infrastructure.BinLocations;
internal sealed class BinLocationRepository(WarehouseDBContext context) : IBinLocationRepository
{
    public void Add(BinLocation entity)
    {
        context.BinLocations.Add(entity);
    }

    public async Task<bool> CodeExistAsync(string code, CancellationToken token = default)
    {
        return await context.BinLocations.AnyAsync(z => z.Code == new BinLocationCode(code), token);
    }

    public async Task<BinLocation?> GetByCodeAsync(string BinLocationCode, CancellationToken token = default)
    {
        return await context.BinLocations.SingleOrDefaultAsync(e=>e.Code == new BinLocationCode(BinLocationCode), token);
    }

    public async Task<BinLocation> GetByIdAsync(Guid Id, CancellationToken token = default)
    {
        return await context.BinLocations.SingleOrDefaultAsync(g => g.Id == Id, token);
    }
}
