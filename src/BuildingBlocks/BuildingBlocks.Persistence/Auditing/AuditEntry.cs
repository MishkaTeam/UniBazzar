using System.Text.Json;
using BuildingBlocks.Persistence.Auditing.Enums;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BuildingBlocks.Persistence.Auditing;

internal class AuditEntry
{
    public AuditEntry(EntityEntry entry) => Entry = entry;

    public EntityEntry Entry { get; }
    public string TableName { get; set; } = "";
    public ActionType Action { get; set; }
    public Guid? UserId { get; set; }
    public Guid? TenantId { get; set; }
    public string? TraceId { get; set; }

    public Dictionary<string, object?> OldValues { get; } = new();
    public Dictionary<string, object?> NewValues { get; } = new();
    public List<string> ChangedColumns { get; } = new();

    public AuditLog ToAuditLog(string primaryKey)
    {
        return new AuditLog
        {
            TableName = TableName,
            Action = Action,
            UserId = UserId,
            TenantId = TenantId,
            TraceId = TraceId,
            PrimaryKey = primaryKey,
            OldValues = JsonSerializer.Serialize(OldValues),
            NewValues = JsonSerializer.Serialize(NewValues),
            ChangedColumns = JsonSerializer.Serialize(ChangedColumns),
        };
    }
}
