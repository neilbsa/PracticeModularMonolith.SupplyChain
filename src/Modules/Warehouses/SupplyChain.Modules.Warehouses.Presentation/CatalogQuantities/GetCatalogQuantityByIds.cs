
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SupplyChain.Common.Application.Caching;
using SupplyChain.Common.Domain;
using SupplyChain.Common.Presentation.ApiResults;
using SupplyChain.Common.Presentation.Endpoint;
using SupplyChain.Modules.Warehouses.Application.CatalogQuantities.GetCatalogQuantityByIds;



namespace SupplyChain.Modules.Warehouses.Presentation.CatalogQuantities;

internal sealed class GetCatalogQuantityByIds : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("catalog/{CatalogId}/binlocation/{BinlocationId}", async Task<IResult> (
            Guid CatalogId,
            Guid BinLocationId,
            ISender sender,
            ICacheService cacheService) =>
        {
            string cachedKey = $"catalog-{CatalogId.ToString()}-{BinLocationId.ToString()}";
            CatalogQuantityResponse cachedData = await cacheService.GetAsync<CatalogQuantityResponse>(cachedKey);

            if (cachedData != null)
            {
                return Result.Success(cachedData)
                .Match(Results.Ok, ApiResults.Problem);
            }

            Result<CatalogQuantityResponse> result = await sender.Send(new GetCatalogQuantityByIdsQuery(CatalogId, BinLocationId));

            if (result.IsSuccess)
            {
                await cacheService.SetAsync(cachedKey, result.Value);
            }


            return result.Match(Results.Ok, ApiResults.Problem);


        }).WithTags(Tags.CatalogQuantities);
    }
}
