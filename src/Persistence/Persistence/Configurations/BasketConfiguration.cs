using Domain.Aggregates.Ordering.Baskets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BuildingBlocks.Persistence.Configurations;

namespace Persistence.Configurations;

internal class BasketConfiguration : BaseConfiguration<Basket>
{
    public override void Configure(EntityTypeBuilder<Basket> builder)
    {
        base.Configure(builder);

        builder.ToTable("Baskets");

        builder.Property(s => s.ReferenceNumber)
               .HasMaxLength(150)
               .IsRequired();

        builder.OwnsMany(x => x.BasketItems, basketBuilder =>
        {
            basketBuilder.ToTable("BasketItems");

            basketBuilder.HasKey(t => t.Id);

            basketBuilder.Property(s => s.BasketReferenceNumber)
                   .HasMaxLength(150)
                   .IsRequired();

            basketBuilder.Property(x => x.Ordering)
                   .HasDefaultValue(10_000);

            basketBuilder.Property(s => s.OwnerId)
                   .IsRequired();

            basketBuilder.Property(s => s.InsertedBy)
                   .IsRequired();

            basketBuilder.Property(s => s.UpdatedBy)
                   .IsRequired();

            basketBuilder.Property(s => s.InsertDateTime)
                   .IsRequired();

            basketBuilder.Property(s => s.UpdateDateTime)
                   .IsRequired();

            basketBuilder.OwnsOne(x => x.DiscountAmount, dBuilder =>
            {
                dBuilder.Property(x => x.DiscountType).HasColumnName("DiscountType");
                dBuilder.Property(x => x.Value).HasColumnName("DiscountAmount");
            });


            basketBuilder.OwnsOne(x => x.ProductAmount, pBuilder =>
            {
                pBuilder.Property(x => x.Quantity).HasColumnName("ProductQuantity");
                pBuilder.Property(x => x.BasePrice).HasColumnName("ProductBasePrice");
                pBuilder.Property(x => x.TotalPrice).HasColumnName("ProductTotalPrice");
            });


            basketBuilder.OwnsOne(x => x.Product, pBuilder =>
            {
                pBuilder.Property(x => x.ProductId).HasColumnName("ProductId");
                pBuilder.Property(x => x.ProductName).HasMaxLength(500).HasColumnName("ProductName");
            });

        });
    }   
}
