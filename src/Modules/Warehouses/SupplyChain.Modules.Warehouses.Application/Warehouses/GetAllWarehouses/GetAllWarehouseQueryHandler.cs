using Dapper;
using SupplyChain.Common.Application.Data;
using SupplyChain.Common.Application.Messaging;
using SupplyChain.Common.Domain;
using System.Data.Common;

namespace SupplyChain.Modules.Warehouses.Application.Warehouses.GetAllWarehouses;


internal sealed class GetAllWarehouseQueryHandler : IQueryHandler<GetAllWarehouseQuery, IReadOnlyList<WarehouseResponse>>
{

    private readonly IDbConnectionFactory _dbConnection;

    public GetAllWarehouseQueryHandler(IDbConnectionFactory dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<Result<IReadOnlyList<WarehouseResponse>>> Handle(GetAllWarehouseQuery request, CancellationToken cancellationToken)
    {
        string sql = $@"
                            SELECT 
                                    ""ID"" as {nameof(WarehouseResponse.Id)},
                                    ""CODE"" as {nameof(WarehouseResponse.WarehouseCode)},
                                    ""ADDRESS_STREET"" as {nameof(WarehouseResponse.Address)},
                                    ""DESCRIPTION"" as {nameof(WarehouseResponse.WarehouseDescription)}
                           FROM warehouse.""WAREHOUSES"" LIMIT @Limit OFFSET @OffSet
                ";

        using DbConnection connection = await _dbConnection.OpenConnectionAsync();

        IEnumerable<WarehouseResponse> result = await connection.QueryAsync<WarehouseResponse>(sql, request);




        return result.ToList();
    }
}
