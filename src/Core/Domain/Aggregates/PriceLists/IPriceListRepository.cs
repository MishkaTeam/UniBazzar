using Domain.BuildingBlocks.Data;

namespace Domain.Aggregates.PriceLists;

public interface IPriceListRepository : IRepositoryBase<PriceList>
{
    Task<PriceList> GetPriceListItems(Guid id);
}
