using BuildingBlocks.Domain.Context;
using BuildingBlocks.Persistence;
using Domain.Aggregates.Products.ProductImages;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories.Aggregates.Products;

public class ProductImagesRepository : RepositoryBase<ProductImage> ,IProductImageRepository
{

    private readonly UniBazzarContext _context;

    public ProductImagesRepository(UniBazzarContext context, IExecutionContextAccessor execution) : base(context, execution)
    {
        _context = context;
    }

    public async Task<List<ProductImage>> GetImageByProductIdAsync(Guid productid)
    {
        return await _context.ProductImages.Where(x => x.ProductId == productid).ToListAsync();
    }
}
