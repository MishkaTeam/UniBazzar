using System.Diagnostics;
using BuildingBlocks.Persistence.Auditing;
using BuildingBlocks.Persistence.Auditing.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BuildingBlocks.Persistence.EFCore;

public class AuditSaveChangesInterceptor : SaveChangesInterceptor
{
    private readonly IExecutionContextAccessor _context;

    public AuditSaveChangesInterceptor(IExecutionContextAccessor Context)
    {
        _context = Context;
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        var context = eventData.Context;
        if (context == null) return await base.SavingChangesAsync(eventData, result, cancellationToken);

        var auditLogs = new List<AuditLog>();
        foreach (var entry in context.ChangeTracker.Entries())
        {
            if (entry.Entity is AuditLog || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                continue;

            var auditEntry = new AuditEntry(entry)
            {
                TableName = entry.Metadata.GetTableName() ?? "",
                Action = GetAction(entry),
                UserId = _context.UserId,
                TenantId = _context.StoreId,
                TraceId = Activity.Current?.TraceId.ToString()
            };

            foreach (var property in entry.Properties)
            {
                var propName = property.Metadata.Name;
                if (entry.State == EntityState.Added)
                {
                    auditEntry.NewValues[propName] = property.CurrentValue;
                }
                else if (entry.State == EntityState.Deleted)
                {
                    auditEntry.OldValues[propName] = property.OriginalValue;
                }
                else if (entry.State == EntityState.Modified && property.IsModified)
                {
                    auditEntry.ChangedColumns.Add(propName);
                    auditEntry.OldValues[propName] = property.OriginalValue;
                    auditEntry.NewValues[propName] = property.CurrentValue;

                    // SoftDelete detection
                    if (propName == "IsDeleted" && property.CurrentValue?.ToString() == "True")
                        auditEntry.Action = ActionType.SoftDelete;
                }
            }

            var pk = entry.Properties.First(p => p.Metadata.IsPrimaryKey()).CurrentValue?.ToString() ?? "";
            auditLogs.Add(auditEntry.ToAuditLog(pk));
        }

        context.Set<AuditLog>().AddRange(auditLogs);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private ActionType GetAction(EntityEntry entry)
    {
        return entry.State switch
        {
            EntityState.Added => ActionType.Insert,
            EntityState.Deleted => ActionType.Delete,
            EntityState.Modified => ActionType.Update,
            _ => throw new NotImplementedException()
        };
    }
}
