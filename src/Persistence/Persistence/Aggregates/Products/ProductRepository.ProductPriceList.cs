using BuildingBlocks.Persistence;
using Domain.Aggregates.PriceLists;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Aggregates.Products;

public class ProductPriceListsRepository : RepositoryBase<PriceList> ,IPriceListRepository
{

    public ProductPriceListsRepository(UniBazzarContext context, IExecutionContextAccessor execution) : base(context, execution)
    {
    }
}
