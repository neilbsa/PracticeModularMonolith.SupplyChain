using System;
using System.Linq;

namespace SupplyChain.Modules.Warehouses.Domain.Warehouses;
public sealed record WarehouseAddress(string Street, string City, string ZipCode, string Country);

