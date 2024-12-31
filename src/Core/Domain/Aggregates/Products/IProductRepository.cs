using Domain.Aggregates.Products.ProductImages;
using Domain.Aggregates.Products.ProductPriceLists;
using Domain.Aggregates.Products.ProductFeatures;

namespace Domain.Aggregates.Products;

public interface IProductRepository : IProductFeatureRepository
{
    void AddProductImage(ProductImage productImage);
    Task<ProductImage> GetProductImageAsync(Guid id);
    Task<List<ProductImage>> GetAllProductImagesAsync();
    Task<List<ProductImage>> GetImageByProductIdAsync(Guid id);
    void Remove(ProductImage productImage);

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

