
using FluentValidation;

namespace SupplyChain.Modules.Warehouses.Application.CatalogQuantities.GetByCatalogIdAndWarehouseId;



internal sealed class GetByCatalogIdAndWarehouseIdQueryValidator : AbstractValidator<GetByCatalogIdAndWarehouseIdQuery>
{
    public GetByCatalogIdAndWarehouseIdQueryValidator()
    {
        RuleFor(z => z.warehouseId).NotNull().NotEmpty();

        RuleFor(z => z.CatalogId).NotNull().NotEmpty();
    }
}
