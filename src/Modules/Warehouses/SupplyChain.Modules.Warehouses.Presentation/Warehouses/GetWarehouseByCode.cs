using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SupplyChain.Common.Application.Caching;
using SupplyChain.Common.Domain;
using SupplyChain.Common.Presentation.ApiResults;
using SupplyChain.Common.Presentation.Endpoint;
using SupplyChain.Modules.Warehouses.Application.Warehouses.GetAllWarehouses;
using SupplyChain.Modules.Warehouses.Application.Warehouses.GetWarehouseByCode;

namespace SupplyChain.Modules.Warehouses.Presentation.Warehouses;




internal sealed class GetWarehouseByCode : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("warehouse/{code}", async Task<IResult> (string code, ISender sender, ICacheService cacheService) =>
        {

            string cacheKey = $"warehouse-code-{code}";

            WarehouseResponse? cached = await cacheService.GetAsync<WarehouseResponse>(cacheKey);
            if (cached != null)
            {
                var cachedResult = Result.Success<WarehouseResponse>(cached);
                return cachedResult.Match(Results.Ok, ApiResults.Problem);
            }

            Result<WarehouseResponse> result = await sender.Send(new GetWarehouseByCodeQuery(code));

            if (result.IsSuccess)
            {
                await cacheService.SetAsync(cacheKey, result);
            }

            return result.Match(Results.Ok, ApiResults.Problem);
        }).WithTags(Tags.Warehouses);
    }
}
