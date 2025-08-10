using BuildingBlocks.Persistence;
using BuildingBlocks.Persistence.EFCore;
using Domain.Aggregates.branches;
using Domain.Aggregates.Categories;
using Domain.Aggregates.Customers;
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
using Domain.Aggregates.CheckoutCounters;
using Domain.Aggregates.ProductReviews;
using Domain.Aggregates.Customers.ShippingAddresses;
using Domain.Aggregates.Discounts.DsiscounProducts;
using Domain.Aggregates.Discounts.DiscountCustomers;
using Domain.Aggregates.SiteSettings;

namespace Persistence;

public class UniBazzarContext(DbContextOptions options, 
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

        optionsBuilder.UseLazyLoadingProxies
            (options => options.IgnoreNonVirtualNavigations(true));
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderConfiguration).Assembly);
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
    public DbSet<DiscountProduct> DiscountProducts { get; set; }
    public DbSet<DiscountCustomer> DiscountCustomers { get; set; }

    public DbSet<ProductReview> ProductReviews { get; set; }
    
    public DbSet<SiteSetting> SiteSettings { get; set; }
}