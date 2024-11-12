using SupplyChain.Common.Application.Messaging;
using SupplyChain.Common.Domain;
using SupplyChain.Modules.Warehouses.Application.Abstractions.Data;
using SupplyChain.Modules.Warehouses.Domain.BinLocations;
using SupplyChain.Modules.Warehouses.Domain.BinLocations.Repository;
using SupplyChain.Modules.Warehouses.Domain.CatalogQuantities;
using SupplyChain.Modules.Warehouses.Domain.CatalogQuantities.Repository;
using SupplyChain.Modules.Warehouses.Domain.Catalogs;
using SupplyChain.Modules.Warehouses.Domain.Catalogs.Repository;

namespace SupplyChain.Modules.Warehouses.Application.CatalogQuantities.AddOnhandQuantity;

internal sealed class AddOnHandCommandHandler : ICommandHandler<AddOnHandCommand>
{

    private readonly ICatalogQuantitiesRepository _catalogQuantitiesRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBinLocationRepository _binLocationRepository;
    private readonly ICatalogRepository _catalogRepository;
    public AddOnHandCommandHandler(ICatalogQuantitiesRepository catalogQuantitiesRepository, IUnitOfWork unitOfWork, IBinLocationRepository binLocationRepository, ICatalogRepository catalogRepository)
    {
        _catalogQuantitiesRepository = catalogQuantitiesRepository;
        _unitOfWork = unitOfWork;
        _binLocationRepository = binLocationRepository;
        _catalogRepository = catalogRepository;
    }

    public async Task<Result> Handle(AddOnHandCommand request, CancellationToken cancellationToken)
    {
        BinLocation location = await _binLocationRepository.GetByIdAsync(request.BinLocationId, cancellationToken);
        if (location is null)
        {
            return Result.Failure(BinLocationErrors.NotFound());
        }

        Catalog catalog = await _catalogRepository.GetByIdAsync(request.CatalogId, cancellationToken);
        if (catalog is null)
        {
            return Result.Failure(CatalogErrors.NotFound());
        }

        CatalogQuantity catalogQuantity =await 
            _catalogQuantitiesRepository
            .GetByCatalogAndBinLocationIdsAsync(request.CatalogId, request.BinLocationId, cancellationToken);


        if(catalogQuantity is null)
        {
            return Result.Failure(CatalogQuantityErrors.NotFound());
        }


       
         catalogQuantity.AddOnHand(new Quantity(request.quantity));
        

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();    
    }
}
