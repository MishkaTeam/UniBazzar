using BuildingBlocks.Domain.Data;

namespace Domain.Aggregates.Discounts.DsiscounProducts;

public interface IDiscountProductRepository : IRepositoryBase<DiscountProduct>
{
	Task<List<DiscountProduct>> GetAllDiscountProductByDiscountId(Guid discountId);
}
