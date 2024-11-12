using Dapper;
using SupplyChain.Common.Application.Data;
using SupplyChain.Common.Application.Messaging;
using SupplyChain.Common.Domain;
using SupplyChain.Modules.Warehouses.Domain.CatalogQuantities;
using SupplyChain.Modules.Warehouses.Domain.Catalogs;
using System;
using System.Data.Common;
using System.Linq;

namespace SupplyChain.Modules.Warehouses.Application.CatalogQuantities.GetCatalogQuantityByIds;


internal sealed class GetCatalogQuantityByIdsQueryHandler : IQueryHandler<GetCatalogQuantityByIdsQuery, CatalogQuantityResponse>
{

    private readonly IDbConnectionFactory _connectionFactory;

    public GetCatalogQuantityByIdsQueryHandler(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<Result<CatalogQuantityResponse>> Handle(GetCatalogQuantityByIdsQuery request, CancellationToken cancellationToken)
    {
        string sqlQuery = $@"
                    SELECT 
                                CatalogQuantity.""ID"" AS {nameof(CatalogQuantityResponse.Id)},
                                Binlocations.""CODE"" AS {nameof(CatalogQuantityResponse.BinLocationCode)}, 
                                 Binlocations.""NAME"" AS {nameof(CatalogQuantityResponse.BinLocationName)}, 
                                Catalogs.""CATALOG_ID"" AS {nameof(CatalogQuantityResponse.CatalogId)}, 
                                Catalogs.""DESCRIPTION"" AS {nameof(CatalogQuantityResponse.Description)}, 
                                Catalogs.""CATEGORY"" AS {nameof(CatalogQuantityResponse.Category)}, 
                                ""ON_HAND"" AS {nameof(CatalogQuantityResponse.OnHand)}, 
                                ""RESERVED"" AS {nameof(CatalogQuantityResponse.Reserved)}
	                    FROM warehouse.""CATALOG_QUANTITIES"" AS CatalogQuantity
	                    INNER JOIN warehouse.""BIN_LOCATIONS"" AS Binlocations on Binlocations.""ID"" = CatalogQuantity.""BIN_LOCATION_ID""
	                    INNER JOIN warehouse.""CATALOGS"" AS Catalogs on Catalogs.""ID"" = CatalogQuantity.""CATALOG_ID""
                            WHERE BinLocations.""ID"" = @BinLocationId AND
	                        Catalogs.""ID"" = @CatalogId


            ";

        using DbConnection connection = await _connectionFactory.OpenConnectionAsync();

        CatalogQuantityResponse? result = await connection.QuerySingleOrDefaultAsync<CatalogQuantityResponse>(sqlQuery, request);

        
        if(result == null)
        {
            return Result.Failure<CatalogQuantityResponse>(CatalogQuantityErrors.NotFound());
        }


        return result;

    }
}
