using BuildingBlocks.Persistence.Auditing;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Persistence;

public class BaseDbContext : DbContext
{
    public BaseDbContext(DbContextOptions options) : base(options)
    {
    }   
    
    public DbSet<AuditLog> AuditLogs { get; set; }


}
