using SupplyChain.Common.Application.Messaging;
using SupplyChain.Common.Domain;
using SupplyChain.Modules.Warehouses.Application.Abstractions.Data;
using SupplyChain.Modules.Warehouses.Domain.Warehouses;
using SupplyChain.Modules.Warehouses.Domain.Warehouses.Repository;
using System;
using System.Linq;

namespace SupplyChain.Modules.Warehouses.Application.Warehouses.CreateNewWarehouse;


internal sealed class CreateNewWarehouseCommandHandler : ICommandHandler<CreateNewWarehouseCommand, Guid>
{

    private readonly IWarehouseRepository _warehouseRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CreateNewWarehouseCommandHandler(IWarehouseRepository warehouseRepository, IUnitOfWork unitOfWork)
    {
        _warehouseRepository = warehouseRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateNewWarehouseCommand request, CancellationToken cancellationToken)
    {

        Warehouse warehouse = await _warehouseRepository.GetByWarehouseCodeAsync(request.Code, cancellationToken);
        if (warehouse is not null)
        {
            return Result.Failure<Guid>(WarehouseErrors.CodeAlreadyExists(request.Code));
        }

        var newWarehouse = Warehouse.Create(
            request.Code,
            request.Description,
            request.Street,
            request.City,
            request.ZipCode,
            request.Country);

        _warehouseRepository.Add(newWarehouse);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return newWarehouse.Id;
    }
}
