using SupplyChain.Common.Application.Messaging;
using SupplyChain.Common.Domain;
using SupplyChain.Modules.Orders.Application.Abstractions.Data;
using SupplyChain.Modules.Orders.Domain.Orders;
using SupplyChain.Modules.Orders.Domain.Orders.Repository;
using SupplyChain.Modules.Users.PublicApi;
using SupplyChain.Modules.Warehouses.PublicApi;


namespace SupplyChain.Modules.Orders.Application.Orders.CreateNewOrder;


internal sealed class CreateNewOrderCommandHandler : ICommandHandler<CreateNewOrderCommand, Guid>
{

    private readonly IUserPublicApi _userPublicApi;
    private readonly IWarehousePublicApi _warehousePublicApi;
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CreateNewOrderCommandHandler(
        IUserPublicApi userPublicApi,
        IWarehousePublicApi warehousePublicApi,
        IOrderRepository orderRepository,
        IUnitOfWork unitOfWork)
    {
        _userPublicApi = userPublicApi;
        _warehousePublicApi = warehousePublicApi;
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateNewOrderCommand request, CancellationToken cancellationToken)
    {
        //check if user exist
        Result<UserApiResponse?> user = await _userPublicApi.GetUserByIdAsync(request.CustomerId, cancellationToken);
        if (user.IsFailure)
        {
            return Result.Failure<Guid>(user.Error);
        }
        //check if warehouse exist
        Result<WarehouseApiResponse?> warehouse = await _warehousePublicApi.GetWarehouseById(request.warehouseId, cancellationToken);
        if (warehouse.IsFailure)
        {
            return Result.Failure<Guid>(warehouse.Error);
        }



        //create order
        var order = Order.Create(request.CustomerId, request.warehouseId);
        _orderRepository.Add(order);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return order.Id;
    }
}
