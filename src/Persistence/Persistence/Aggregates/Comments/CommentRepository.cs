using BuildingBlocks.Persistence;
using BuildingBlocks.Persistence.Extensions;
using Domain.Aggregates.ProductReviews;
using Domain.Aggregates.Products;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Aggregates.Comments;

public class CommentRepository
    (UniBazzarContext uniBazzarContext, IExecutionContextAccessor executionContextAccessor)
    : RepositoryBase<ProductReview>(uniBazzarContext, executionContextAccessor), IProductReviewRepository
{
    public async Task<List<ProductReview>> GetProductReviewsByProductIdAsync(Guid productId)
    {
        return await uniBazzarContext.ProductReviews
            .StoreFilter(executionContextAccessor.StoreId)
            .AsNoTracking()
            .Include(x => x.Customer)
            .Include(x => x.Product)
            .Where(x => x.ProductId == productId)
            .ToListAsync();
    }

    public async Task<List<ProductReview>> GetProductReviewsByProductSkuAsync(string sku)
    {
        return await uniBazzarContext.ProductReviews
            .StoreFilter(executionContextAccessor.StoreId)
            .AsNoTracking()
            .Include(x => x.Customer)
            .Include(x => x.Product)
            .Where(x => x.Product.SKU == sku)
            .ToListAsync();
    }
}
