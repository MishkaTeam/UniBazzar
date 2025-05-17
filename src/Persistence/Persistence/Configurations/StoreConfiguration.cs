using Domain.Aggregates.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal class StoreConfiguration : IEntityTypeConfiguration<Store>
{
    public void Configure(EntityTypeBuilder<Store> builder)
    {
        builder.ToTable("Stores");
        builder.HasKey(x => x.Id);

        builder.Property(s => s.Name)
                       .IsRequired()
                       .HasMaxLength(100);

        builder.Property(s => s.Description)
               .HasMaxLength(500);

        builder.Property(s => s.PhoneNumber)
               .IsRequired()
               .HasMaxLength(20);

        builder.Property(s => s.HostUrl)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(s => s.Culture)
               .HasMaxLength(10);

        builder.Property(s => s.LogoUrl)
               .HasMaxLength(300);

        builder.Property(s => s.IsActive)
               .IsRequired();

        builder.Property(s => s.Version)
               .IsRequired()
               .HasDefaultValue(1);

        builder.Property(x => x.Ordering)
                .HasDefaultValue(10_000);

        builder.Property(s => s.OwnerId)
               .IsRequired();

        builder.Property(s => s.InsertedBy)
               .IsRequired();

        builder.Property(s => s.UpdatedBy)
               .IsRequired();

        builder.Property(s => s.InsertDateTime)
               .IsRequired();

        builder.Property(s => s.UpdateDateTime)
               .IsRequired();

    }
}
