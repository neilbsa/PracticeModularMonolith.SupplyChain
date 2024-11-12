using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupplyChain.Common.Domain;
using SupplyChain.Modules.Users.Domain.Users.Events;

namespace SupplyChain.Modules.Users.Domain.Users;
public sealed class User : Entity
{
    private User(Guid id, Email email, FirstName firstName, LastName lastName)
    {
        Id = id;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
    }

    public Guid Id { get; set; }
    public Email Email { get; private set; }
    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }


    public static User Create(string FirstName, string LastName, string Email)
    {
        var user = new User(Guid.NewGuid(), new Email(Email), new FirstName(FirstName), new LastName(LastName));
        user.Raise(new NewUserCreatedDomainEvent(user.Id));
        return user;
    }

    public Result Update(string _firstName, string _lastName)
    {
        if(FirstName.Value == _firstName && LastName.Value == _lastName)
        {
            return Result.Success();
        }
        FirstName = new FirstName(_firstName);
        LastName = new LastName(_lastName);
        Raise(new UserUpdatedDomainEvent(Id, _firstName, _lastName));
        return Result.Success();
    }
}
