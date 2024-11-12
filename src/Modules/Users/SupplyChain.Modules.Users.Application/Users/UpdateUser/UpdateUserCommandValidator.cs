using FluentValidation;
using System;
using System.Linq;

namespace SupplyChain.Modules.Users.Application.Users.UpdateUser;



internal sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(z => z.Firstname).NotEmpty().NotNull();
        RuleFor(z => z.LastName).NotEmpty().NotNull();
        RuleFor(z => z.Id).NotNull().NotEmpty();
    }
}

