using Domain.Aggregates.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal class UserConfiguration : BaseConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.ToTable("Users");

        builder.Property(u => u.FirstName)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(u => u.LastName)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(u => u.UserName)
               .IsRequired()
               .HasMaxLength(50);

        builder.HasIndex(u => u.UserName)
               .IsUnique(); 

        builder.Property(u => u.Mobile)
               .IsRequired()
               .HasMaxLength(15);

        builder.HasIndex(u => u.Mobile)
               .IsUnique(); 

        builder.Property(u => u.Password)
               .IsRequired()
               .HasMaxLength(256); 


    }
}
