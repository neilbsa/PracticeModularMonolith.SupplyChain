
using SupplyChain.Common.Application.Messaging;
using SupplyChain.Common.Domain;
using SupplyChain.Modules.Warehouses.Domain.CatalogQuantities;
using SupplyChain.Modules.Warehouses.Domain.CatalogQuantities.Repository;

namespace SupplyChain.Modules.Warehouses.Application.CatalogQuantities.GetByCatalogIdAndWarehouseId;


internal sealed class GetByCatalogIdAndWarehouseIdQueryHandler : IQueryHandler<GetByCatalogIdAndWarehouseIdQuery, CatalogQuantityResponse?>
{

    private readonly ICatalogQuantitiesRepository _catalogQuantityRepository;

    public GetByCatalogIdAndWarehouseIdQueryHandler(ICatalogQuantitiesRepository catalogQuantityRepository)
    {
        _catalogQuantityRepository = catalogQuantityRepository;
    }

    public async Task<Result<CatalogQuantityResponse?>> Handle(GetByCatalogIdAndWarehouseIdQuery request, CancellationToken cancellationToken)
    {
        CatalogQuantity? currentQuantity = await _catalogQuantityRepository.GetByCatalogIdAndWarehouseIdAsync(request.CatalogId, request.warehouseId, cancellationToken);

        if (currentQuantity is null)
        {
            return Result.Failure<CatalogQuantityResponse?>(CatalogQuantityErrors.NotFound());
        }

        return new CatalogQuantityResponse()
        {

            BinLocationId = currentQuantity.BinLocationId,
            CatalogId = currentQuantity.CatalogId,
            Id = currentQuantity.Id,
            OnHand = currentQuantity.OnHand.Value,
            Reserved = currentQuantity.Reserved.Value

        };

    }
}


