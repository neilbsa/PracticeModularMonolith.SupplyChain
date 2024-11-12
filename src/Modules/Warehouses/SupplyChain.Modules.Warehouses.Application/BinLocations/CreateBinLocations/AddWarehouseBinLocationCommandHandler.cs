using System.Diagnostics.CodeAnalysis;
using SupplyChain.Common.Application.Messaging;
using SupplyChain.Common.Domain;
using SupplyChain.Modules.Warehouses.Application.Abstractions.Data;
using SupplyChain.Modules.Warehouses.Domain.BinLocations;
using SupplyChain.Modules.Warehouses.Domain.BinLocations.Repository;
using SupplyChain.Modules.Warehouses.Domain.Warehouses;
using SupplyChain.Modules.Warehouses.Domain.Warehouses.Repository;

namespace SupplyChain.Modules.Warehouses.Application.BinLocations.CreateBinLocations;


internal sealed class AddWarehouseBinLocationCommandHandler : ICommandHandler<AddWarehouseBinLocationCommand,Guid>
{

    private readonly IBinLocationRepository _binLocationRepository;
    private readonly IWarehouseRepository _warehouseRepository;
    private readonly IUnitOfWork _unitOfWork;
    public AddWarehouseBinLocationCommandHandler(
        IBinLocationRepository binLocationRepository,
        IWarehouseRepository warehouseRepository,
        IUnitOfWork unitOfWork)
    {
        _binLocationRepository = binLocationRepository;
        _warehouseRepository = warehouseRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(AddWarehouseBinLocationCommand request, CancellationToken cancellationToken)
    {
        BinLocation? binlocation = await _binLocationRepository.GetByCodeAsync(request.BinlocationCode, cancellationToken);
        if (binlocation is not null)
        {
            return Result.Failure<Guid>(BinLocationErrors.CodeExist(request.BinlocationCode));
        }

        Warehouse warehouse = await _warehouseRepository.GetByIdAsync(request.WarehouseId, cancellationToken);

        if (warehouse is null)
        {
            return Result.Failure<Guid>(WarehouseErrors.NotFound());
        }

        var Location = BinLocation.Create(request.BinlocationCode, request.BinLocationName, request.WarehouseId);


        _binLocationRepository.Add(Location);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Location.Id;
    }
}
