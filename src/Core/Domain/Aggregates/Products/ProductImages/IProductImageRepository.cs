namespace Domain.Aggregates.Products.ProductImages;

public interface IProductImageRepository
{
    void AddProductImage(ProductImage productImage);
    Task<ProductImage> GetProductImageAsync(Guid id);
    Task<List<ProductImage>> GetAllProductImagesAsync();
    Task<List<ProductImage>> GetImageByProductIdAsync(Guid id);
    void Remove(ProductImage productImage);
}
