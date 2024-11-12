
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SupplyChain.Common.Application.Caching;
using SupplyChain.Common.Domain;
using SupplyChain.Common.Presentation.ApiResults;
using SupplyChain.Common.Presentation.Endpoint;

using SupplyChain.Modules.Warehouses.Application.CatalogQuantities.AddCatalogToBinLocation;



namespace SupplyChain.Modules.Warehouses.Presentation.CatalogQuantities;





internal sealed class AddCatalogToBinLocation : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("catalog/add-binlocation", async (AddCatalogToBinLocationRequest request, ISender _sender) =>
        {

            Result<Guid> result = await _sender.Send(new CatalogAddToBinLocationCommand(request.CatalogId, request.BinLocationId));

            return result.Match(Results.Ok, ApiResults.Problem);


        }).WithTags(Tags.CatalogQuantities);



    }



    internal sealed class AddCatalogToBinLocationRequest
    {
        public Guid CatalogId { get; set; }
        public Guid BinLocationId { get; set; }
    }
}
