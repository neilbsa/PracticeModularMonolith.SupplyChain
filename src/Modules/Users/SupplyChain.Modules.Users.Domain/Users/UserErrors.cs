using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupplyChain.Common.Domain;

namespace SupplyChain.Modules.Users.Domain.Users;
public static class UserErrors
{

    public static Error NotFound(Guid id) => Error.NotFound("User.NotFound", $"User with id {id} is not found");
    public static Error AlreadyExist(string email) => Error.Conflict("User.AlreadyExist", $"User with id {email} already exist");
}
