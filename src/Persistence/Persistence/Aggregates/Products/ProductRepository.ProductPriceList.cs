using BuildingBlocks.Persistence;
using Domain.Aggregates.PriceLists;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Aggregates.Products;

public class PriceListsRepository : RepositoryBase<PriceList> ,IPriceListRepository
{

    public PriceListsRepository(UniBazzarContext context, IExecutionContextAccessor execution) : base(context, execution)
    {
    }
}
