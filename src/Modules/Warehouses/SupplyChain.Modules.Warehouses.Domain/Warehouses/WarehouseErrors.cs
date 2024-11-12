using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupplyChain.Common.Domain;

namespace SupplyChain.Modules.Warehouses.Domain.Warehouses;
public static  class WarehouseErrors
{
    public static Error CodeAlreadyExists(string warehouseCode) =>
        Error.Conflict("WarehouseCode.Exists", $"Warehouse code {warehouseCode} is already exists!");
    public static Error NotFound() =>
    Error.NotFound("WarehouseCode.NotFound", $"Warehouse not found.");
}
