using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SupplyChain.Modules.Users.Domain.Users;
using SupplyChain.Modules.Users.Domain.Users.Repository;
using SupplyChain.Modules.Users.Infrastructure.Database;

namespace SupplyChain.Modules.Users.Infrastructure.Users;
internal sealed class UserRepository(UserDBContext context) : IUserRepository
{
    public void Add(User user)
    {
        context.Users.Add(user);
    }

    public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await context.Users.SingleOrDefaultAsync(u => u.Email == new Email(email), cancellationToken);
    }

    public async Task<User?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Users.SingleOrDefaultAsync(u=>u.Id == id, cancellationToken);
    }
}
