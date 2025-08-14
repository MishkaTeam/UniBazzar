
using BuildingBlocks.Domain.Data;

namespace Domain.Aggregates.ProductReviews;

public interface IProductReviewRepository : IRepositoryBase<ProductReview>
{
    Task<List<ProductReview>> GetProductReviewsByProductIdAsync(Guid productId);

    Task<List<ProductReview>> GetProductReviewsByProductSkuAsync(string sku);
}
