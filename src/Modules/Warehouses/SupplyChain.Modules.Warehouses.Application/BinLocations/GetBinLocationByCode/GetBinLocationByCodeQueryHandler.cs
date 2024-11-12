using SupplyChain.Common.Application.Messaging;
using SupplyChain.Common.Domain;
using SupplyChain.Modules.Warehouses.Application.BinLocations.GetBinLocationById;
using SupplyChain.Modules.Warehouses.Domain.BinLocations;
using SupplyChain.Modules.Warehouses.Domain.BinLocations.Repository;

namespace SupplyChain.Modules.Warehouses.Application.BinLocations.GetBinLocationByCode;

internal sealed class GetBinLocationByCodeQueryHandler : IQueryHandler<GetBinLocationByCodeQuery, BinlocationDto?>
{
    private readonly IBinLocationRepository _binLocationRepository;

    public GetBinLocationByCodeQueryHandler(IBinLocationRepository binLocationRepository)
    {
        _binLocationRepository = binLocationRepository;
    }

    public async Task<Result<BinlocationDto?>> Handle(GetBinLocationByCodeQuery request, CancellationToken cancellationToken)
    {
        BinLocation? binLocation = await _binLocationRepository.GetByCodeAsync(request.code, cancellationToken);

        if (binLocation == null)
        {
            return Result.Failure<BinlocationDto?>(BinLocationErrors.NotFound());
        }


        return new BinlocationDto() {

            Code = binLocation.Code.value,
            Id = binLocation.Id,
            Name = binLocation.Name.value,

        };

    }
}
