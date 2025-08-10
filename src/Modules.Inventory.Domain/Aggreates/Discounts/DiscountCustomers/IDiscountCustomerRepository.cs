using BuildingBlocks.Domain.Data;

namespace Domain.Aggregates.Discounts.DiscountCustomers;

public interface IDiscountCustomerRepository : IRepositoryBase<DiscountCustomer>
{
	Task<List<DiscountCustomer>> GetAllDiscountCustomerByDiscountId(Guid discountId);
}
