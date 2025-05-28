using Domain.Aggregates.PriceLists;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    internal class PriceListConfiguration : BaseConfiguration<PriceList>
    {

        public override void Configure(EntityTypeBuilder<PriceList> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Title)
                   .IsRequired();

            builder.OwnsMany(x => x.Items, itemBuilder =>
            {
                   itemBuilder
                        .Property(x => x.CurrencyCode)
                        .HasMaxLength(4);
            });
        }
    }
}
