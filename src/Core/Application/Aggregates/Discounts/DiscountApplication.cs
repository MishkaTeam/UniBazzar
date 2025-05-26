using Application.Aggregates.Discounts.ViewModels;
using Application.Aggregates.Products.ViewModels;
using Domain;
using Domain.Aggregates.Discounts;
using Mapster;
using Resources.Messages;

namespace Application.Aggregates.Discounts;

public class DiscountApplication(IDiscountRepository discountRepository, IUnitOfWork unitOfWork)
{

    public async Task<CreateDiscountViewModel> CreateDiscount(CreateDiscountViewModel viewModel)
    {
        var discount = Discount.Create(viewModel.Title, viewModel.DiscountCode, viewModel.IsActive, viewModel.Type, viewModel.Minimum,
                                       viewModel.Maximum, viewModel.Start, viewModel.End, viewModel.Amount);

        await discountRepository.AddAsync(discount);
        await unitOfWork.CommitAsync();

        return discount.Adapt<CreateDiscountViewModel>();
    }

    public async Task<List<DiscountViewModel>> GetAllDiscount()
    {
        var discount = await discountRepository.GetAllAsync();

        return discount.Adapt<List<DiscountViewModel>>();
    }

    public async Task<DiscountViewModel> GetDiscountAsync(Guid id)
    {
        var product =
            await discountRepository.GetByIdAsync(id);

        return product.Adapt<DiscountViewModel>();
    }

    public async Task<DiscountViewModel> UpdateDiscountAsync(DiscountViewModel updateViewModel)
    {
        var discountForUpdate =
            await discountRepository.GetByIdAsync(updateViewModel.Id);

        if (discountForUpdate == null || discountForUpdate.Id == Guid.Empty)
        {
            var message =
                string.Format(Errors.NotFound, Resources.DataDictionary.Discount);

            throw new Exception(message);
        }

        discountForUpdate.Update(updateViewModel.Title, updateViewModel.DiscountCode, updateViewModel.IsActive, updateViewModel.Type, updateViewModel.Minimum,
							     updateViewModel.Maximum, updateViewModel.Start, updateViewModel.End, updateViewModel.Amount);

        await unitOfWork.CommitAsync();
        return discountForUpdate.Adapt<DiscountViewModel>();
    }

    public async Task DeleteDiscountAsync(Guid id)
    {
        var discountForDelete =
            await discountRepository.GetByIdAsync(id);

        if (discountForDelete == null || discountForDelete.Id == Guid.Empty)
        {
            var message =
                string.Format(Errors.NotFound, Resources.DataDictionary.Discount);

            throw new Exception(message);
        }

        discountRepository.RemoveAsync(discountForDelete);
        await unitOfWork.CommitAsync();
    }
}
