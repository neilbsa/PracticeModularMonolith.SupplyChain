using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SupplyChain.Modules.Users.Application.Abstractions.Data;
using SupplyChain.Modules.Users.Domain.Users;
using SupplyChain.Modules.Users.Infrastructure.Users;

namespace SupplyChain.Modules.Users.Infrastructure.Database;
public sealed class UserDBContext(DbContextOptions<UserDBContext> opt) : DbContext(opt) , IUnitOfWork
{
    internal DbSet<User> Users { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Users);
        modelBuilder.ApplyConfiguration(new UserConfigurations());

    }

}
