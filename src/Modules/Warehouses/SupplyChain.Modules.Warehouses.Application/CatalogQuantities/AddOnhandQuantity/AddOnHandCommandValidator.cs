using FluentValidation;
using System;
using System.Linq;

namespace SupplyChain.Modules.Warehouses.Application.CatalogQuantities.AddOnhandQuantity;




internal sealed class AddOnHandCommandValidator : AbstractValidator<AddOnHandCommand>
{
    public AddOnHandCommandValidator()
    {
        RuleFor(z => z.quantity).GreaterThan(0);

    }
}
