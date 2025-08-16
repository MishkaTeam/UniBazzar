using BuildingBlocks.Domain.Context;
using BuildingBlocks.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BuildingBlocks.Persistence.EFCore;

public class OwnerIdSaveChangesInterceptor : SaveChangesInterceptor
{
    private readonly IExecutionContextAccessor _executionContext;

    public OwnerIdSaveChangesInterceptor(IExecutionContextAccessor executionContext)
    {
        _executionContext = executionContext ?? throw new ArgumentNullException(nameof(executionContext));
    }

    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        ApplyOwnerId(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        ApplyOwnerId(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void ApplyOwnerId(DbContext? context)
    {
        if (context == null) return;

        var entries = context.ChangeTracker
            .Entries<IEntityHasOwner>()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added || entry.Property(nameof(IEntityHasOwner.OwnerId)).CurrentValue is Guid id && id == Guid.Empty)
            {
                entry.Property(nameof(IEntityHasOwner.OwnerId)).CurrentValue = _executionContext.UserId ?? Guid.Empty;
            }
        }
    }

}
