using BuildingBlocks.Persistence;
using Domain.Aggregates.Ordering.Orders;
using Domain.Aggregates.Ordering.Orders.Data;

namespace Persistence.Aggregates.Ordering;

public class OrderRepository : RepositoryBase<Order>, IOrderRepository
{
    public OrderRepository(UniBazzarContext context, IExecutionContextAccessor executionContext) : base(context, executionContext)
    {
    }
}