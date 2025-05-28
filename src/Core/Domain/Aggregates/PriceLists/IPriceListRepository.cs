using Domain.BuildingBlocks.Data;

namespace Domain.Aggregates.PriceLists;

public interface IPriceListRepository : IRepositoryBase<PriceList>
{
    Task<List<(Guid productId, decimal price)>> GetPrice(List<Guid> guids);
    Task<PriceList> GetPriceListItems(Guid id);
}
