using SupplyChain.Common.Application.Messaging;
using SupplyChain.Common.Domain;
using SupplyChain.Modules.Warehouses.Application.Abstractions.Data;
using SupplyChain.Modules.Warehouses.Domain.BinLocations;
using SupplyChain.Modules.Warehouses.Domain.BinLocations.Repository;
using SupplyChain.Modules.Warehouses.Domain.CatalogQuantities;
using SupplyChain.Modules.Warehouses.Domain.CatalogQuantities.Repository;
using SupplyChain.Modules.Warehouses.Domain.Catalogs;
using SupplyChain.Modules.Warehouses.Domain.Catalogs.Repository;
using System;
using System.Linq;

namespace SupplyChain.Modules.Warehouses.Application.CatalogQuantities.DeductOnHandQuantity;


internal sealed class DeductOnHandCommandHandler : ICommandHandler<DeductOnHandCommand>
{
    private readonly ICatalogQuantitiesRepository _catalogQuantitiesRepository;
    private readonly IUnitOfWork _unitOfWork;
  
    public DeductOnHandCommandHandler(ICatalogQuantitiesRepository catalogQuantitiesRepository, IUnitOfWork unitOfWork)
    {
        _catalogQuantitiesRepository = catalogQuantitiesRepository;
        _unitOfWork = unitOfWork;
    
    }

    public async Task<Result> Handle(DeductOnHandCommand request, CancellationToken cancellationToken)
    {
     


        CatalogQuantity catalogQuantity = await
            _catalogQuantitiesRepository.GetByCatalogAndBinLocationIdsAsync(request.CatalogId, request.BinLocationId, cancellationToken);

        if (catalogQuantity is null)
        {
            return Result.Failure(CatalogQuantityErrors.NotFound());
        }



        Result result = catalogQuantity.DeductOnHand(new Quantity(request.Quantity));


        if (result.IsSuccess)
        {
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }



        return result;
    }
}
