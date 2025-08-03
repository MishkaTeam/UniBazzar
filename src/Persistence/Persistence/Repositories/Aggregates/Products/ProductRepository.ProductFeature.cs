using BuildingBlocks.Domain.Context;
using BuildingBlocks.Persistence;
using Domain.Aggregates.Products.ProductFeatures;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories.Aggregates.Products;

public class ProductFeaturesRepository : RepositoryBase<ProductFeature> ,IProductFeatureRepository
{
    private readonly UniBazzarContext _context;

    public ProductFeaturesRepository(UniBazzarContext context, IExecutionContextAccessor execution) : base(context, execution)
    {
        _context = context;
    }

    public async Task<List<ProductFeature>> GetAllProductFeaturesAsync(Guid productId)
    {
        return await _context.ProductFeatures
                               .Include(x => x.Product)
                               .Where(x => x.ProductId == productId)
                               .ToListAsync();
    }

    public async Task<ProductFeature?> GetProductFeatureAsync(Guid id)
    {
        var productFeature = await _context.ProductFeatures
                                .Include(x => x.Product)
                                .FirstOrDefaultAsync(x => x.Id == id);

        return productFeature;
    }

}