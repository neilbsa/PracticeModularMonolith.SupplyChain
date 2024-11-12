using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Xsl;
using FluentValidation;
using SupplyChain.Common.Application.Messaging;


namespace SupplyChain.Modules.Orders.Application.Orders.AddItemsToOrder;
public sealed record AddCatalogToOrderCommand(Guid CatalogId, Guid OrderId,decimal Quantity) : ICommand;


internal sealed class AddCatalogToOrderCommandValidator : AbstractValidator<AddCatalogToOrderCommand>
{
    public AddCatalogToOrderCommandValidator()
    {
        RuleFor(z => z.CatalogId).NotEmpty();
        RuleFor(z => z.OrderId).NotEmpty();
        RuleFor(z => z.Quantity).GreaterThan(decimal.Zero);

    }
}
