using Persistence.Auditing.Enums;

namespace Persistence.Auditing;

public class AuditLog
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    public Guid? TenantId { get; set; }
    public Guid? UserId { get; set; }

    public string TableName { get; set; }
    public ActionType Action { get; set; }

    public string PrimaryKey { get; set; }
    public string? OldValues { get; set; }
    public string? NewValues { get; set; }
    public string? ChangedColumns { get; set; }

    public string? TraceId { get; set; }

}
