using Domain.Aggregates.branches;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal class BranchConfiguration : BaseConfiguration<Branch>
{
    public override void Configure(EntityTypeBuilder<Branch> builder)
    {
        base.Configure(builder);

        builder.ToTable("Branches");

        builder.Property(s => s.Name)
               .IsRequired()
               .HasMaxLength(100);
    }
}
