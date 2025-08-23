using BuildingBlocks.Persistence;
using BuildingBlocks.Persistence.EFCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Modules.Inventory.Persistence;

public class InventoryDbContext(DbContextOptions<InventoryDbContext> options,
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
        modelBuilder.HasDefaultSchema("INV");
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(InventoryDbContext).Assembly);
    }

}
