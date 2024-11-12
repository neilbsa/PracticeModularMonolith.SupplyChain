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
using SupplyChain.Modules.Warehouses.Application.Catalogs.ActivateCatalog;

namespace SupplyChain.Modules.Warehouses.Presentation.Catalogs;


internal sealed class ActivateCatalog : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("catalog/activate", async (ActivateCatalogRequest reuqest, ISender sender) =>
        {
            Result result = await sender.Send(new ActivateCatalogCommand(reuqest.Id));
            return result.Match(() => Results.Ok(), ApiResults.Problem);

        }).WithTags(Tags.Catalogs);
    }

    internal sealed class ActivateCatalogRequest
    {
        public Guid Id { get; set; }
    }
}
