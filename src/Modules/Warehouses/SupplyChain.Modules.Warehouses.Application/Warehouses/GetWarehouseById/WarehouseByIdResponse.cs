using System;
using System.Linq;

namespace SupplyChain.Modules.Warehouses.Application.Warehouses.GetWarehouseById;



public class WarehouseByIdResponse
{
    public Guid Id { get; set; }
    public string WarehouseCode { get; set; }
    public string WarehouseDescription { get; set; }
    public string Address { get; set; }
}
