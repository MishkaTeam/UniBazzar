using Application.Aggregates.Stores.ViewModels;
using Domain;
using Domain.Aggregates.Stores;
using Mapster;
using Resources.Messages;

namespace Application.Aggregates.Stores;

public class StoresApplication
	(IStoreRepository storeRepository, IUnitOfWork unitOfWork)
{
	public async Task<StoreViewModel> CreateStoreAsync(CreateStoreViewModel viewModel)
	{
		var store = Store.Create(viewModel.Name, viewModel.Description, viewModel.PhoneNumber,
			viewModel.Address, viewModel.Culture, viewModel.LogoUrl, viewModel.IsActive);

		await storeRepository.AddStoreAsync(store);
		await unitOfWork.SaveChangesAsync();

		return store.Adapt<StoreViewModel>();
	}

	public async Task<List<StoreViewModel>> GetStores()
	{
		var stores =
			await storeRepository.GetAllStoresAsync();

		return stores.Adapt<List<StoreViewModel>>();
	}

	public async Task<StoreViewModel> GetStoreAsync(Guid id)
	{
		var store =
			await storeRepository.GetStoreAsync(id);

		return store.Adapt<StoreViewModel>();
	}

	public async Task<StoreViewModel> UpdateStoreAsync(StoreViewModel updateViewModel)
	{
		var storeForUpdate =
			await storeRepository.GetStoreAsync(updateViewModel.Id);

		if (storeForUpdate == null || storeForUpdate.Id == Guid.Empty)
		{
			var message =
				string.Format(Errors.NotFound, Resources.DataDictionary.Store);

			throw new Exception(message);
		}

		storeForUpdate.Update(updateViewModel.Name, updateViewModel.Description,
			updateViewModel.PhoneNumber, updateViewModel.Culture,
			updateViewModel.LogoUrl);

		await unitOfWork.SaveChangesAsync();
		return storeForUpdate.Adapt<StoreViewModel>();
	}

	public async Task DeleteStoreAsync(Guid id)
	{
		var storeForDelete =
			await storeRepository.GetStoreAsync(id);

		if (storeForDelete == null || storeForDelete.Id == Guid.Empty)
		{
			var message =
				string.Format(Errors.NotFound, Resources.DataDictionary.Store);

			throw new Exception(message);
		}

		storeRepository.RemoveStore(storeForDelete);
		await unitOfWork.SaveChangesAsync();
	}
}