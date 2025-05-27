using BuildingBlocks.Persistence;
using Domain.Aggregates.PriceLists;
using Microsoft.EntityFrameworkCore;
using Persistence.Extensions;

namespace Persistence.Aggregates.Products;

public class PriceListsRepository : RepositoryBase<PriceList> ,IPriceListRepository
{

    public PriceListsRepository(UniBazzarContext context, IExecutionContextAccessor execution) : base(context, execution)
    {
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
