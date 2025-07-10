using BuildingBlocks.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BuildingBlocks.Persistence.EFCore;

public class StoreIdSaveChangesInterceptor : SaveChangesInterceptor
{
    private readonly IExecutionContextAccessor _executionContext;

    public StoreIdSaveChangesInterceptor(IExecutionContextAccessor executionContext)
    {
        _executionContext = executionContext ?? throw new ArgumentNullException(nameof(executionContext));
    }

    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        ApplyStoreId(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        ApplyStoreId(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void ApplyStoreId(DbContext? context)
    {
        if (context == null) return;

        var entries = context.ChangeTracker
            .Entries<IEntityHasStore>()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added || entry.Property(nameof(IEntityHasStore.StoreId)).CurrentValue is Guid id && id == Guid.Empty)
            {
                entry.Property(nameof(IEntityHasStore.StoreId)).CurrentValue = _executionContext.StoreId;
            }
        }
    }

}
