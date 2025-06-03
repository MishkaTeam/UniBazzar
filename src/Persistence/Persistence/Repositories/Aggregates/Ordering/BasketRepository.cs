using BuildingBlocks.Persistence;
using Domain.Aggregates.Ordering.Baskets;
using Domain.Aggregates.Ordering.Baskets.Data;
using Microsoft.EntityFrameworkCore;
using BuildingBlocks.Persistence.Extensions;

namespace Persistence.Repositories.Aggregates.Ordering;

public class BasketRepository(
    UniBazzarContext context,
    IExecutionContextAccessor executionContext)
    : RepositoryBase<Basket>(context, executionContext), IBasketRepository
{
    public async Task<Basket?> GetWithItemsByIdAsync(Guid requestBasketId)
    {
        var basket = await DbSet
            .Include(x => x.BasketItems)
            .StoreFilter(executionContext.StoreId)
            .FirstOrDefaultAsync(b => b.Id == requestBasketId);

        return basket;
    }

    public async Task<Basket?> GetWithItemsByReferenceNumberAsync(string referenceNumber)
    {
        var basket = await DbSet
            .Include(x => x.BasketItems)
            .StoreFilter(executionContext.StoreId)
            .FirstOrDefaultAsync(b => b.ReferenceNumber == referenceNumber);

        return basket;
    }
}