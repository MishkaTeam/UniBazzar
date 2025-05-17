using Domain.Aggregates.Products.ProductFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal class ProductFeatureConfiguration : BaseConfiguration<ProductFeature>
{
    public override void Configure(EntityTypeBuilder<ProductFeature> builder)
    {
        base.Configure(builder);
        builder.ToTable("ProductFeatures");

        builder.Property(p => p.Key)
       .IsRequired()
       .HasMaxLength(150);

        builder.Property(p => p.Value)
       .IsRequired()
       .HasMaxLength(150);
    }
}
