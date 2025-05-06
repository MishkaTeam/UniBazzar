using BuildingBlocks.Persistence;
using Domain.Aggregates.Products.ProductPriceLists;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Aggregates.Products;

public class ProductPriceListsRepository : RepositoryBase<ProductPriceList> ,IProductPriceListRepository
{

    private readonly UniBazzarContext _context;

    public ProductPriceListsRepository(UniBazzarContext context, IExecutionContextAccessor execution) : base(context, execution)
    {
        _context = context;
    }

    public async Task<ProductPriceList> GetPriceByProductId(Guid productid)
    {
        var productpricelist = await _context.ProductPriceLists.FirstOrDefaultAsync(x => x.ProductId == productid);
        return productpricelist ?? new ProductPriceList();
    }

    public async Task<List<ProductPriceList>> GetPriceListByProductId(Guid productid)
    {
        return await _context.ProductPriceLists.Where(x => x.ProductId == productid).ToListAsync();
    }
}
