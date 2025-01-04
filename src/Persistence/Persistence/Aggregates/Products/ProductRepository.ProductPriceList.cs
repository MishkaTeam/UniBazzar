using Domain.Aggregates.Products.ProductPriceLists;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Aggregates.Products;

public partial class ProductRepository
{
    public void AddProductPriceList(ProductPriceList productPriceList)
    {
        uniBazzarContext.Add(productPriceList);
    }

    public async Task<ProductPriceList> GetProductPriceListAsync(Guid id)
    {

        var productpricelist = await uniBazzarContext.ProductPriceLists.FirstOrDefaultAsync(x => x.Id == id);
        return productpricelist ?? new ProductPriceList();
    }

    public Task<List<ProductPriceList>> GetAllProductPriceListAsync()
    {

        return uniBazzarContext.ProductPriceLists.ToListAsync();
    }

    public async Task<ProductPriceList> GetPriceByProductId(Guid id)
    {
        var productpricelist = await uniBazzarContext.ProductPriceLists.FirstOrDefaultAsync(x => x.ProductId == id);
        return productpricelist ?? new ProductPriceList();
    }

    public void Remove(ProductPriceList productPriceList)
    {
        uniBazzarContext.ProductPriceLists.Remove(productPriceList);
    }
}
