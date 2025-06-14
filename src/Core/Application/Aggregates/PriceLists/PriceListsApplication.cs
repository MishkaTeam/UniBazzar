﻿using Application.Aggregates.PriceLists.ViewModels.PriceList;
using Application.Aggregates.PriceLists.ViewModels.PriceListItem;
using Domain;
using Domain.Aggregates.PriceLists;
using Framework.DataType;
using Mapster;
using Resources;
using Resources.Messages;

namespace Application.Aggregates.PriceLists;

public class PriceListsApplication(IPriceListRepository repository, IUnitOfWork unitOfWork)
{
    public async Task CreatePriceList(CreatePriceListViewModel viewModel)
    {
        var pricelist = PriceList.Create(viewModel.Title);
        await repository.AddAsync(pricelist);
        await unitOfWork.SaveChangesAsync();
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

        await unitOfWork.SaveChangesAsync();

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

        await unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<ResultContract> AddPricelistItem(CreatePriceListItemViewModel model)
    {
        try
        {
            var priceList = await repository.GetByIdAsync(model.PriceListId);

            if (priceList.Exists(model.ProductId))
                return (ErrorType.DuplicateRecord, string.Format(Errors.AlreadyExists, DataDictionary.Product));

            priceList.AddItem(model.ProductId, model.Price, "IRR");
            await unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<ResultContract<List<PriceListItemViewModel>>> GetPriceListItems(Guid id)
    {
        var res = await repository.GetPriceListItems(id);

        if (res == null)
        {
            return (ErrorType.NotFound, Resources.DataDictionary.NothingFound);
        }

        return res.Items.Select(x => new PriceListItemViewModel
        {
            Id = x.Id,
            Price = x.Price,
            PriceListId = res.Id,
            ProductId = x.ProductId,
            ProductName = x.Product.Name,
        }).ToList();
    }

    public async Task<ResultContract> RemovePricelistItem(Guid priceListId, Guid priceListItemId)
    {
        var res = await repository.GetPriceListItems(priceListId);
        if (res == null)
        {
            return (ErrorType.NotFound, Resources.DataDictionary.NothingFound);
        }

        res.RemoveItem(priceListItemId);
        await unitOfWork.SaveChangesAsync();

        return true;
    }
}
