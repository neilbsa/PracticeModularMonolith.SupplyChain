
namespace SupplyChain.Modules.Warehouses.Application.CatalogQuantities.GetByCatalogIdAndWarehouseId;


public class CatalogQuantityResponse
{
    public Guid Id { get; set; }
    public Guid BinLocationId { get; set; }
    public Guid CatalogId { get; set; }
    public decimal OnHand { get; set; }
    public decimal Reserved { get; set; }
}


