using Domain.Aggregates.Products.ProductImages;

namespace Domain.Aggregates.Products;

public interface IProductRepository
{
    void AddProductImage(ProductImage productImage);
    Task<ProductImage> GetProductImageAsync(Guid id);
    Task<List<ProductImage>> GetAllProductImagesAsync();
    Task<List<ProductImage>> GetImageByProductIdAsync(Guid id);
    void Remove(ProductImage productImage);
}
