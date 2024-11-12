using System;
using System.Linq;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SupplyChain.Common.Domain;
using SupplyChain.Common.Presentation.ApiResults;
using SupplyChain.Common.Presentation.Endpoint;
using SupplyChain.Modules.Warehouses.Application.Catalogs.DeactivateCatalog;

namespace SupplyChain.Modules.Warehouses.Presentation.Catalogs;


internal sealed class DeactivateCatalog : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("catalog/deactivate", async (DeactivateCatalogRequest reuqest, ISender sender) => 
        {
            Result result = await sender.Send(new DeactivateCatalogCommand(reuqest.Id));
            return result.Match(()=>Results.Ok(), ApiResults.Problem);

        }).WithTags(Tags.Catalogs);


       
    }


    internal sealed class DeactivateCatalogRequest
    {
        public Guid Id { get; set; }
    }
}
