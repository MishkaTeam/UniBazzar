using BuildingBlocks.Persistence;
using Domain.Aggregates.Comments;
using Microsoft.EntityFrameworkCore;
using Persistence.Extensions;

namespace Persistence.Aggregates.Comments;

public class CommentRepository
    (UniBazzarContext uniBazzarContext, IExecutionContextAccessor executionContextAccessor)
    : RepositoryBase<Comment>(uniBazzarContext, executionContextAccessor), ICommentRepository
{
    public async Task<List<Comment>> GetCommentsByProductIdAsync(Guid productId)
    {
        return await uniBazzarContext.Comments
            .StoreFilter(executionContextAccessor.StoreId)
            .AsNoTracking()
            .Include(x => x.Customer)
            .Include(x => x.Product)
            .Where(x => x.ProductId == productId)
            .ToListAsync();
    }
}
