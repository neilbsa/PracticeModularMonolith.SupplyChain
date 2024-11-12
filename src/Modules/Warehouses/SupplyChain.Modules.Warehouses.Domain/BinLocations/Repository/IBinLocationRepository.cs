using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyChain.Modules.Warehouses.Domain.BinLocations.Repository;
public interface IBinLocationRepository
{
    void Add(BinLocation entity);
    Task<bool> CodeExistAsync(string code, CancellationToken token = default);
    Task<BinLocation> GetByIdAsync(Guid Id, CancellationToken token = default);
    Task<BinLocation?> GetByCodeAsync(string BinLocationCode, CancellationToken token = default);
}
