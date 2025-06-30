using Application.Aggregates.Ordering.Baskets.ViewModels.BasketItems;
using Application.Aggregates.Ordering.Baskets.ViewModels.Baskets;
using Application.Aggregates.Ordering.Baskets.ViewModels.InitializeBasket;
using Domain;
using Domain.Aggregates.Ordering.Baskets;
using Domain.Aggregates.Ordering.Baskets.Data;
using Domain.Aggregates.Ordering.Baskets.Enums;
using Domain.Aggregates.Ordering.ValueObjects;
using Framework.DataType;
using Mapster;
using Microsoft.Extensions.Logging;

namespace Application.Aggregates.Ordering.Baskets;

public class BasketApplication(ILogger<BasketApplication> logger, IBasketRepository basketRepository, IUnitOfWork unitOfWork)
{
    public async Task<ResultContract<InitializeBasketViewModel>> InitializeBasket(InitializeBasketRequestModel request)
    {
        var basket = Basket.Initialize(request.Platform);

        if (!string.IsNullOrWhiteSpace(request.Description))
            basket.SetDescription(request.Description);

        if (request.TotalDiscountType != DiscountType.None)
            basket.SetTotalDiscount(request.TotalDiscountAmount, request.TotalDiscountType);

        await basketRepository.AddAsync(basket);
        await unitOfWork.SaveChangesAsync();

        return new InitializeBasketViewModel(basket.Id, basket.ReferenceNumber);
    }


    public async Task<ResultContract<BasketViewModel>> UpdateTotalDiscount(Guid basketId, decimal totalDiscountAmount, DiscountType totalDiscountType)
    {
        try
        {
            var basket = await basketRepository.GetByIdAsync(basketId);

            if (totalDiscountType != DiscountType.None)
                basket.SetTotalDiscount(totalDiscountAmount, totalDiscountType);
            else
                basket.SetTotalDiscount(0m, DiscountType.None);

            await unitOfWork.SaveChangesAsync();
            return BasketViewModel.FromBasket(basket);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "[BasketApplication] [UpdateTotalDiscount] Error in basketId : {BasketId}", basketId);
            return (ErrorType.InternalError, Resources.Messages.Errors.InternalError);
        }
    }


    public async Task<ResultContract> UpdateDescription(Guid basketId, string description)
    {
        try
        {
            var basket = await basketRepository.GetByIdAsync(basketId);
            basket.SetDescription(description);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "[BasketApplication] [UpdateDescription] Error in basketId : {BasketId}", basketId);
            return false;
        }
    }

    public async Task<ResultContract<BasketViewModel>> AddItem(AddBasketItemRequestModel basketItemRequest)
    {
        var basket = await basketRepository.GetByIdAsync(basketItemRequest.BasketId);
        var product = ProductType.Create(basketItemRequest.ProductId, basketItemRequest.ProductName);
        var amount = ProductAmount.Create(basketItemRequest.Quantity, basketItemRequest.BasePrice);
        var discount = DiscountAmount.Create(basketItemRequest.DiscountAmount, basketItemRequest.DiscountType);

        List<BasketItemAttribute> basketAttributes = null!;
        if (basketItemRequest.BasketItemAttributes != null
            && basketItemRequest.BasketItemAttributes.Count != 0)
        {
            basketAttributes = basketItemRequest.ToBasketItemAttribute();
        }

        var basketItem = BasketItem.Create(
            basket.Id, 
            basket.ReferenceNumber, 
            product, 
            amount, 
            discount, 
            basketAttributes);

        basket.AddItem(basketItem);
        await unitOfWork.SaveChangesAsync();
        return BasketViewModel.FromBasket(basket);
    }

    public async Task<ResultContract> CheckoutBasket(Guid basketId)
    {
        var basket = await basketRepository.GetByIdAsync(basketId);
        basket.Checkout();
        await unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<ResultContract<BasketViewModel>> GetByReferenceNumber(string referenceNumber)
    {
        var basket =
            await basketRepository.GetWithItemsByReferenceNumberAsync(referenceNumber);

        if (basket == null)
        {
            var message =
                string.Format(Resources.Messages.Errors.NotFound, Resources.DataDictionary.Basket);

            return (ErrorType.NotFound, message);
        }

        return new BasketViewModel
        {
            Id = basket.Id,
            OwnerId = basket.OwnerId,
            Platform = basket.Platform,
            BasketStatus = basket.BasketStatus,
            ReferenceNumber = basket.ReferenceNumber,
            BasketItems = basket.BasketItems.Select(x => new BasketItemViewModel
            {
                ProductId = x.Product.ProductId,
                ProductName = x.Product.ProductName,
                Quantity = x.ProductAmount.Quantity,
                BasePrice = x.ProductAmount.BasePrice,
                TotalPrice = x.ProductAmount.TotalPrice,
                DiscountValue = x.DiscountAmount.Value,
                DiscountType = x.DiscountAmount.DiscountType,
            }).ToList()
        };
    }

    public async Task<ResultContract<BasketViewModel>> GetBasket(Guid basketId)
    {
        var basket =
            await basketRepository.GetByIdAsync(basketId);

        if (basket == null)
        {
            var message =
                string.Format(Resources.Messages.Errors.NotFound, Resources.DataDictionary.Basket);

            return (ErrorType.NotFound, message);
        }

        return new BasketViewModel
        {
            Id = basket.Id,
            Platform = basket.Platform,
            BasketStatus = basket.BasketStatus,
            ReferenceNumber = basket.ReferenceNumber,
            BasketItems = basket.BasketItems.Select(x => new BasketItemViewModel
            {
                ProductId = x.Product.ProductId,
                ProductName = x.Product.ProductName,
                Quantity = x.ProductAmount.Quantity,
                BasePrice = x.ProductAmount.BasePrice,
                TotalPrice = x.ProductAmount.TotalPrice,
                DiscountValue = x.DiscountAmount.Value,
                DiscountType = x.DiscountAmount.DiscountType,
            }).ToList()
        };
    }

    public async Task<ResultContract<BasketViewModel>> ChangeOwnerAsync(Guid basketId, Guid ownerId)
    {
        var basket =
            await basketRepository.GetByIdAsync(basketId);

        basket.SetOwner(ownerId);

        await unitOfWork.SaveChangesAsync();

        return basket.Adapt<BasketViewModel>();
    }

    public async Task<ResultContract<bool>> Exist(Guid id)
    {
        var basket =
            await basketRepository.GetByIdAsync(id);

        return basket != null;
    }
}