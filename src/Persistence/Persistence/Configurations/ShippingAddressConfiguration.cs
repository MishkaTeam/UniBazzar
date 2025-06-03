using Domain.Aggregates.Customers.ShippingAddresses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BuildingBlocks.Persistence.Configurations;

namespace Persistence.Configurations;

internal class ShippingAddressConfiguration : BaseConfiguration<ShippingAddress>
{
    public override void Configure(EntityTypeBuilder<ShippingAddress> builder)
    {
        base.Configure(builder);

        builder.Property(a => a.Country)
       .IsRequired()
       .HasMaxLength(100);

        builder.Property(a => a.Province)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(a => a.City)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(a => a.Address)
               .IsRequired()
               .HasMaxLength(500);

        builder.Property(a => a.PostalCode)
               .IsRequired()
               .HasMaxLength(20);

        builder.Property(a => a.CustomerId)
               .IsRequired();

        builder.HasOne(a => a.Customers)
               .WithMany(c => c.ShippingAddresses) 
               .HasForeignKey(a => a.CustomerId)
               .OnDelete(DeleteBehavior.Cascade); 

    }
}
