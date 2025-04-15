using Domain.BuildingBlocks.Data;

namespace Domain.Aggregates.Ordering.Baskets.Data;

public interface IBasketRepository : IRepositoryBase<Basket>
{
    Task<Basket?> GetWithItemsByIdAsync(Guid requestBasketId);
}