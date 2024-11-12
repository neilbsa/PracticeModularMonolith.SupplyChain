using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SupplyChain.Common.Domain;
using SupplyChain.Common.Presentation.ApiResults;
using SupplyChain.Common.Presentation.Endpoint;
using SupplyChain.Modules.Orders.Application.Orders.AddItemsToOrder;
using SupplyChain.Modules.Warehouses.Presentation;
using System;
using System.Linq;

namespace SupplyChain.Modules.Orders.Presentation.Orders;



internal sealed class AddCatalogToOrder : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("orders/add-catalog-order", async Task<IResult> (AddCatalogToOrderRequest request, ISender _sender) =>
        {
            Result result = await _sender.Send(new AddCatalogToOrderCommand(request.CatalogId, request.OrderId, request.Quantity));

            return result.Match(() => Results.Ok(), ApiResults.Problem);

        }).WithTags(Tags.Orders);
    }

    internal sealed class AddCatalogToOrderRequest
    {
        public Guid OrderId { get; set; }
        public Guid CatalogId { get; set; }
        public decimal Quantity { get; set; }
    }
}
