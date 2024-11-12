using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupplyChain.Common.Domain;

namespace SupplyChain.Modules.Users.Domain.Users.Events;
internal sealed class NewUserCreatedDomainEvent(Guid id) : DomainEvent
{
    public Guid UserId { get; init; } = id;
}

