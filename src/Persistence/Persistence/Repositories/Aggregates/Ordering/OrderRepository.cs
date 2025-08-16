using BuildingBlocks.Domain.Context;
using BuildingBlocks.Persistence;
using BuildingBlocks.Persistence.Extensions;
using Domain.Aggregates.Ordering.Orders;
using Domain.Aggregates.Ordering.Orders.Data;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories.Aggregates.Ordering;

public class OrderRepository(
    UniBazzarContext context,
    IExecutionContextAccessor executionContext) : RepositoryBase<Order>(context, executionContext), IOrderRepository
{

    public async Task<List<OrderItem>> GetAllOrderItem(Guid orderId)
    {
        var order = await context.Orders
                         .Include(x => x.OrderItems)
                         .FirstOrDefaultAsync(x => x.Id == orderId);

        var orderItems =
            order!.OrderItems;

        return orderItems;
    }

    public override async Task<IEnumerable<Order>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var orders = await context.Orders
            .StoreFilter(ExecutionContext.StoreId)
            .Include(x => x.Customer)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return orders;
    }
}