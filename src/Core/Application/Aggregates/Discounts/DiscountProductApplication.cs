using Application.Aggregates.Discounts.ViewModels;
using Domain;
using Domain.Aggregates.Discounts;
using Domain.Aggregates.Discounts.DsiscounProducts;
using Mapster;
using Resources.Messages;

namespace Application.Aggregates.Discounts;

public class DiscountProductApplication(IDiscountProductRepository repository, IUnitOfWork unitOfWork)
{

	public async Task<DiscountProductViewModel> CreateDiscountProduct(DiscountProductViewModel viewModel)
	{
		var discountProduct = DiscountProduct.Create(viewModel.DiscountId, viewModel.ProductId);

		await repository.AddAsync(discountProduct);
		await unitOfWork.SaveChangesAsync();

		return discountProduct.Adapt<DiscountProductViewModel>();
	}

	public async Task<List<DetailsAndDeleteDiscountProductViewModel>> GetAllDiscountProductByDiscountId(Guid discountId)
	{
		var discountProduct = await repository.GetAllDiscountProductByDiscountId(discountId);

		return discountProduct.Adapt<List<DetailsAndDeleteDiscountProductViewModel>>();
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
