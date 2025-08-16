using BuildingBlocks.Persistence;
using BuildingBlocks.Persistence.EFCore;
using Microsoft.EntityFrameworkCore;
using Modules.WalletOps.Domain.Aggregates.WalletTrx;
using Modules.WalletOps.Persistence.Configurations;

namespace Modules.WalletOps.Persistence;

public class WalletOpsDbContext(DbContextOptions<WalletOpsDbContext> options, 
    AuditSaveChangesInterceptor auditInterceptor,
    StoreIdSaveChangesInterceptor storeIdSaveChangesInterceptor,
    OwnerIdSaveChangesInterceptor ownerIdSaveChangesInterceptor,
    StoreQueryInterceptor storeQueryInterceptor) : BaseDbContext(options)
{

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(auditInterceptor);
        optionsBuilder.AddInterceptors(storeIdSaveChangesInterceptor);
        optionsBuilder.AddInterceptors(ownerIdSaveChangesInterceptor);
        optionsBuilder.AddInterceptors(storeQueryInterceptor);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.HasDefaultSchema("WLT");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WalletConfiguration).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Wallet> Wallets { get; set; }
    public DbSet<Transaction>  Transactions { get; set; }
    public DbSet<HeldFund> HeldFunds { get; set; }
}