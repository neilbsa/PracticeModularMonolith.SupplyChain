using SupplyChain.Common.Application.Messaging;
using SupplyChain.Common.Domain;
using SupplyChain.Modules.Orders.Application.Abstractions.Data;
using SupplyChain.Modules.Orders.Domain.Orders;
using SupplyChain.Modules.Orders.Domain.Orders.Repository;

using SupplyChain.Modules.Warehouses.PublicApi;


namespace SupplyChain.Modules.Orders.Application.Orders.AddItemsToOrder;

internal sealed class AddCatalogToOrderCommandHandler : ICommandHandler<AddCatalogToOrderCommand>
{


    private readonly IOrderRepository _orderRepository;
    private readonly IWarehousePublicApi _warehousePublicApi;

    public AddCatalogToOrderCommandHandler(

        IOrderRepository orderRepository,
        IWarehousePublicApi warehousePublicApi)
    {

        _orderRepository = orderRepository;
        _warehousePublicApi = warehousePublicApi;
    }

    public async Task<Result> Handle(AddCatalogToOrderCommand request, CancellationToken cancellationToken)
    {
        //validate if order exist
        Order? order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);

        if(order is null)
        {
            return Result.Failure(OrderErrors.OrderNotFound());
        }


        //validate catalog
        Result<CatalogApiResponse?> catalog = await _warehousePublicApi.GetCatalogById(request.CatalogId, cancellationToken);

        if (catalog.IsFailure)
        {
            return Result.Failure(catalog.Error);
        }

        //validate if the catalog exist in this warehouse
        Result<CatalogQuantityApiResponse> catalogQuantity = await _warehousePublicApi.GetQuantityByWarehouseIdAndCatalogIdAsync(order.WarehouseId.Value, catalog!.Value.Id, cancellationToken);

        if (catalogQuantity.IsFailure)
        {
            return Result.Failure(catalogQuantity.Error);
        }


        order.AddOrUpdateDetailsToOrder(request.CatalogId, request.Quantity);


        await _orderRepository.SaveOrderChangesAsync(cancellationToken);

        return Result.Success();
    }
}


