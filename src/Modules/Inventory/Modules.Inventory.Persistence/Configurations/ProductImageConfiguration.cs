using Domain.Aggregates.Products.ProductImages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BuildingBlocks.Persistence.Configurations;

namespace Persistence.Configurations;

internal class ProductImageConfiguration : BaseConfiguration<ProductImage>
{
    public override void Configure(EntityTypeBuilder<ProductImage> builder)
    {
        base.Configure(builder);

        builder.ToTable("ProductImages");

        builder.Property(x => x.ImageUrl).HasMaxLength(2000).IsRequired();
    }
}
