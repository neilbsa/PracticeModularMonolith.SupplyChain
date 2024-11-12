
using SupplyChain.Common.Domain;


namespace SupplyChain.Modules.Warehouses.PublicApi;

public interface IWarehousePublicApi
{
    Task<Result<WarehouseApiResponse?>> GetWarehouseById(Guid id, CancellationToken cancellationToken);

    Task<Result<CatalogQuantityApiResponse>> GetQuantityByWarehouseIdAndCatalogIdAsync(Guid warehouseId, Guid CatalogId, CancellationToken cancellationToken);

    Task<Result<CatalogApiResponse?>> GetCatalogById(Guid CatalogId, CancellationToken cancellationToken);
}


public class CatalogApiResponse
{
    public Guid Id { get; set; }
    public string CatalogId { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
}
public class CatalogQuantityApiResponse
{
    public Guid Id { get; set; }
    public Guid BinLocationId { get; set; }
    public Guid CatalogId { get; set; }
    public decimal OnHand { get; set; }
    public decimal Reserved { get; set; }
}
public class WarehouseApiResponse
{
    public Guid Id { get;  set; }
    public string Code { get;  set; }
    public string Description { get;  set; }
    public string Address { get;  set; }
}
