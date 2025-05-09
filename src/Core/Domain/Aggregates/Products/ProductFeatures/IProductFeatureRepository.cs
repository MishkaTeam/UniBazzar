using Domain.BuildingBlocks.Data;

namespace Domain.Aggregates.Products.ProductFeatures;

public interface IProductFeatureRepository : IRepositoryBase<ProductFeature>
{
    Task<List<ProductFeature>> GetAllProductFeaturesAsync(Guid productId);
    Task<ProductFeature?> GetProductFeatureAsync(Guid id);
}