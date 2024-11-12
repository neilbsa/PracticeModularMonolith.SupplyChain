using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SupplyChain.Common.Application.Caching;
using SupplyChain.Common.Domain;
using SupplyChain.Common.Presentation.ApiResults;
using SupplyChain.Common.Presentation.Endpoint;
using SupplyChain.Modules.Warehouses.Application.Warehouses.GetAllWarehouses;

namespace SupplyChain.Modules.Warehouses.Presentation.Warehouses;
internal sealed class GetWarehouses : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("warehouse", async Task<IResult> (
            int Limit,
            int Offset,
            ISender sender,
            ICacheService cache) =>
        {

               IReadOnlyList<WarehouseResponse> warehouseList = await cache.GetAsync<IReadOnlyList<WarehouseResponse>>("warehouses");


            if (warehouseList is not null)
            {
                return Results.Ok(warehouseList);
            }
         

            Result<IReadOnlyList<WarehouseResponse>> result = await sender.Send(new GetAllWarehouseQuery(Limit, Offset));
            if (result.IsSuccess)
            {
               await cache.SetAsync("warehouses", result.Value,TimeSpan.FromSeconds(30));
            }


            return result.Match(Results.Ok, ApiResults.Problem);


        }).WithTags(Tags.Warehouses);
    }
}
