using Domain.Aggregates.Products.ProductImages;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Aggregates.Products;

public partial class ProductRepository
{
    public async Task AddProductImage(ProductImage productImage)
    {
        await uniBazzarContext.AddAsync(productImage);
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

    public void RemoveImage(ProductImage productImage)
    {
        uniBazzarContext.ProductImages.Remove(productImage);
    }
}
