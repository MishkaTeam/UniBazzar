using BuildingBlocks.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Attribute = Domain.Aggregates.Attributes.Attribute;

namespace Persistence.Configurations;

internal class AttributeConfiguration : BaseConfiguration<Attribute>
{
    public override void Configure(EntityTypeBuilder<Attribute> builder)
    {
        base.Configure(builder);
        builder.ToTable("Attributes");

        builder.Property(x => x.Name).HasMaxLength(100);
        builder.Property(x => x.Description).HasMaxLength(1000);

        builder.OwnsMany(x => x.AttributeValues, abuilder =>
        {
            abuilder.ToTable("AttributeValues");
            abuilder.HasKey(x => x.Id);
            abuilder.Property(x => x.Name).HasMaxLength(100);
        });

    }
}
