using Domain.Aggregates.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal class CategoryConfiguration : BaseConfiguration<Category>
{
    public override void Configure(EntityTypeBuilder<Category> builder)
    {
        base.Configure(builder);

        builder.ToTable("Categories");


        builder.Property(c => c.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(c => c.IconClass)
               .IsRequired()
               .HasMaxLength(100);

        builder.HasOne(c => c.Parent)
               .WithMany() 
               .HasForeignKey(c => c.ParentId)
               .OnDelete(DeleteBehavior.Restrict);

    }
}
