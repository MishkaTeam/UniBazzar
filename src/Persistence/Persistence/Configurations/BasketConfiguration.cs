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

        builder.OwnsOne(x => x.TotalDiscountAmount, dBuilder =>
        {
            dBuilder.Property(x => x.DiscountType).HasColumnName("TotalDiscountType");
            dBuilder.Property(x => x.Value).HasColumnName("TotalDiscountAmount");

        });
        builder.Ignore(x => x.TotalBeforeDiscount);

        builder.Ignore(x => x.Total);

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

            basketBuilder.Ignore(x => x.TotalPrice);
            basketBuilder.OwnsOne(x => x.DiscountAmount, dBuilder =>
            {
                dBuilder.Property(x => x.DiscountType).HasColumnName("DiscountType");
                dBuilder.Property(x => x.Value).HasColumnName("DiscountAmount");
            });


            basketBuilder.OwnsOne(x => x.ProductAmount, pBuilder =>
            {
                pBuilder.Property(x => x.Quantity).HasColumnName("ProductQuantity");
                pBuilder.Property(x => x.BasePrice).HasColumnName("ProductBasePrice");
                //pBuilder.Property(x => x.TotalPrice).HasColumnName("ProductTotalPrice").HasComputedColumnSql("ProductQuantity * ProductBasePrice");
                pBuilder.Ignore(x => x.TotalPrice);
            });


            basketBuilder.OwnsOne(x => x.Product, pBuilder =>
            {
                pBuilder.Property(x => x.ProductId).HasColumnName("ProductId");
                pBuilder.Property(x => x.ProductName).HasMaxLength(500).HasColumnName("ProductName");
            });

            basketBuilder.OwnsMany(x => x.BasketItemAttributes, attBuilder =>
            {
                attBuilder.ToTable("BasketItemAttributes");
                attBuilder.HasKey(t => t.Id);

                attBuilder.Property(x => x.Ordering)
                        .HasDefaultValue(10_000);

                attBuilder.Property(s => s.OwnerId)
                       .IsRequired();

                attBuilder.Property(s => s.InsertedBy)
                       .IsRequired();

                attBuilder.Property(s => s.UpdatedBy)
                       .IsRequired();

                attBuilder.Property(s => s.InsertDateTime)
                       .IsRequired();

                attBuilder.Property(s => s.UpdateDateTime)
                       .IsRequired();

                attBuilder.Property(x => x.ProductAttributeName).HasMaxLength(2000);
                attBuilder.Property(x => x.ProductAttributeValue).HasMaxLength(2000);
            });

        });
    }   
}