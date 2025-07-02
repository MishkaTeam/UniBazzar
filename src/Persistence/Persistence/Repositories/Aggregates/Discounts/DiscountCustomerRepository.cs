using BuildingBlocks.Persistence;
using Domain.Aggregates.Discounts.DiscountCustomers;
using Domain.Aggregates.Discounts.DsiscounProducts;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories.Aggregates.Discounts;

public class DiscountCustomerRepository : RepositoryBase<DiscountCustomer> , IDiscountCustomerRepository
{
	private readonly UniBazzarContext _context;

	public DiscountCustomerRepository(UniBazzarContext context, IExecutionContextAccessor execution) : base(context, execution)
	{
		_context = context;
	}

	public async Task<List<DiscountCustomer>> GetAllDiscountCustomerByDiscountId(Guid discountId)
	{
		var discountCustomers = await _context.DiscountCustomers.Where(x => x.DiscountId == discountId).ToListAsync();

		return discountCustomers;
	}
}
