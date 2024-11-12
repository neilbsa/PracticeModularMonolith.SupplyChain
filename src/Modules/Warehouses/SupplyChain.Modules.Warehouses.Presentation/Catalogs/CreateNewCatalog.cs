using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SupplyChain.Common.Domain;
using SupplyChain.Common.Presentation.ApiResults;
using SupplyChain.Common.Presentation.Endpoint;
using SupplyChain.Modules.Warehouses.Application.Catalogs.CreateNewCatalog;

namespace SupplyChain.Modules.Warehouses.Presentation.Catalogs;



internal sealed class CreateNewCatalog : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("catalog/create", async (CreateCatalogRequest request, ISender _sender) =>
        {

            Result<Guid> result = await _sender.Send(
                new CreateNewCatalogCommand(request.CatalogId, request.Description, request.Category));
            return result.Match(Results.Ok, ApiResults.Problem);
        }).WithTags(Tags.Catalogs);

    }
    internal sealed class CreateCatalogRequest
    {
        public string CatalogId { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
    }

}


 
    

