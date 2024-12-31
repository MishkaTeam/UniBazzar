using Domain.Aggregates.Products.ProductImages;
using Domain.Aggregates.Products.ProductPriceLists;
using Domain.Aggregates.Products;
using Domain.Aggregates.Products.ProductFeatures;
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
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<ProductPriceList> ProductPriceLists { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductFeature> ProductFeatures { get; set; }

}