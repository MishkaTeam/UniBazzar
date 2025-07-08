using Domain.Aggregates.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Persistence.Repositories.Aggregates.Stores;

public class StoreRepository
    (UniBazzarContext uniBazzarContext,
    IMemoryCache memoryCache) : IStoreRepository
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
        if (!memoryCache.TryGetValue(hostUrl, out Guid storeId))
        {

            var store = uniBazzarContext.Stores
                        .Select(x => new { x.Id, x.HostUrl })
                        .FirstOrDefault(x => x.HostUrl == hostUrl);

            if (store == null)
                throw new Exception("Store not found");

            memoryCache.Set(hostUrl, store.Id);
            return store.Id;
        }
        return storeId;
    }

    public async Task<Guid> GetStoreByHostUrlAsync(string hostUrl)
    {
        if (!memoryCache.TryGetValue(hostUrl, out Guid storeId))
        {

            var store = await uniBazzarContext.Stores
                    .Select(x => new { x.Id, x.HostUrl })
                    .FirstOrDefaultAsync(x => x.HostUrl == hostUrl);

        if (store == null)
            throw new Exception("Store not found");

            memoryCache.Set(hostUrl, store.Id);
            return store.Id;
        }
        return storeId;
    }

    public void RemoveStore(Store entity)
    {
        uniBazzarContext.Remove(entity);
    }
}