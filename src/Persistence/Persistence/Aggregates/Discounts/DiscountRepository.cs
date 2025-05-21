using BuildingBlocks.Persistence;
using Domain.Aggregates.Discounts;

namespace Persistence.Aggregates.Discounts;

public class DiscountRepository : RepositoryBase<Discount>, IDiscountRepository
{
    public DiscountRepository(UniBazzarContext context, IExecutionContextAccessor execution) : base(context, execution)
    { 
    }
}
