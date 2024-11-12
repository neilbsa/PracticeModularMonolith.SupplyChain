using System;
using System.Linq;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SupplyChain.Common.Domain;
using SupplyChain.Common.Presentation.ApiResults;
using SupplyChain.Common.Presentation.Endpoint;
using SupplyChain.Modules.Warehouses.Application.CatalogQuantities.DeductOnHandQuantity;

namespace SupplyChain.Modules.Warehouses.Presentation.CatalogQuantities;

internal sealed class DeductOnHandQuantity : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("catalog-quantity/deduct-onhand", async (DeductOnHandRequest request, ISender sender) => { 
        
        
               Result result = await sender.Send(new DeductOnHandCommand(request.CatalogId,request.BinLocationId,request.Quantity));


            return result.Match(()=> Results.Ok(), ApiResults.Problem);
        
        }).WithTags(Tags.CatalogQuantities);
    }


    internal sealed class DeductOnHandRequest
    {
        public Guid CatalogId { get; set; }
        public Guid BinLocationId { get; set; }
        public decimal Quantity { get; set; }
    }
}
