using Domain.BuildingBlocks.Data;

namespace Domain.Aggregates.Products.ProductPriceLists;

public interface IProductPriceListRepository : IRepositoryBase<ProductPriceList>
{
    Task<ProductPriceList> GetPriceByProductId(Guid productid);
    Task<List<ProductPriceList>> GetPriceListByProductId(Guid productid);
}
