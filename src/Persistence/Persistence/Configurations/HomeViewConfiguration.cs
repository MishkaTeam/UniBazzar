using BuildingBlocks.Persistence.Configurations;
using Domain.Aggregates.Cms.HomeViews;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal class HomeViewConfiguration : BaseConfiguration<HomeView>
{
    public override void Configure(EntityTypeBuilder<HomeView> builder)
    {
        base.Configure(builder);

        builder.ToTable("HomeViews");

        builder.Property(x => x.Title)
               .HasMaxLength(150)
               .IsRequired();

        builder.Property(x => x.Type)
                .IsRequired()
                .HasConversion<int>();

        builder.Property(x => x.Sorting)
                .IsRequired();

        builder.Property(x => x.IsActive)
                .IsRequired();

        builder.Property(x => x.DeactivateDate)
               .IsRequired();

        builder.OwnsMany(x => x.SliderViews, itemBuilder =>
        {
            itemBuilder.ToTable("SliderViewItems");

            itemBuilder.HasKey(x => x.Id);

            itemBuilder.Property(x => x.Ordering)
                    .HasDefaultValue(10_000);

            itemBuilder.Property(x => x.OwnerId)
                   .IsRequired();

            itemBuilder.Property(x => x.InsertedBy)
                   .IsRequired();

            itemBuilder.Property(x => x.UpdatedBy)
                   .IsRequired();

            itemBuilder.Property(x => x.InsertDateTime)
                   .IsRequired();

            itemBuilder.Property(x => x.UpdateDateTime)
                   .IsRequired();

            itemBuilder.Property(x => x.Title)
                   .HasMaxLength(150)
                   .IsRequired();

            itemBuilder.Property(x => x.ImageUrl)
                   .HasMaxLength(500)
                   .IsRequired();

            itemBuilder.Property(x => x.NavigationUrl)
                   .HasMaxLength(500);

            itemBuilder.Property(x => x.Interval)
                       .IsRequired();
        });

        builder.OwnsMany(x => x.ProductViews, itemBuilder =>
        {
            itemBuilder.ToTable("ProductViewItems");

            itemBuilder.HasKey(x => x.Id);

            itemBuilder.Property(x => x.Ordering)
                    .HasDefaultValue(10_000);

            itemBuilder.Property(x => x.OwnerId)
                   .IsRequired();

            itemBuilder.Property(x => x.InsertedBy)
                   .IsRequired();

            itemBuilder.Property(x => x.UpdatedBy)
                   .IsRequired();

            itemBuilder.Property(x => x.InsertDateTime)
                   .IsRequired();

            itemBuilder.Property(x => x.UpdateDateTime)
                   .IsRequired();

            itemBuilder.HasOne(x => x.Product)
                   .WithMany()
                   .HasForeignKey(x => x.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);
        });

        builder.OwnsMany(x => x.ImageViews, itemBuilder =>
        {
            itemBuilder.ToTable("ImageViewItems");

            itemBuilder.HasKey(x => x.Id);

            itemBuilder.Property(x => x.Ordering)
                    .HasDefaultValue(10_000);

            itemBuilder.Property(x => x.OwnerId)
                   .IsRequired();

            itemBuilder.Property(x => x.InsertedBy)
                   .IsRequired();

            itemBuilder.Property(x => x.UpdatedBy)
                   .IsRequired();

            itemBuilder.Property(x => x.InsertDateTime)
                   .IsRequired();

            itemBuilder.Property(x => x.UpdateDateTime)
                   .IsRequired();

            itemBuilder.Property(x => x.Title)
                   .HasMaxLength(150)
                   .IsRequired();

            itemBuilder.Property(x => x.ImageUrl)
                   .HasMaxLength(500)
                   .IsRequired();

            itemBuilder.Property(x => x.NavigationUrl)
                   .HasMaxLength(500);

            itemBuilder.Property(x => x.Column)
                   .HasMaxLength(10);
        });
    }
}
