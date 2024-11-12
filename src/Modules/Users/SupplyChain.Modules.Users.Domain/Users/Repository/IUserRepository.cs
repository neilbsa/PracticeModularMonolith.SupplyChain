using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyChain.Modules.Users.Domain.Users.Repository;
public interface IUserRepository 
{
    void Add(User user);    
    Task<User?> GetUserByIdAsync(Guid id,CancellationToken cancellationToken = default);
    Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);

}
