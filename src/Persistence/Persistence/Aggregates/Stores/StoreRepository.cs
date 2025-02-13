using Domain.Aggregates.Stores;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Aggregates.Stores;

public class StoreRepository
	(UniBazzarContext uniBazzarContext) : IStoreRepository
{
	public async Task AddStoreAsync(Store entity)
	{
		await uniBazzarContext.AddAsync(entity);
	}

	public async Task<List<Store>> GetAllStoresAsync()
	{
		return await uniBazzarContext.Stores.ToListAsync();
	}

	public async Task<Store?> GetStoreAsync(Guid id)
	{
		var store = await uniBazzarContext.Stores
					.FirstOrDefaultAsync(x => x.Id == id);

		return store;
	}

	public void RemoveStore(Store entity)
	{
		uniBazzarContext.Remove(entity);
	}
}