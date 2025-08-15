using BuildingBlocks.Domain.Context;
using BuildingBlocks.Persistence;
using Domain.Aggregates.Products.ProductFeatures;
using Microsoft.EntityFrameworkCore;
using Modules.Inventory.Persistence;

namespace Persistence.Repositories.Aggregates.Products;

public class ProductFeaturesRepository : RepositoryBase<ProductFeature> ,IProductFeatureRepository
{
    private readonly InventoryDbContext _INVdbcontext;

    public ProductFeaturesRepository(InventoryDbContext INVdbcontext, IExecutionContextAccessor execution) : base(INVdbcontext, execution)
    {
        _INVdbcontext = INVdbcontext;
    }

    public async Task<List<ProductFeature>> GetAllProductFeaturesAsync(Guid productId)
    {
        return await DbSet
                    .Include(x => x.Product)
                    .Where(x => x.ProductId == productId)
                    .ToListAsync();
    }

    public async Task<ProductFeature?> GetProductFeatureAsync(Guid id)
    {
        var productFeature = await DbSet
                                  .Include(x => x.Product)
                                  .FirstOrDefaultAsync(x => x.Id == id);

        return productFeature;
    }

}