using SupplyChain.Common.Application.Messaging;
using SupplyChain.Common.Domain;
using SupplyChain.Modules.Warehouses.Application.Warehouses.GetAllWarehouses;
using SupplyChain.Modules.Warehouses.Domain.Warehouses;
using SupplyChain.Modules.Warehouses.Domain.Warehouses.Repository;
using System;
using System.Linq;

namespace SupplyChain.Modules.Warehouses.Application.Warehouses.GetWarehouseByCode;


internal sealed class GetWarehouseByCodeQueryHandler : IQueryHandler<GetWarehouseByCodeQuery, WarehouseResponse>
{

    private readonly IWarehouseRepository _warehouseRepository;

    public GetWarehouseByCodeQueryHandler(IWarehouseRepository warehouseRepository)
    {
        _warehouseRepository = warehouseRepository;
    }

    public async Task<Result<WarehouseResponse>> Handle(GetWarehouseByCodeQuery request, CancellationToken cancellationToken)
    {
        Warehouse? warehouse = await _warehouseRepository.GetByWarehouseCodeAsync(request.Code, cancellationToken);


        if (warehouse == null)
        {
            return Result.Failure<WarehouseResponse>(WarehouseErrors.NotFound());
        }



        return new WarehouseResponse()
        {
            Address = $@"
                        {warehouse.Address.Street},
                        {warehouse.Address.City} ,
                       {warehouse.Address.Country} ,
                       {warehouse.Address.ZipCode}",
            Id = warehouse.Id,
            WarehouseCode = warehouse.Code.value,
            WarehouseDescription = warehouse.Description.value
        };



    }
}

