using Application.Aggregates.Ordering.Baskets.ViewModels.Baskets;
using Application.Aggregates.Orders.ViewModels;
using Application.Aggregates.Orders.ViewModels.BasketItems;
using Domain;
using Domain.Aggregates.Ordering.Baskets;
using Domain.Aggregates.Ordering.Baskets.Data;
using Domain.Aggregates.Ordering.Baskets.Enums;
using Domain.Aggregates.Ordering.ValueObjects;
using Framework.DataType;
using Mapster;
using Microsoft.Extensions.Logging;

namespace Application.Aggregates.Orders;

public class BasketApplication(ILogger<BasketApplication> logger, IBasketRepository basketRepository, IUnitOfWork unitOfWork)
{
    public async Task<ResultContract<InitializeBasketViewModel>> InitializeBasket(InitializeBasketRequestModel request)
    {
        var basket = Basket.Initialize(request.platform);

        if (!string.IsNullOrWhiteSpace(request.Description))
            basket.SetDescription(request.Description);

        if (request.TotalDiscountType != DiscountType.None)
            basket.SetTotalDiscount(request.TotalDiscountAmount, request.TotalDiscountType);

        await basketRepository.AddAsync(basket);
        await unitOfWork.CommitAsync();

        return new InitializeBasketViewModel(basket.ReferenceNumber);
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

            await unitOfWork.CommitAsync();
            return BasketViewModel.FormBasket(basket);
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
            await unitOfWork.CommitAsync();
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
        var basketItem = BasketItem.Create(basket.Id, basket.ReferenceNumber, product, amount, discount);
        basket.AddItem(basketItem);
        await unitOfWork.CommitAsync();
        return BasketViewModel.FormBasket(basket);
    }

    public async Task<ResultContract> CheckoutBasket(Guid basketId)
    {
        var basket = await basketRepository.GetByIdAsync(basketId);
        basket.Checkout();
        await unitOfWork.CommitAsync();
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
            Platform = basket.PlatForm,
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
            Platform = basket.PlatForm,
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
}