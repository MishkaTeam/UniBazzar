using Domain.Aggregates.SiteSettings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Constants;

namespace Persistence.Configurations;

public class SiteSettingConfiguration : IEntityTypeConfiguration<SiteSetting>
{
    public void Configure(EntityTypeBuilder<SiteSetting> builder)
    {
        builder.ToTable("SiteSettings");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(MaxLength.Name)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(x => x.PhoneNumber)
            .HasMaxLength(MaxLength.CellPhoneNumber)
            .IsRequired();

        builder.Property(x => x.LogoURL)
            .HasMaxLength(500);

        builder.Property(x => x.Address)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.PriceListID)
            .IsRequired(false);

        // Index for faster queries
        builder.HasIndex(x => x.Name);
    }
}
