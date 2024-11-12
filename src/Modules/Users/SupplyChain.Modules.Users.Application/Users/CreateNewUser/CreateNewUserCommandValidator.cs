using FluentValidation;
using System;
using System.Linq;

namespace SupplyChain.Modules.Users.Application.Users.CreateNewUser;



internal sealed class CreateNewUserCommandValidator : AbstractValidator<CreateNewUserCommand>
{
    public CreateNewUserCommandValidator()
    {
        RuleFor(z => z.Email).EmailAddress().NotEmpty().NotNull();
        RuleFor(g => g.FirstName).NotEmpty().MaximumLength(20).NotNull();
        RuleFor(g => g.LastName).NotEmpty().MaximumLength(20).NotNull();
    }
}
