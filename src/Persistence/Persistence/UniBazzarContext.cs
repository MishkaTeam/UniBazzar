using Domain.Aggregates.Customers;
using Domain.Aggregates.ShippingAddress;
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

    public DbSet<Customer> customers { get; set; }

    public DbSet<ShippingAddress> shippingAddresses { get; set; }
}
