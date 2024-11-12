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
using SupplyChain.Modules.Warehouses.Application.BinLocations.CreateBinLocations;
using SupplyChain.Modules.Warehouses.Domain.BinLocations;

namespace SupplyChain.Modules.Warehouses.Presentation.BinLocations;




internal sealed class AddBinLocation : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("binlocation/create", async (AddBinLocationToWarehouseRequest request, ISender sender) =>
        {
            var command = new AddWarehouseBinLocationCommand(
                request.BinLocationCode,
                request.BinLocationName,
                request.WarehouseId);
            Result<Guid> result = await sender.Send(command);
            return result.Match(Results.Ok, ApiResults.Problem);
        }).WithTags(Tags.BinLocations);
    }

    internal sealed class AddBinLocationToWarehouseRequest
    {
        public string BinLocationCode { get; set; }
        public string BinLocationName { get; set; }
        public Guid WarehouseId { get; set; }
    }
}
