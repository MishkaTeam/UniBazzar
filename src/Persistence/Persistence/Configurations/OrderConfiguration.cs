using Domain.Aggregates.Ordering.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BuildingBlocks.Persistence.Configurations;

namespace Persistence.Configurations;

internal class OrderConfiguration : BaseConfiguration<Order>
{
    public override void Configure(EntityTypeBuilder<Order> builder)
    {
        base.Configure(builder);

        builder.ToTable("Orders");

        builder.Property(s => s.ReferenceNumber)
               .HasMaxLength(150)
               .IsRequired();

        builder.Property(s => s.BasketReferenceNumber)
               .HasMaxLength(150)
               .IsRequired();

        builder.OwnsMany(x => x.OrderItems, itemBuilder =>
        {
            itemBuilder.ToTable("OrderItems");

            itemBuilder.HasKey(t => t.Id);

            itemBuilder.Property(s => s.OrderReferenceNumber)
                   .HasMaxLength(150)
                   .IsRequired();

            builder.OwnsOne(x => x.TotalDiscountAmount, dBuilder =>
            {
                dBuilder.Property(x => x.DiscountType).HasColumnName("TotalDiscountType");
                dBuilder.Property(x => x.Value).HasColumnName("TotalDiscountAmount");

            });

            itemBuilder.Property(x => x.Ordering)
                   .HasDefaultValue(10_000);

            itemBuilder.Property(s => s.OwnerId)
                   .IsRequired();

            itemBuilder.Property(s => s.InsertedBy)
                   .IsRequired();

            itemBuilder.Property(s => s.UpdatedBy)
                   .IsRequired();

            itemBuilder.Property(s => s.InsertDateTime)
                   .IsRequired();

            itemBuilder.Property(s => s.UpdateDateTime)
                   .IsRequired();

            itemBuilder.OwnsOne(x => x.DiscountAmount, dBuilder =>
            {
                dBuilder.Property(x => x.DiscountType).HasColumnName("DiscountType");
                dBuilder.Property(x => x.Value).HasColumnName("DiscountAmount");
            });

            itemBuilder.OwnsOne(x => x.ProductAmount, pBuilder =>
            {
                pBuilder.Property(x => x.Quantity).HasColumnName("ProductQuantity");
                pBuilder.Property(x => x.BasePrice).HasColumnName("ProductBasePrice");
                pBuilder.Ignore(x => x.TotalPrice);
            });

            itemBuilder.OwnsOne(x => x.Product, pBuilder =>
            {
                pBuilder.Property(x => x.ProductId).HasColumnName("ProductId");
                pBuilder.Property(x => x.ProductName).HasMaxLength(500).HasColumnName("ProductName");
            });

            itemBuilder.OwnsMany(x => x.OrderItemAttribute, attBuilder =>
            {
                attBuilder.ToTable("OrderItemAttributes");
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
