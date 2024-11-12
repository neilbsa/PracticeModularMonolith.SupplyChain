using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SupplyChain.Modules.Users.Domain.Users;

namespace SupplyChain.Modules.Users.Infrastructure.Users;
internal sealed class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.HasKey(x => x.Id);
        builder.Property(z => z.FirstName)
            .IsRequired()
            .HasMaxLength(100)
            .HasConversion(z => z.Value, value => new FirstName(value));

        builder.Property(z => z.LastName)
            .IsRequired()
            .HasMaxLength(100)
            .HasConversion(z => z.Value, value => new LastName(value));

        builder.Property(z => z.Email)
            .IsRequired()
            .HasMaxLength (300)
            .HasConversion(z => z.Value, value => new Email(value));

        builder.HasIndex(x => x.Email).IsUnique();

    }
}
