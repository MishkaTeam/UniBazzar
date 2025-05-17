using Domain.Aggregates.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal class ProductConfiguration : BaseConfiguration<Product>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        base.Configure(builder);

        builder.ToTable("Products");

        builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(150);

        builder.Property(p => p.ShortDescription)
               .IsRequired()
               .HasMaxLength(300);

        builder.Property(p => p.FullDescription)
               .IsRequired();

        builder.Property(p => p.SKU)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(p => p.DownloadUrl)
               .HasMaxLength(500);

        builder.Property(p => p.ProductType)
               .IsRequired()
               .HasConversion<int>();
    }
}
