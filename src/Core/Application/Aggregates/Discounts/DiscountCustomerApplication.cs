using Application.Aggregates.Discounts.ViewModels;
using Domain;
using Domain.Aggregates.Discounts.DiscountCustomers;
using Domain.Aggregates.Discounts.DsiscounProducts;
using Mapster;
using Resources.Messages;

namespace Application.Aggregates.Discounts;

public class DiscountCustomerApplication(IDiscountCustomerRepository repository, IUnitOfWork unitOfWork)
{

	public async Task<DiscountCustomerViewModel> CreateDiscountProduct(DiscountCustomerViewModel viewModel)
	{
		var discountCustomer = DiscountCustomer.Create(viewModel.DiscountId, viewModel.CustomerId);

		await repository.AddAsync(discountCustomer);
		await unitOfWork.SaveChangesAsync();

		return discountCustomer.Adapt<DiscountCustomerViewModel>();
	}

	public async Task<List<DetailsAndDeleteDiscountCustomerViewModel>> GetAllDiscountProductByDiscountId(Guid discountId)
	{
		var discountCustomer = await repository.GetAllDiscountCustomerByDiscountId(discountId);

		return discountCustomer.Adapt<List<DetailsAndDeleteDiscountCustomerViewModel>>();
	}

	public async Task DeleteDiscountProductAsync(Guid id)
	{
		var discountProductForDelete =
			await repository.GetByIdAsync(id);

		if (discountProductForDelete == null || discountProductForDelete.Id == Guid.Empty)
		{
			var message =
				string.Format(Errors.NotFound, Resources.DataDictionary.Discount);

			throw new Exception(message);
		}

		repository.RemoveAsync(discountProductForDelete);
		await unitOfWork.SaveChangesAsync();
	}

}
