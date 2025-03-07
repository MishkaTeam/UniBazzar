using Domain.BuildingBlocks.Data;

namespace Domain.Aggregates.Ordering.Baskets.Data;

public interface IBasketRepository : IRepositoryBase<Basket>
{
    Task AddItemToBasket(BasketItem basketItem);
}