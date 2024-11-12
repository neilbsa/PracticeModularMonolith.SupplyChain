using SupplyChain.Common.Application.Messaging;
using SupplyChain.Common.Domain;
using SupplyChain.Modules.Warehouses.Application.Abstractions.Data;
using SupplyChain.Modules.Warehouses.Domain.Catalogs;
using SupplyChain.Modules.Warehouses.Domain.Catalogs.Repository;

namespace SupplyChain.Modules.Warehouses.Application.Catalogs.CreateNewCatalog;


internal sealed class CreateNewCatalogCommandHandler : ICommandHandler<CreateNewCatalogCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;


    private readonly ICatalogRepository _catalogRepository;
    public CreateNewCatalogCommandHandler(IUnitOfWork unitOfWork, ICatalogRepository catalogRepository)
    {
        _unitOfWork = unitOfWork;
        _catalogRepository = catalogRepository;
    }

    public async Task<Result<Guid>> Handle(CreateNewCatalogCommand request, CancellationToken cancellationToken)
    {

        if (await _catalogRepository.CatalogIdExists(request.CatalogId, cancellationToken))
        {
            return Result.Failure<Guid>(CatalogErrors.IdAlreadyExist(request.CatalogId));
        }
        var catalog = Catalog.Create(request.CatalogId, request.description, request.category);
        _catalogRepository.AddCatalog(catalog);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return catalog.Id;
    }
}
