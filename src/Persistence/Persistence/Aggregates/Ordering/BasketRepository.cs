using BuildingBlocks.Persistence;
using Domain.Aggregates.Ordering.Baskets;
using Domain.Aggregates.Ordering.Baskets.Data;
using Microsoft.EntityFrameworkCore;
using Persistence.Extensions;

namespace Persistence.Aggregates.Ordering;

public class BasketRepository(
    UniBazzarContext context,
    IExecutionContextAccessor executionContext)
    : RepositoryBase<Basket>(context, executionContext), IBasketRepository
{
    public Task AddItemToBasket(BasketItem basketItem)
    {
        throw new NotImplementedException();
    }

    public async Task<Basket?> GetWithItemsByIdAsync(Guid requestBasketId)
    {
        var basket = await DbSet
            .Include(x => x.BasketItems)
            .StoreFilter(executionContext.StoreId)
            .FirstOrDefaultAsync(b => b.Id == requestBasketId);
        return basket;
    }
}