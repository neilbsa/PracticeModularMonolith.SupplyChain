using FluentValidation;

namespace SupplyChain.Modules.Warehouses.Application.BinLocations.CreateBinLocations;



internal sealed class AddWarehouseBinLocationValidator : AbstractValidator<AddWarehouseBinLocationCommand>
{
    public AddWarehouseBinLocationValidator()
    {
        RuleFor(c => c.WarehouseId).NotEmpty().NotNull();
        RuleFor(c => c.BinlocationCode).NotNull().NotEmpty()
            .MinimumLength(3)
            .MaximumLength(5)
            .Must(g => !g.All(c => char.IsWhiteSpace(c)));
        RuleFor(c => c.BinLocationName).NotNull().NotEmpty();
    }
}
