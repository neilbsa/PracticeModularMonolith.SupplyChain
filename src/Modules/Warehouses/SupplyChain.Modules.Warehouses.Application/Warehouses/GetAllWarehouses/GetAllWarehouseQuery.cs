
using System.Data;
using System.Data.Common;
using Dapper;
using SupplyChain.Common.Application.Data;
using SupplyChain.Common.Application.Messaging;
using SupplyChain.Common.Domain;

namespace SupplyChain.Modules.Warehouses.Application.Warehouses.GetAllWarehouses;
public sealed record GetAllWarehouseQuery(int Limit, int OffSet) : IQuery<IReadOnlyList<WarehouseResponse>>;





public class WarehouseResponse
{
    public Guid Id { get; set; }
    public string WarehouseCode { get; set; }
    public string WarehouseDescription { get; set; }
    public string Address { get; set; }
}
