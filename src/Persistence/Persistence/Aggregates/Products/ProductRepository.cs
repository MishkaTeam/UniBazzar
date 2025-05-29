using BuildingBlocks.Persistence;
using Domain.Aggregates.Products;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Aggregates.Products;

public partial class ProductRepository : RepositoryBase<Product>, IProductRepository
{
    public ProductRepository(UniBazzarContext context, IExecutionContextAccessor execution) : base(context, execution)
    {
    }

    public Task<List<Product>> GetFullProductData(CancellationToken cancellationToken = default)
    {
        var query = DbSet.Include(x => x.ProductImages)
            .Include(x => x.ProductFeatures)
            .Take(4);

        return query.ToListAsync();
           
    }

    public Task<Product> GetFullProductData(string sku)
    {
        return DbSet.Include(x => x.ProductImages)
            .Include(x => x.ProductFeatures)
            .FirstOrDefaultAsync(x => x.SKU == sku);

    }
}