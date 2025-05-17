using Domain.Aggregates.Units;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal class UnitConfiguration : BaseConfiguration<Unit>
{
    public override void Configure(EntityTypeBuilder<Unit> builder)
    {
        base.Configure(builder);

        builder.ToTable("Units");

        builder.Property(x => x.Title)
            .HasMaxLength(150)
            .IsRequired();

        builder.HasOne(c => c.BaseUnit)
           .WithMany() 
           .HasForeignKey(c => c.BaseUnitId)
           .OnDelete(DeleteBehavior.Restrict);

    }
}
