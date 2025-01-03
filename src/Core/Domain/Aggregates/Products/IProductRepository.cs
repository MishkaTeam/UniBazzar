using Domain.Aggregates.Products.ProductImages;
using Domain.Aggregates.Products.ProductPriceLists;
using Domain.Aggregates.Products.ProductFeatures;

namespace Domain.Aggregates.Products;

public interface IProductRepository : IProductFeatureRepository,
                                      IProductImageRepository
{
    void AddProductPriceList(ProductPriceList productPriceList);
    Task<ProductPriceList> GetProductPriceListAsync(Guid id);
    Task<List<ProductPriceList>> GetAllProductPriceListAsync();
    Task<ProductPriceList> GetPriceByProductId(Guid id);
    void Remove(ProductPriceList priceList);
  
  	void AddProduct(Product entity);
	  Task<List<Product>> GetAllProductsAsync();
	  Task<Product> GetProductAsync(Guid id);
	  void RemoveProduct(Product entity);
}

