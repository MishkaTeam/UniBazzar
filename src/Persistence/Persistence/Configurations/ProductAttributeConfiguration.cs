using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BuildingBlocks.Persistence.Configurations;
using Domain.Aggregates.Products.ProductAttributes;

namespace Persistence.Configurations;

internal class ProductAttributeConfiguration : BaseConfiguration<ProductAttribute>
{
    public override void Configure(EntityTypeBuilder<ProductAttribute> builder)
    {
        base.Configure(builder);
        builder.ToTable("ProductAttributes");
    }
}

