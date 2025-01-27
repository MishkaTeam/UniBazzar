using Domain.Aggregates.Products.ProductImages;
using Domain.Aggregates.Products.ProductPriceLists;
using Domain.Aggregates.Products.ProductFeatures;

namespace Domain.Aggregates.Products;

public interface IProductRepository : IProductFeatureRepository,
									  IProductPriceListRepository,
									  IProductImageRepository
{
	Task AddProductAsync(Product entity);
	Task<List<Product>> GetAllProductsAsync();
	Task<Product> GetProductAsync(Guid id);
	void RemoveProduct(Product entity);
}