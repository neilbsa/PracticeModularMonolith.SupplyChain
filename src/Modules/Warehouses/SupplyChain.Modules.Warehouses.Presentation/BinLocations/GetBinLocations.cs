using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SupplyChain.Common.Application.Caching;
using SupplyChain.Common.Domain;
using SupplyChain.Common.Presentation.ApiResults;
using SupplyChain.Common.Presentation.Endpoint;
using SupplyChain.Modules.Warehouses.Application.BinLocations.GetBinLocations;
using SupplyChain.Modules.Warehouses.Domain.BinLocations;
using System;
using System.Linq;

namespace SupplyChain.Modules.Warehouses.Presentation.BinLocations;



internal sealed class GetBinLocations : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("binlocation/get/{Limit}/page/{Offset}", async Task<IResult> (
            int Limit,
            int Offset,
            ISender sender,
            ICacheService cachingService) =>
        {

            string cacheKey = $"binlocation-get-limit-{Limit}-offset-{Offset}";

            IReadOnlyList<BinLocationResponse> cachedData = await cachingService.GetAsync<IReadOnlyList<BinLocationResponse>>(cacheKey);

            if (cachedData is not null)
            {
                return Result.Success(cachedData).Match(Results.Ok, ApiResults.Problem);
            }


            Result<IReadOnlyList<BinLocationResponse>> result = await sender.Send(new GetBinLocationsQuery(Limit, Offset));

            if (result.IsSuccess)
            {
                await cachingService.SetAsync<IReadOnlyList<BinLocationResponse>>(cacheKey, result.Value);
            }

            return result.Match(Results.Ok, ApiResults.Problem);
        }).WithTags(Tags.BinLocations);
    }
}
