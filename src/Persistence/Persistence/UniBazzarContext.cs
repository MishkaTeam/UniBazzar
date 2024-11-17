using Domain.Aggregates.Units;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class UniBazzarContext : DbContext
{
    public UniBazzarContext(DbContextOptions options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Unit> Units { get; set; }


}
