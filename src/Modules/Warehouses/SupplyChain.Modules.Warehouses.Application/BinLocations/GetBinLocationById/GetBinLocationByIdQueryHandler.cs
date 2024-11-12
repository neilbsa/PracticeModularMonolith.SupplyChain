using SupplyChain.Common.Application.Messaging;
using SupplyChain.Common.Domain;
using SupplyChain.Modules.Warehouses.Domain.BinLocations;
using SupplyChain.Modules.Warehouses.Domain.BinLocations.Repository;
using System;
using System.Linq;

namespace SupplyChain.Modules.Warehouses.Application.BinLocations.GetBinLocationById;
internal sealed class GetBinLocationByIdQueryHandler : IQueryHandler<GetBinLocationByIdQuery, BinlocationDto?>
{


    private readonly IBinLocationRepository _binlocationRepository;

    public GetBinLocationByIdQueryHandler(IBinLocationRepository binlocationRepository)
    {
        _binlocationRepository = binlocationRepository;
    }

    public async Task<Result<BinlocationDto?>> Handle(GetBinLocationByIdQuery request, CancellationToken cancellationToken)
    {
        BinLocation binlocation = await _binlocationRepository.GetByIdAsync(request.id, cancellationToken);

        if (binlocation == null)
        {

            return Result.Failure<BinlocationDto?>(BinLocationErrors.NotFound());

        }


        return new BinlocationDto() {
        
                     Code = binlocation.Code.value,
                      Id = binlocation.Id,
                       Name = binlocation.Name.value,
                      
                     
        
        };
    }
}


