using System;
using System.Linq;

namespace SupplyChain.Modules.Users.Application.Users.GetUserById;

public class UserResponse
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
