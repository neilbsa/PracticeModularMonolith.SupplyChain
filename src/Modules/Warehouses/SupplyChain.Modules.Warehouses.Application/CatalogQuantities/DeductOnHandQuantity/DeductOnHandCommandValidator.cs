using FluentValidation;
using System;
using System.Linq;

namespace SupplyChain.Modules.Warehouses.Application.CatalogQuantities.DeductOnHandQuantity;


internal sealed class DeductOnHandCommandValidator : AbstractValidator<DeductOnHandCommand>
{
    public DeductOnHandCommandValidator()
    {
        RuleFor(z => z.Quantity).GreaterThan(0);
    }
}
