using Application.Aggregates.PriceLists.ViewModels.PriceList;
using Application.Aggregates.PriceLists.ViewModels.PriceListItem;
using Domain;
using Domain.Aggregates.PriceLists;
using Framework.DataType;
using Mapster;
using Resources.Messages;

namespace Application.Aggregates.PriceLists;

public class PriceListsApplication(IPriceListRepository repository, IUnitOfWork unitOfWork)
{
    public async Task CreatePriceList(CreatePriceListViewModel viewModel)
    {
        var pricelist = PriceList.Create(viewModel.Title);
        await repository.AddAsync(pricelist);
        await unitOfWork.CommitAsync();
    }

    public async Task<PriceListViewModel> GetPriceListAsync(Guid id)
    {
        var pricelist = await repository.GetByIdAsync(id);
        return pricelist.Adapt<PriceListViewModel>();
    }

    public async Task<List<PriceListViewModel>> GetAllPriceListAsync()
    {
        var pricelist = await repository.GetAllAsync();
        return pricelist.Adapt<List<PriceListViewModel>>();
    }


    public async Task<ResultContract<PriceListItemViewModel>> UpdatePriceList(UpdatePriceListViewModel model)
    {
        var priceList = await repository.GetByIdAsync(model.Id);

        if (priceList == null || priceList.Id == Guid.Empty)
        {
            var message =
                string.Format(Errors.NotFound, Resources.DataDictionary.ProductFeature);

            return (ErrorType.NotFound, message);
        }

        priceList.Update(model.Title);

        await unitOfWork.CommitAsync();

        return priceList.Adapt<PriceListItemViewModel>();

    }

    public async Task<ResultContract> DeletePriceList(Guid id)
    {
        var priceList = await repository.GetByIdAsync(id);

        if (priceList == null || priceList.Id == Guid.Empty)
        {
            return (ErrorType.NotFound, Errors.NotFound);
        }

        await repository.RemoveAsync(priceList);

        await unitOfWork.CommitAsync();

        return true;
    }

}
