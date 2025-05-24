using Application.Aggregates.PriceLists.ViewModels.PriceList;
using Application.Aggregates.PriceLists.ViewModels.PriceListItem;
using Domain;
using Domain.Aggregates.PriceLists;
using Mapster;
using Resources.Messages;

namespace Application.Aggregates.PriceLists;

public class PriceListsApplication(IPriceListRepository repository ,IUnitOfWork unitOfWork)
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

    public async Task<List<PriceListItemViewModel>> GetAllPriceListAsync()
    {
        var pricelist = await repository.GetAllAsync();
        return pricelist.Adapt<List<PriceListItemViewModel>>();
    }

    //public async Task<PriceListItemViewModel> UpdatePriceList(PriceListItemViewModel model)
    //{
    //    var productpricelistForUpdate = await productPriceList.GetByIdAsync(model.Id);

    //    if (productpricelistForUpdate == null || productpricelistForUpdate.Id == Guid.Empty)
    //    {
    //        var message =
    //            string.Format(Errors.NotFound, Resources.DataDictionary.ProductFeature);

    //        throw new Exception(message);
    //    }

    //    productpricelistForUpdate.Update(model.ProductId, model.Price);

    //    await unitOfWork.CommitAsync();

    //    return productpricelistForUpdate.Adapt<PriceListItemViewModel>();

    //}

    //public async Task DeletePriceList(Guid id)
    //{
    //    var productpricelistForDelete = await productPriceList.GetByIdAsync(id);

    //    if (productpricelistForDelete == null || productpricelistForDelete.Id == Guid.Empty)
    //    {
    //        throw new Exception(Errors.NotFound);
    //    }

    //    productPriceList.RemoveAsync(productpricelistForDelete);

    //    await unitOfWork.CommitAsync();
    //}

}
