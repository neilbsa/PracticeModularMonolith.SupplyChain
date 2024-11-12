namespace SupplyChain.Modules.Warehouses.Application.BinLocations.GetBinLocations;


public class BinLocationResponse
{
    public Guid Id { get; set; }
    public string WarehouseCode { get; set; }
    public string BinLocationCode { get; set; }

    public string BinLocationName { get; set; }
}
