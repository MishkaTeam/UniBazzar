using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BuildingBlocks.Persistence.Configurations;
using Domain.Aggregates.CheckoutCounters;

namespace Persistence.Configurations;

internal class CheckoutCounterConfiguration : BaseConfiguration<CheckoutCounter>
{
    public override void Configure(EntityTypeBuilder<CheckoutCounter> builder)
    {
        base.Configure(builder);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(150);
    }
}
