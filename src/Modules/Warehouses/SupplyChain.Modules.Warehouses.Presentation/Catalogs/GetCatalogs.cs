using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SupplyChain.Common.Application.Caching;
using SupplyChain.Common.Domain;
using SupplyChain.Common.Presentation.ApiResults;
using SupplyChain.Common.Presentation.Endpoint;
using SupplyChain.Modules.Warehouses.Application.Catalogs.GetCatalogs;
using System;
using System.Linq;

namespace SupplyChain.Modules.Warehouses.Presentation.Catalogs;


internal sealed class GetCatalogs : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {

        app.MapGet("catalogs", async Task<IResult> (int Limit, int Offset, ISender _sender, ICacheService caching) =>
        {
            string cachedKey = $"catalog-{Limit}-{Offset}";

            IReadOnlyList<CatalogResponse> cachedData = await caching.GetAsync<IReadOnlyList<CatalogResponse>>(cachedKey);

            if (cachedData != null)
            {
                return Result.Success(cachedData)
                .Match(Results.Ok, ApiResults.Problem);
            }


            Result<IReadOnlyList<CatalogResponse>> result = await _sender.Send(new GetCatalogsQuery(Limit, Offset));

            if (result.IsSuccess)
            {
                await caching.SetAsync(cachedKey, result.Value);
            }


            return result.Match(Results.Ok, ApiResults.Problem);

        }).WithTags(Tags.Catalogs);
    }
}
