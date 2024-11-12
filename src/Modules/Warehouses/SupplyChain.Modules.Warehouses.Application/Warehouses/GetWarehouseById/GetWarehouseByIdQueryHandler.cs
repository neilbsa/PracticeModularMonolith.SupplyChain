using SupplyChain.Common.Application.Messaging;
using SupplyChain.Common.Domain;
using SupplyChain.Modules.Warehouses.Domain.Warehouses;
using SupplyChain.Modules.Warehouses.Domain.Warehouses.Repository;
using System;
using System.Linq;

namespace SupplyChain.Modules.Warehouses.Application.Warehouses.GetWarehouseById;

internal sealed class GetWarehouseByIdQueryHandler : IQueryHandler<GetWarehouseByIdQuery, WarehouseByIdResponse?>
{
    private readonly IWarehouseRepository _warehouseRepository;

    public GetWarehouseByIdQueryHandler(IWarehouseRepository warehouseRepository)
    {
        _warehouseRepository = warehouseRepository;
    }

    public async Task<Result<WarehouseByIdResponse?>> Handle(GetWarehouseByIdQuery request, CancellationToken cancellationToken)
    {
        Warehouse? warehouse = await _warehouseRepository.GetByIdAsync(request.Id, cancellationToken);
        if (warehouse is null)
        {
            return Result.Failure<WarehouseByIdResponse?>(WarehouseErrors.NotFound());
        }

        return Result.Success<WarehouseByIdResponse?>(new WarehouseByIdResponse()
        {

            WarehouseCode = warehouse.Code.value,
            Id = warehouse.Id,
            WarehouseDescription = warehouse.Description.value,
            Address = $" {warehouse.Address.Street}, {warehouse.Address.City}, {warehouse.Address.Country}, {warehouse.Address.ZipCode}"

        });

    }
}
