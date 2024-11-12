
using FluentValidation;

namespace SupplyChain.Modules.Warehouses.Application.Catalogs.CreateNewCatalog;



internal sealed class CreateNewCatalogCommandValidator : AbstractValidator<CreateNewCatalogCommand>
{
    public CreateNewCatalogCommandValidator()
    {
        RuleFor(x => x.CatalogId).NotEmpty().NotNull().MinimumLength(5).MaximumLength(20).Must(g=> !g.All(c=> char.IsWhiteSpace(c)));
        
        RuleFor(z => z.description).NotNull().NotEmpty().MaximumLength(100);
        RuleFor(z => z.category).NotEmpty().NotNull().MaximumLength(30);

    }

}
