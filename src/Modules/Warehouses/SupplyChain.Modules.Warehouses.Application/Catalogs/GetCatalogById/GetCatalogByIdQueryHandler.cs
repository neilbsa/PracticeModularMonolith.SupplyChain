using SupplyChain.Common.Application.Messaging;
using SupplyChain.Common.Domain;
using SupplyChain.Modules.Warehouses.Application.Catalogs.GetCatalogs;
using SupplyChain.Modules.Warehouses.Domain.Catalogs;
using SupplyChain.Modules.Warehouses.Domain.Catalogs.Repository;
using System;
using System.Linq;

namespace SupplyChain.Modules.Warehouses.Application.Catalogs.GetCatalogById;


internal sealed class GetCatalogByIdQueryHandler : IQueryHandler<GetCatalogByIdQuery, CatalogResponse>
{

    private readonly ICatalogRepository _catalogRepository;

    public GetCatalogByIdQueryHandler(ICatalogRepository catalogRepository)
    {
        _catalogRepository = catalogRepository;
    }

    public async Task<Result<CatalogResponse>> Handle(GetCatalogByIdQuery request, CancellationToken cancellationToken)
    {
        Catalog result = await _catalogRepository.GetByIdAsync(request.id, cancellationToken);

        if (result == null)
        {
            return Result.Failure<CatalogResponse>(CatalogErrors.NotFound());
        }
        var cat = new CatalogResponse()
        {
            CatalogId = result.CatalogId.value,
            Id = result.Id,
            Category = result.Category.value,
            Description = result.Description.value

        };


        return Result.Success(cat);
    }
}
