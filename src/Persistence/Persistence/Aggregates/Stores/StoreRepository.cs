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

    public Guid GetStoreByHostUrl(string hostUrl)
    {
        var store = uniBazzarContext.Stores
                    .Select(x => new { x.Id, x.HostUrl })
                    .FirstOrDefault(x => x.HostUrl == hostUrl);

        if (store == null)
            throw new Exception("Store not found");

        return store.Id;
    }

    public async Task<Guid> GetStoreByHostUrlAsync(string hostUrl)
    {
        var store = await uniBazzarContext.Stores
                    .Select(x => new { x.Id, x.HostUrl })
                    .FirstOrDefaultAsync(x => x.HostUrl == hostUrl);

        if (store == null)
            throw new Exception("Store not found");

        return store.Id;
    }

    public void RemoveStore(Store entity)
    {
        uniBazzarContext.Remove(entity);
    }
}