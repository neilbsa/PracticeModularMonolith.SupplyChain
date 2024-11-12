using System.Diagnostics.CodeAnalysis;
using SupplyChain.Common.Application.Messaging;
using SupplyChain.Common.Domain;
using SupplyChain.Modules.Warehouses.Application.Abstractions.Data;
using SupplyChain.Modules.Warehouses.Domain.BinLocations;
using SupplyChain.Modules.Warehouses.Domain.BinLocations.Repository;
using SupplyChain.Modules.Warehouses.Domain.CatalogQuantities;
using SupplyChain.Modules.Warehouses.Domain.CatalogQuantities.Repository;
using SupplyChain.Modules.Warehouses.Domain.Catalogs;
using SupplyChain.Modules.Warehouses.Domain.Catalogs.Repository;
namespace SupplyChain.Modules.Warehouses.Application.CatalogQuantities.AddCatalogToBinLocation;


internal sealed class CatalogAddToBinLocationCommandHandler : ICommandHandler<CatalogAddToBinLocationCommand,Guid>
{
    private readonly ICatalogRepository _catalogRepository;
    private readonly ICatalogQuantitiesRepository _quantitiesRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBinLocationRepository _binLocationRepository;
    public CatalogAddToBinLocationCommandHandler(
        ICatalogRepository catalogRepository,
        ICatalogQuantitiesRepository quantitiesRepository,
        IUnitOfWork unitOfWork,
        IBinLocationRepository binLocationRepository)
    {
        _catalogRepository = catalogRepository;
        _quantitiesRepository = quantitiesRepository;
        _unitOfWork = unitOfWork;
        _binLocationRepository = binLocationRepository;
    }

    public async Task<Result<Guid>> Handle(CatalogAddToBinLocationCommand request, CancellationToken cancellationToken)
    {
        CatalogQuantity catalogQuantity = await _quantitiesRepository.
            GetByCatalogAndBinLocationIdsAsync(request.CatalogId, request.BinLocationId, cancellationToken);


        if (catalogQuantity is not null)
        {
            return Result.Failure<Guid>(CatalogQuantityErrors.AlreadyTagged());
        }


        Catalog catalog = await _catalogRepository.GetByIdAsync(request.CatalogId, cancellationToken);

        if (catalog is null)
        {
            return Result.Failure<Guid>(CatalogErrors.NotFound());
        }

        BinLocation location = await _binLocationRepository.GetByIdAsync(request.BinLocationId, cancellationToken);

        if (location is null)
        {
            return Result.Failure<Guid>(BinLocationErrors.NotFound());
        }

        CatalogQuantity? catalogInWarehouse = await _quantitiesRepository.GetByCatalogIdAndWarehouseIdAsync(catalog.Id, location.WarehouseId, cancellationToken);

        if(catalogInWarehouse is not null)
        {
            return Result.Failure<Guid>(CatalogQuantityErrors.AlreadyTagged());
        }


        var catQuantity = CatalogQuantity.Create(request.BinLocationId, request.CatalogId, 0);
        _quantitiesRepository.Add(catQuantity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return catQuantity.Id;
    }
}
