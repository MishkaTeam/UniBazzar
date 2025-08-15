using BuildingBlocks.Persistence;
using Domain.Aggregates.PriceLists;
using Microsoft.EntityFrameworkCore;
using BuildingBlocks.Persistence.Extensions;
using BuildingBlocks.Domain.Context;

namespace Persistence.Repositories.Aggregates.Products;

public class PriceListsRepository : RepositoryBase<PriceList> ,IPriceListRepository
{

    public PriceListsRepository(UniBazzarContext context, IExecutionContextAccessor execution) : base(context, execution)
    {
    }

    public async Task<List<(Guid productId, decimal price)>> GetPrice(List<Guid> guids)
    {
        var prices = await DbSet
            .SelectMany(p => p.Items)
            .Where(item => guids.Contains(item.ProductId))
            .Select(item => new { item.ProductId, item.Price })
            .ToListAsync(); // از EF به LINQ to Objects سوییچ می‌کنیم

        return [.. prices.Select(x => (x.ProductId, x.Price))];
    }

    public Task<PriceList> GetPriceListItems(Guid id)
    {
        return DbSet
               .Include(x => x.Items)
               .ThenInclude(x => x.Product)
               .Where(x => x.Id == id)
               .StoreFilter(ExecutionContext.StoreId)
               .FirstOrDefaultAsync();
    }
}
