using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SupplyChain.Common.Application.Caching;
using SupplyChain.Common.Domain;
using SupplyChain.Common.Presentation.ApiResults;
using SupplyChain.Common.Presentation.Endpoint;
using SupplyChain.Modules.Warehouses.Application.BinLocations.GetBinLocationByCode;
using SupplyChain.Modules.Warehouses.Application.BinLocations.GetBinLocationById;
using System;
using System.Linq;

namespace SupplyChain.Modules.Warehouses.Presentation.BinLocations;

internal sealed class GetBinLocationByCode : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("binlocation/get-code/{code}", async Task<IResult> (string code, ISender sender, ICacheService cache) =>
        {
            string cachedKey = $"binlocation-get-code-{code}";

            BinlocationDto cachedData = await cache.GetAsync<BinlocationDto>(cachedKey);

            if (cachedData != null)
            {
                return Result.Success(cachedData).Match(Results.Ok, ApiResults.Problem);
            }

            Result<BinlocationDto?> result = await sender.Send(new GetBinLocationByCodeQuery(code));

            if (result.IsSuccess)
            {
                await cache.SetAsync(cachedKey, result.Value);
            }

            return result.Match(Results.Ok, ApiResults.Problem);
        }).WithTags(Tags.BinLocations);
    }
}
