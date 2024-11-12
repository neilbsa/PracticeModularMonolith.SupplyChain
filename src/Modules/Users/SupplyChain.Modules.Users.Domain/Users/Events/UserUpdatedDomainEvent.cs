using SupplyChain.Common.Domain;
using System;
using System.Linq;

namespace SupplyChain.Modules.Users.Domain.Users.Events;


internal sealed class UserUpdatedDomainEvent(Guid id, string _firstName, string _lastName) : DomainEvent
{
    public Guid UserId { get; init; } = id;
    public string FirstName { get; init; } = _firstName;
    public string LastName { get; init; } = _lastName;
}

