using Domain.Aggregates.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal class CustomerConfiguration : BaseConfiguration<Customer>
{
    public override void Configure(EntityTypeBuilder<Customer> builder)
    {
        base.Configure(builder);

        builder.ToTable("Customers");

        builder.Property(u => u.FirstName)
                .HasMaxLength(100);

        builder.Property(u => u.LastName)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(u => u.NationalCode)
               .HasMaxLength(10);

        builder.HasIndex(u => u.NationalCode)
               .IsUnique()
               .HasFilter("[NationalCode] IS NOT NULL"); 

        builder.Property(u => u.Mobile)
               .IsRequired()
               .HasMaxLength(15);

        builder.HasIndex(u => u.Mobile)
               .IsUnique();

        builder.Property(u => u.Email)
               .HasMaxLength(150);

        builder.HasIndex(u => u.Email)
               .IsUnique()
               .HasFilter("[Email] IS NOT NULL");

        builder.Property(u => u.IsMobileVerified)
               .IsRequired()
               .HasDefaultValue(false);

        builder.Property(u => u.IsEmailVerified)
               .IsRequired()
               .HasDefaultValue(false);

        builder.Property(u => u.Password)
               .HasMaxLength(256);
    }
}
