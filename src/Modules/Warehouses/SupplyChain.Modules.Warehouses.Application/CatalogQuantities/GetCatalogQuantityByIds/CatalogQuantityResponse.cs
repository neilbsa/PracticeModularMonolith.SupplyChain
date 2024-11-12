using System;
using System.Linq;

namespace SupplyChain.Modules.Warehouses.Application.CatalogQuantities.GetCatalogQuantityByIds;


public sealed class CatalogQuantityResponse
{
    public Guid Id { get; set; }
    public string BinLocationCode { get; set; }
    public string BinLocationName { get; set; }
    public string CatalogId { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public decimal OnHand { get; set; }
    public decimal Reserved { get; set; }
}
