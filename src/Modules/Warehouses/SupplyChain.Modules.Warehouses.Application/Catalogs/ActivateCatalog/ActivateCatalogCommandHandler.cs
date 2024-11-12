
using SupplyChain.Common.Application.Messaging;
using SupplyChain.Common.Domain;
using SupplyChain.Modules.Warehouses.Application.Abstractions.Data;
using SupplyChain.Modules.Warehouses.Domain.Catalogs;
using SupplyChain.Modules.Warehouses.Domain.Catalogs.Repository;

namespace SupplyChain.Modules.Warehouses.Application.Catalogs.ActivateCatalog;



internal sealed class ActivateCatalogCommandHandler : ICommandHandler<ActivateCatalogCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICatalogRepository _catalogRepository;

    public ActivateCatalogCommandHandler(IUnitOfWork unitOfWork, ICatalogRepository catalogRepository)
    {
        _unitOfWork = unitOfWork;
        _catalogRepository = catalogRepository;
    }

    public async Task<Result> Handle(ActivateCatalogCommand request, CancellationToken cancellationToken)
    {
        Catalog catalog = await _catalogRepository.GetByIdAsync(request.CatalogId, cancellationToken);

        if (catalog == null)
        {
            return Result.Failure(CatalogErrors.NotFound());
        }

        Result result = catalog.Active();

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return result;

    }
}
