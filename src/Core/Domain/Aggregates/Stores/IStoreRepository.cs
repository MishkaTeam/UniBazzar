﻿namespace Domain.Aggregates.Stores;

public interface IStoreRepository
{
	Task AddStoreAsync(Store entity);
	Task<List<Store>> GetAllStoresAsync();
	Task<Store?> GetStoreAsync(Guid id);
	void RemoveStore(Store entity);
}