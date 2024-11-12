using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupplyChain.Common.Application.Messaging;
using SupplyChain.Common.Domain;
using SupplyChain.Modules.Users.Domain.Users;
using SupplyChain.Modules.Users.Domain.Users.Repository;

namespace SupplyChain.Modules.Users.Application.Users.GetUserById;
public sealed record GetUserByIdQuery(Guid Id) : IQuery<UserResponse?>;
