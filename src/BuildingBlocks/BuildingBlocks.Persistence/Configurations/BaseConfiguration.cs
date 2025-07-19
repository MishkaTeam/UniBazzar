using BuildingBlocks.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BuildingBlocks.Persistence.Configurations;

public class BaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class, IEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(x => x.Ordering)
        .HasDefaultValue(10_000);

        builder.Property(s => s.OwnerId)
       .IsRequired();

        builder.Property(s => s.InsertedBy)
               .IsRequired();

        builder.Property(s => s.UpdatedBy)
               .IsRequired();

        builder.Property(s => s.InsertDateTime)
               .IsRequired();

        builder.Property(s => s.UpdateDateTime)
               .IsRequired();

        // StoreId ???
        // Version ???
    }
}
