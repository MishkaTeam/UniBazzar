using Domain.Aggregates.Products;
using Domain.Aggregates.Products.ProductImages;
using Domain.Aggregates.Products.ProductPriceLists;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Aggregates.Products;

public class ProductRepository(UniBazzarContext uniBazzarContext) : IProductRepository
{
    public void AddProductImage(ProductImage productImage)
    {
        uniBazzarContext.Add(productImage);
    }

    public Task<List<ProductImage>> GetAllProductImagesAsync()
    {
        return uniBazzarContext.ProductImages.ToListAsync();
    }

    public Task<List<ProductImage>> GetImageByProductIdAsync(Guid id)
    {
        return uniBazzarContext.ProductImages.Where(x => x.ProductId == id).ToListAsync();
    }

    public async Task<ProductImage> GetProductImageAsync(Guid id)
    {
        var productimage = await uniBazzarContext.ProductImages.FirstOrDefaultAsync(x => x.Id == id);
        return productimage ?? new ProductImage();
    }

    public void Remove(ProductImage productImage)
    {
        uniBazzarContext.ProductImages.Remove(productImage);
    }

    /*--------------------------------------------------------------------------------------------------------*/

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
