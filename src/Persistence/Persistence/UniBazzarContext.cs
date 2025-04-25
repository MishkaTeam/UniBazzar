using Domain.Aggregates.Categories;
using Domain.Aggregates.Customers;
using Domain.Aggregates.Products;
using Domain.Aggregates.Products.ProductFeatures;
using Domain.Aggregates.Products.ProductImages;
using Domain.Aggregates.Products.ProductPriceLists;
using Domain.Aggregates.ShippingAddress;
using Domain.Aggregates.Stores;
using Domain.Aggregates.Units;
using Domain.Aggregates.Users;
using Microsoft.EntityFrameworkCore;

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


	public DbSet<Store> Stores { get; set; }

	public DbSet<Product> Products { get; set; }
	public DbSet<ProductImage> ProductImages { get; set; }
	public DbSet<ProductFeature> ProductFeatures { get; set; }
	public DbSet<ProductPriceList> ProductPriceLists { get; set; }

	public DbSet<Unit> Units { get; set; }

	public DbSet<Category> Categories { get; set; }
	public DbSet<User> Users { get; set; }
	public DbSet<Customer> Customers { get; set; }
	public DbSet<ShippingAddress> ShippingAddresses { get; set; }

}