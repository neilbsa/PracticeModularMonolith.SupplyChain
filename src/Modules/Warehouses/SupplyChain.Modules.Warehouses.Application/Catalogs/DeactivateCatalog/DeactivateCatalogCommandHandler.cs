using SupplyChain.Common.Application.Messaging;
using SupplyChain.Common.Domain;
using SupplyChain.Modules.Warehouses.Application.Abstractions.Data;
using SupplyChain.Modules.Warehouses.Domain.Catalogs;
using SupplyChain.Modules.Warehouses.Domain.Catalogs.Repository;
using System;
using System.Linq;


namespace SupplyChain.Modules.Warehouses.Application.Catalogs.DeactivateCatalog;



internal sealed class DeactivateCatalogCommandHandler : ICommandHandler<DeactivateCatalogCommand>
{

    private readonly ICatalogRepository _catalogRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeactivateCatalogCommandHandler(ICatalogRepository catalogRepository, IUnitOfWork unitOfWork)
    {
        _catalogRepository = catalogRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeactivateCatalogCommand request, CancellationToken cancellationToken)
    {
        Catalog catalog = await _catalogRepository.GetByIdAsync(request.catalogId, cancellationToken);
        if (catalog == null)
        {
            return Result.Failure(CatalogErrors.NotFound());
        }

        Result result = catalog.Deactivate();

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return result;
    }
}



