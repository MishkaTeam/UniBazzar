namespace Domain.Aggregates.Products.ProductFeatures;

public interface IProductFeatureRepository
{
	Task AddProductFeature(ProductFeature entity);
	Task<List<ProductFeature>> GetAllProductFeaturesAsync(Guid productId);
	Task<ProductFeature?> GetProductFeatureAsync(Guid id);
	void RemoveProductFeature(ProductFeature entity);
}