using BuildingBlocks.Domain.Context;
using BuildingBlocks.Persistence;
using Domain.Aggregates.Products.ProductImages;
using Microsoft.EntityFrameworkCore;
using Modules.Inventory.Persistence;

namespace Persistence.Repositories.Aggregates.Products;

public class ProductImagesRepository : RepositoryBase<ProductImage> ,IProductImageRepository
{

    private readonly InventoryDbContext _INVdbcontext;

    public ProductImagesRepository(InventoryDbContext INVdbcontext, IExecutionContextAccessor execution) : base(INVdbcontext, execution)
    {
        _INVdbcontext = INVdbcontext;
    }

    public async Task<List<ProductImage>> GetImageByProductIdAsync(Guid productid)
    {
        return await DbSet
                    .Where(x => x.ProductId == productid).ToListAsync();
    }
}
