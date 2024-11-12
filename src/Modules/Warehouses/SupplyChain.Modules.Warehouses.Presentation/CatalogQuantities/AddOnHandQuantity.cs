using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SupplyChain.Common.Domain;
using SupplyChain.Common.Presentation.ApiResults;
using SupplyChain.Common.Presentation.Endpoint;
using SupplyChain.Modules.Warehouses.Application.CatalogQuantities.AddOnhandQuantity;

namespace SupplyChain.Modules.Warehouses.Presentation.CatalogQuantities;
internal sealed class AddOnHandQuantity : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("catalog-quantity/add-onhand",async (AddOnHandRequest request, ISender _sender) => {
           
          Result result = await _sender.Send(new AddOnHandCommand(request.CatalogId,request.BinLocationId,request.Quantity));
            return result.Match(() => Results.Ok(), ApiResults.Problem);
           
        }).WithTags(Tags.CatalogQuantities);
    }

    internal sealed class AddOnHandRequest
    {
        public Guid CatalogId { get; set; }
        public Guid BinLocationId { get; set; }
        public decimal Quantity { get; set; }
    }
}
