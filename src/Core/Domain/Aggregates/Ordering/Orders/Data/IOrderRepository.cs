using BuildingBlocks.Domain.Data;

namespace Domain.Aggregates.Ordering.Orders.Data;

public interface IOrderRepository : IRepositoryBase<Order>
{
    Task<List<OrderItem>> GetAllOrderItem(Guid orderId);
}