using BuildingBlocks.Domain.Context;
using BuildingBlocks.Persistence;
using BuildingBlocks.Persistence.Extensions;
using Domain.Aggregates.Discounts.DsiscounProducts;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories.Aggregates.Discounts;

public class DiscountProductRepository : RepositoryBase<DiscountProduct>, IDiscountProductRepository
{
	private readonly UniBazzarContext _context;

	public DiscountProductRepository(UniBazzarContext context, IExecutionContextAccessor execution) : base(context, execution)
	{
		_context = context;
	}

	public async Task<List<DiscountProduct>> GetAllDiscountProductByDiscountId(Guid discountId)
	{
		var discountProducts = await _context.DiscountProducts
			.Include(x => x.Product)
			.Where(x => x.DiscountId == discountId)
			.StoreFilter(ExecutionContext.StoreId)
			.ToListAsync();

		return discountProducts;
	}
}
