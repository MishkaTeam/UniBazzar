using BuildingBlocks.Persistence;
using BuildingBlocks.Persistence.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Modules.Treasury.Persistence;

public class TreasuryDbContext(DbContextOptions<TreasuryDbContext> options,
AuditSaveChangesInterceptor auditInterceptor,
StoreIdSaveChangesInterceptor storeIdSaveChangesInterceptor,
OwnerIdSaveChangesInterceptor ownerIdSaveChangesInterceptor,
StoreQueryInterceptor storeQueryInterceptor) : BaseDbContext(options)
{
    protected override void OnConfiguring
    (DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(auditInterceptor);
        optionsBuilder.AddInterceptors(storeIdSaveChangesInterceptor);
        optionsBuilder.AddInterceptors(ownerIdSaveChangesInterceptor);
        optionsBuilder.AddInterceptors(storeQueryInterceptor);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("TRS");
        base.OnModelCreating(modelBuilder);
    }
}
