using Dapper;
using SupplyChain.Common.Application.Data;
using SupplyChain.Common.Application.Messaging;
using SupplyChain.Common.Domain;
using System;
using System.Data.Common;
using System.Linq;

namespace SupplyChain.Modules.Warehouses.Application.Catalogs.GetCatalogs;


internal sealed class GetCatalogQueryHandler : IQueryHandler<GetCatalogsQuery, IReadOnlyList<CatalogResponse>>
{

    private readonly IDbConnectionFactory _connectionFactory;

    public GetCatalogQueryHandler(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<Result<IReadOnlyList<CatalogResponse>>> Handle(GetCatalogsQuery request, CancellationToken cancellationToken)
    {
        string query = $@"
                        SELECT  ""ID"" AS {nameof(CatalogResponse.Id)}, 
                                ""CATALOG_ID"" AS {nameof(CatalogResponse.CatalogId)}, 
                                ""DESCRIPTION"" AS {nameof(CatalogResponse.Description)}, 
                                ""CATEGORY"" AS {nameof(CatalogResponse.Category)}
	                        FROM warehouse.""CATALOGS"" LIMIT @limit OFFSET @Offset
                ";

        using DbConnection connection = await _connectionFactory.OpenConnectionAsync();

        IEnumerable<CatalogResponse> result = await connection.QueryAsync<CatalogResponse>(query, request);


        return result.ToList();


    }
}
