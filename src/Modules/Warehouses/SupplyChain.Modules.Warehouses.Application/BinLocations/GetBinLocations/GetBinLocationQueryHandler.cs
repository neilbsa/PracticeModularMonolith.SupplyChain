using System.Data.Common;
using Dapper;
using SupplyChain.Common.Application.Data;
using SupplyChain.Common.Application.Messaging;
using SupplyChain.Common.Domain;
using SupplyChain.Modules.Warehouses.Domain.BinLocations;

namespace SupplyChain.Modules.Warehouses.Application.BinLocations.GetBinLocations;




internal sealed class GetBinLocationQueryHandler : IQueryHandler<GetBinLocationsQuery, IReadOnlyList<BinLocationResponse>>
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public GetBinLocationQueryHandler(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<Result<IReadOnlyList<BinLocationResponse>>> Handle(GetBinLocationsQuery request, CancellationToken cancellationToken)
    {
        string sqlQuery = $@"
                        SELECT Locations.""ID"" AS  {nameof(BinLocationResponse.Id)},
                            warehouse.""CODE"" AS  {nameof(BinLocationResponse.WarehouseCode)}, 
                                Locations.""CODE"" AS {nameof(BinLocationResponse.BinLocationCode)}, 
                                ""NAME"" AS {nameof(BinLocationResponse.BinLocationName)}
	                        FROM warehouse.""BIN_LOCATIONS"" AS Locations
	                        INNER JOIN warehouse.""WAREHOUSES"" AS warehouse on warehouse.""ID"" = Locations.""WAREHOUSE_ID""
                          LIMIT @Limit OFFSET @Offset
                        ";



        using DbConnection dbConnection = await _dbConnectionFactory.OpenConnectionAsync();

        IEnumerable<BinLocationResponse> result = await dbConnection.QueryAsync<BinLocationResponse>(sqlQuery, request);
        return result.ToList();


    }
}
