using BuildingBlocks.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Modules.Treasury.Persistence;

public class TreasuryDbContext : BaseDbContext
{
    public TreasuryDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("TRS");
        base.OnModelCreating(modelBuilder);
    }
}
