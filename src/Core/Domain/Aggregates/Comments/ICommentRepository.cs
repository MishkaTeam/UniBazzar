
using BuildingBlocks.Domain.Data;

namespace Domain.Aggregates.Comments;

public interface ICommentRepository :IRepositoryBase<Comment>
{
    Task<List<Comment>> GetCommentsByProductIdAsync(Guid productId);
    
}
