using Domain.Aggregates.Products.ProductFeatures;

namespace Domain.Aggregates.Products;

public interface IProductRepository : IProductFeatureRepository
{
	void AddProduct(Product entity);
	Task<List<Product>> GetAllProductsAsync();
	Task<Product> GetProductAsync(Guid id);
	void RemoveProduct(Product entity);
}