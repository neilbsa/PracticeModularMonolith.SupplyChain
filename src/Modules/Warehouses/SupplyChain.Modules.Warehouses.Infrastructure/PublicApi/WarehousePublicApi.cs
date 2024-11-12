using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SupplyChain.Common.Domain;
using SupplyChain.Modules.Warehouses.Application.CatalogQuantities.GetByCatalogIdAndWarehouseId;
using SupplyChain.Modules.Warehouses.Application.Catalogs.GetCatalogById;
using SupplyChain.Modules.Warehouses.Application.Catalogs.GetCatalogs;
using SupplyChain.Modules.Warehouses.Application.Warehouses.GetWarehouseById;
using SupplyChain.Modules.Warehouses.Domain.CatalogQuantities;
using SupplyChain.Modules.Warehouses.Domain.Catalogs;
using SupplyChain.Modules.Warehouses.Domain.Warehouses;
using SupplyChain.Modules.Warehouses.Domain.Warehouses.Repository;
using SupplyChain.Modules.Warehouses.PublicApi;

namespace SupplyChain.Modules.Warehouses.Infrastructure.PublicApi;
internal sealed class WarehousePublicApi : IWarehousePublicApi
{
    private readonly ISender _sender;

    public WarehousePublicApi(ISender sender)
    {
        _sender = sender;
    }

    public async Task<Result<CatalogApiResponse?>> GetCatalogById(Guid CatalogId, CancellationToken cancellationToken)
    {
        Result<CatalogResponse> result = await _sender.Send(new GetCatalogByIdQuery(CatalogId), cancellationToken);
        if (result.IsFailure)
        {
            return Result.Failure<CatalogApiResponse?>(CatalogErrors.NotFound());
        }


        return new CatalogApiResponse() {
        
        
             CatalogId = result.Value.CatalogId,
              Category = result.Value.Category,
               Description = result.Value.Description,
                Id = result.Value.Id,
        };

    }

    public async Task<Result<CatalogQuantityApiResponse>> GetQuantityByWarehouseIdAndCatalogIdAsync(Guid warehouseId, Guid CatalogId, CancellationToken cancellationToken)
    {
        Result<CatalogQuantityResponse?> result = await _sender.Send(new GetByCatalogIdAndWarehouseIdQuery(warehouseId, CatalogId), cancellationToken);

        if(result.IsFailure)
        {
            return Result.Failure<CatalogQuantityApiResponse>(result.Error);
        }



        return new CatalogQuantityApiResponse()
        {

            BinLocationId = result.Value.CatalogId,
            CatalogId = result.Value.CatalogId,
            Id = result.Value.Id,
            OnHand = result.Value.OnHand,
            Reserved = result.Value.Reserved,

        };




    }

    public async Task<Result<WarehouseApiResponse?>> GetWarehouseById(Guid id, CancellationToken cancellationToken)
    {
        Result<WarehouseByIdResponse?> result = await _sender.Send(new GetWarehouseByIdQuery(id), cancellationToken);
        
        if (result.IsFailure)
        {
            return Result.Failure<WarehouseApiResponse?>(WarehouseErrors.NotFound());
        }

        return Result.Success<WarehouseApiResponse?>(new WarehouseApiResponse()
        {

            Address = result.Value.Address,
            Code = result.Value.WarehouseCode,
            Description = result.Value.WarehouseDescription,
            Id = result.Value.Id,


        });

    }

 
}
