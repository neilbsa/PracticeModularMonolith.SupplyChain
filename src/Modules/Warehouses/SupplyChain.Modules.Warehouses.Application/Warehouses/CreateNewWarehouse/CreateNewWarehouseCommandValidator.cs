using FluentValidation;
using System;
using System.Linq;

namespace SupplyChain.Modules.Warehouses.Application.Warehouses.CreateNewWarehouse;

internal sealed class CreateNewWarehouseCommandValidator : AbstractValidator<CreateNewWarehouseCommand>
{
    public CreateNewWarehouseCommandValidator()
    {
        RuleFor(c => c.Code).MinimumLength(3)
            .MaximumLength(5).NotEmpty().NotNull().Must(g => !g.All(c => char.IsWhiteSpace(c)));
        RuleFor(c => c.Description).MaximumLength(100).NotNull().NotEmpty();
        RuleFor(c => c.Street).NotNull().NotEmpty();
        RuleFor(c => c.City).NotNull().NotEmpty();
        RuleFor(c => c.Country).NotNull().NotEmpty();
        RuleFor(c => c.ZipCode).NotNull().NotEmpty();
    }

  
}
