using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SupplyChain.Common.Application.Caching;
using SupplyChain.Common.Domain;
using SupplyChain.Common.Presentation.ApiResults;
using SupplyChain.Common.Presentation.Endpoint;
using SupplyChain.Modules.Warehouses.Application.Catalogs.GetCatalogById;
using SupplyChain.Modules.Warehouses.Application.Catalogs.GetCatalogs;
using System;
using System.Linq;

namespace SupplyChain.Modules.Warehouses.Presentation.Catalogs;

internal sealed class GetCatalogById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("catalog/{id}", async Task<IResult> (Guid id, ISender sender, ICacheService cachedService) =>
        {

            string cacheKey = $"catalog-{id.ToString()}";
            CatalogResponse cachedData = await cachedService.GetAsync<CatalogResponse>(cacheKey);

            if (cachedData != null)
            {
                return Result.Success(cachedData).Match(Results.Ok, ApiResults.Problem);
            }



            Result<CatalogResponse> result = await sender.Send(new GetCatalogByIdQuery(id));

            if (result.IsSuccess)
            {
                await cachedService.SetAsync(cacheKey, result.Value);
            }


            return result.Match(Results.Ok, ApiResults.Problem);





        }).WithTags(Tags.Catalogs);
    }
}
