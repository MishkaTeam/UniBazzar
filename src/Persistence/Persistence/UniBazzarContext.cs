using Domain.Aggregates.branches;
using Domain.Aggregates.Categories;
using Domain.Aggregates.CheckoutCounter;
using Domain.Aggregates.Customers;
using Domain.Aggregates.Customers.ShippingAddresses;
using Domain.Aggregates.Discounts;
using Domain.Aggregates.Ordering.Baskets;
using Domain.Aggregates.Ordering.Orders;
using Domain.Aggregates.PriceLists;
using Domain.Aggregates.Products;
using Domain.Aggregates.Products.ProductFeatures;
using Domain.Aggregates.Products.ProductImages;
using Domain.Aggregates.Stores;
using Domain.Aggregates.Units;
using Domain.Aggregates.Users;
using Microsoft.EntityFrameworkCore;
using Persistence.Configurations;

namespace Persistence;

public class UniBazzarContext : DbContext
{
    public UniBazzarContext
        (DbContextOptions options) : base(options)
    {
    }

    protected override void OnConfiguring
        (DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies
            (options => options.IgnoreNonVirtualNavigations(true));
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BaseConfiguration<>).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Store> Stores { get; set; }
    public DbSet<Branch> Branches { get; set; }
	public DbSet<CheckoutCounter> CheckoutCounters { get; set; }

	public DbSet<Customer> Customers { get; set; }
	public DbSet<ShippingAddress> ShippingAddresses { get; set; }

    public DbSet<Basket> Baskets { get; set; }
    public DbSet<Order> Orders  { get; set; }

    public DbSet<Product> Products { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<ProductFeature> ProductFeatures { get; set; }
    public DbSet<PriceList> ProductPriceLists { get; set; }

    public DbSet<Unit> Units { get; set; }

	public DbSet<Category> Categories { get; set; }
    public DbSet<Discount> Discounts { get; set; }
	public DbSet<User> Users { get; set; }
	public DbSet<Customer> Customers { get; set; }
	public DbSet<ShippingAddress> ShippingAddresses { get; set; }
	public DbSet<CheckoutCounter> CheckoutCounters { get; set; }

}