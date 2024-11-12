using System.Globalization;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SupplyChain.Common.Application.Caching;
using SupplyChain.Common.Domain;
using SupplyChain.Common.Presentation.ApiResults;
using SupplyChain.Common.Presentation.Endpoint;
using SupplyChain.Modules.Warehouses.Application.Warehouses.CreateNewWarehouse;

namespace SupplyChain.Modules.Warehouses.Presentation.Warehouses;


internal sealed class CreateNewWarehouse : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("warehouse/create",async (CreateWarehouseRequest request,ISender sender,ICacheService cacheService) => {

         




          Result<Guid> result= await sender.Send(new CreateNewWarehouseCommand(
              request.Code,
              request.Description,
              request.Street,
              request.City,
              request.ZipCode,
              request.Country));

            


            return result.Match(Results.Ok, ApiResults.Problem); 
           


        }).WithTags(Tags.Warehouses);
    }

    internal sealed class CreateWarehouseRequest
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
    }
}
