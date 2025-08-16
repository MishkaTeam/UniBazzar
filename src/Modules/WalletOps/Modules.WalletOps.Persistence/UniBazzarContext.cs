using BuildingBlocks.Persistence;
using BuildingBlocks.Persistence.EFCore;
using Microsoft.EntityFrameworkCore;
using Modules.WalletOps.Domain.Aggregates.WalletTrx;
using Attribute = Domain.Aggregates.Attributes.Attribute;

namespace Persistence;

public class WalletOpsContext(DbContextOptions<WalletOpsContext> options, 
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

        optionsBuilder.UseLazyLoadingProxies
            (options => options.IgnoreNonVirtualNavigations(true));
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderConfiguration).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Wallet> Wallets { get; set; }

    public DbSet<Transaction>  Transactions { get; set; }
    public DbSet<HeldFund> HeldFunds { get; set; }
}