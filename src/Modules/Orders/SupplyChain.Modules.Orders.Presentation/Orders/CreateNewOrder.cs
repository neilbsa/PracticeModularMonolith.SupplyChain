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
using SupplyChain.Modules.Orders.Application.Orders.AddItemsToOrder;
using SupplyChain.Modules.Orders.Application.Orders.CreateNewOrder;
using SupplyChain.Modules.Orders.Domain.Orders;
using SupplyChain.Modules.Warehouses.Presentation;

namespace SupplyChain.Modules.Orders.Presentation.Orders;
internal sealed class CreateNewOrder : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("orders/create-new", async Task<IResult> (CreateOrderRequest request, ISender sender) =>
        {
            Result<Guid> result = await sender.Send(new CreateNewOrderCommand(request.WarehouseId, request.CustomerId));

            return result.Match(Results.Ok, ApiResults.Problem);

        }).WithTags(Tags.Orders);
    }




    internal sealed class CreateOrderRequest
    {
        public Guid WarehouseId { get; set; }
        public Guid CustomerId { get; set; }
    }
}
