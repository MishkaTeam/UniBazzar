using Application.Aggregates.Ordering.Baskets.ViewModels.BasketItems;
using Application.Aggregates.Ordering.Baskets.ViewModels.Baskets;
using Application.Aggregates.Ordering.Baskets.ViewModels.InitializeBasket;
using BuildingBlocks.Domain.Context;
using BuildingBlocks.Persistence;
using Domain;
using Domain.Aggregates.Ordering.Baskets;
using Domain.Aggregates.Ordering.Baskets.Data;
using Domain.Aggregates.Ordering.Baskets.Enums;
using Domain.Aggregates.Ordering.ValueObjects;
using Framework.DataType;
using Microsoft.Extensions.Logging;

namespace Application.Aggregates.Ordering.Baskets;

public class BasketApplication(ILogger<BasketApplication> logger,
    IBasketRepository basketRepository, 
    IUnitOfWork unitOfWork,
    IExecutionContextAccessor executionContextAccessor)
{
    public async Task<ResultContract<InitializeBasketViewModel>> InitializeBasket(InitializeBasketRequestModel request)
    {
        var basket = Basket.Initialize(request.Platform);

        if (!string.IsNullOrWhiteSpace(request.Description))
            basket.SetDescription(request.Description);

        if (request.TotalDiscountType != DiscountType.None)
            basket.SetTotalDiscount(request.TotalDiscountAmount, request.TotalDiscountType);

        if (request.CustomerId != Guid.Empty)
        {
            basket.SetCustomer(request.CustomerId);
        }

        await basketRepository.AddAsync(basket);
        await unitOfWork.SaveChangesAsync();

        return new InitializeBasketViewModel(basket.Id, basket.ReferenceNumber);
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

    public async Task<ResultContract<BasketViewModel>> RemoveItem(Guid basketId, Guid basketItemId)
    {
        var basket =
            await basketRepository.GetByIdAsync(basketId);

        if (basket == null)
        {
            var message =
                string.Format(Resources.Messages.Errors.NotFound, Resources.DataDictionary.Basket);

            return (ErrorType.NotFound, message);
        }

        var basketItem =
            basket.BasketItems.FirstOrDefault(x => x.Id == basketItemId);

        if (basketItem == null)
        {
            var message =
                string.Format(Resources.Messages.Errors.NotFound, Resources.DataDictionary.Basket);

            return (ErrorType.NotFound, message);
        }

        basket.RemoveItem(basketItem);
        await unitOfWork.SaveChangesAsync();

        return BasketViewModel.FromBasket(basket);
    }

    public async Task<ResultContract> CheckoutBasket(Guid basketId)
    {
        var basket =
            await basketRepository.GetByIdAsync(basketId);

        var result =
            basket.Checkout();

        if (result == false)
        {
            return false;
        }

        await unitOfWork.SaveChangesAsync();
        return true;
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
    
    public async Task<ResultContract<BasketViewModel>> UpdateDescription(Guid basketId, string description)
    {
        try
        {
            var basket = await basketRepository.GetByIdAsync(basketId);
            basket.SetDescription(description);
            await unitOfWork.SaveChangesAsync();
            return BasketViewModel.FromBasket(basket);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "[BasketApplication] [UpdateDescription] Error in basketId : {BasketId}", basketId);
            return (ErrorType.NotFound, "Not Found");
        }
    }

    public async Task<ResultContract> SetCustomerAsync(Guid basketId, Guid customerId)
    {
        var basket =
            await basketRepository.GetByIdAsync(basketId);

        if (basket == null)
        {
            var message =
                string.Format(Resources.Messages.Errors.NotFound, Resources.DataDictionary.Basket);

            return (ErrorType.NotFound, message);
        }

        basket.SetCustomer(customerId);

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

        return BasketViewModel.FromBasket(basket);
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

        return BasketViewModel.FromBasket(basket);
    }

    public async Task<ResultContract<bool>> Exist(Guid id)
    {
        var basket =
            await basketRepository.GetByIdAsync(id);

        return basket != null;
    }


    public async Task<ResultContract<BasketViewModel>> SetBasePrice(Guid basketId, Guid basketItemId, decimal basePrice)
    {
        var basket =
            await basketRepository.GetByIdAsync(basketId);

        if (basket == null)
        {
            var message =
                string.Format(Resources.Messages.Errors.NotFound, Resources.DataDictionary.Basket);

            return (ErrorType.NotFound, message);
        }

        var basketItem =
            basket.BasketItems.FirstOrDefault(x => x.Id == basketItemId);

        if (basketItem == null)
        {
            var message =
                string.Format(Resources.Messages.Errors.NotFound, Resources.DataDictionary.Basket);

            return (ErrorType.NotFound, message);
        }

        basketItem.ProductAmount.SetBasePrice(basePrice);

        await unitOfWork.SaveChangesAsync();

        return BasketViewModel.FromBasket(basket);
    }

    public async Task<ResultContract<BasketViewModel>> UpdateDiscount(Guid basketId, Guid basketItemId, decimal discountValue, DiscountType discountType)
    {
        var basket =
            await basketRepository.GetByIdAsync(basketId);

        var basketItem =
            basket.BasketItems.FirstOrDefault(x => x.Id == basketItemId);

        var discount =
            DiscountAmount.Create(discountValue, discountType);

        basketItem?.DiscountAmount.UpdateDiscount(discount);

        await unitOfWork.SaveChangesAsync();
        return BasketViewModel.FromBasket(basket);
    }

    public async Task<ResultContract<BasketViewModel>> SetDiscountValue(Guid basketId, Guid basketItemId, decimal value)
    {
        var basket =
            await basketRepository.GetByIdAsync(basketId);

        if (basket == null)
        {
            var message =
                string.Format(Resources.Messages.Errors.NotFound, Resources.DataDictionary.Basket);

            return (ErrorType.NotFound, message);
        }


        var basketItem =
            basket.BasketItems.FirstOrDefault(x => x.Id == basketItemId);

        if (basketItem == null)
        {
            var message =
                string.Format(Resources.Messages.Errors.NotFound, Resources.DataDictionary.Basket);

            return (ErrorType.NotFound, message);
        }

        basketItem.DiscountAmount.SetValue(value);

        await unitOfWork.SaveChangesAsync();

        return BasketViewModel.FromBasket(basket);
    }

    public async Task<ResultContract<BasketViewModel>> SetQuantity(Guid basketId, Guid basketItemId, long quantity)
    {
        var basket =
            await basketRepository.GetByIdAsync(basketId);

        if (basket == null)
        {
            var message =
                string.Format(Resources.Messages.Errors.NotFound, Resources.DataDictionary.Basket);

            return (ErrorType.NotFound, message);
        }

        var basketItem =
            basket.BasketItems.FirstOrDefault(x => x.Id == basketItemId);

        if (basketItem == null)
        {
            var message =
                string.Format(Resources.Messages.Errors.NotFound, Resources.DataDictionary.Basket);

            return (ErrorType.NotFound, message);
        }

        basketItem.ProductAmount.SetQuantity(quantity);

        await unitOfWork.SaveChangesAsync();

        return BasketViewModel.FromBasket(basket);
    }

    public async Task<ResultContract<BasketViewModel>> SetAffectedQuantity(Guid basketId, Guid basketItemId, long changedQuantity)
    {
        var basket =
            await basketRepository.GetByIdAsync(basketId);

        if (basket == null)
        {
            var message =
                string.Format(Resources.Messages.Errors.NotFound, Resources.DataDictionary.Basket);

            return (ErrorType.NotFound, message);
        }

        var basketItem =
            basket.BasketItems.FirstOrDefault(x => x.Id == basketItemId);

        if (basketItem == null)
        {
            var message =
                string.Format(Resources.Messages.Errors.NotFound, Resources.DataDictionary.Basket);

            return (ErrorType.NotFound, message);
        }

        basketItem.ProductAmount.SetAffectedQuantity(changedQuantity);

        await unitOfWork.SaveChangesAsync();

        return BasketViewModel.FromBasket(basket);
    }

    public async Task<ResultContract<BasketViewModel>> PlusQuantity(Guid basketId, Guid basketItemId)
    {
        var basket =
            await basketRepository.GetByIdAsync(basketId);

        if (basket == null)
        {
            var message =
                string.Format(Resources.Messages.Errors.NotFound, Resources.DataDictionary.Basket);

            return (ErrorType.NotFound, message);
        }

        var basketItem =
            basket.BasketItems.FirstOrDefault(x => x.Id == basketItemId);

        if (basketItem == null)
        {
            var message =
                string.Format(Resources.Messages.Errors.NotFound, Resources.DataDictionary.Basket);

            return (ErrorType.NotFound, message);
        }

        basketItem.ProductAmount.PlusQuantity();

        await unitOfWork.SaveChangesAsync();

        return BasketViewModel.FromBasket(basket);
    }

    public async Task<ResultContract<BasketViewModel>> MinusQuantity(Guid basketId, Guid basketItemId)
    {
        var basket =
            await basketRepository.GetByIdAsync(basketId);

        if (basket == null)
        {
            var message =
                string.Format(Resources.Messages.Errors.NotFound, Resources.DataDictionary.Basket);

            return (ErrorType.NotFound, message);
        }

        var basketItem =
            basket.BasketItems.FirstOrDefault(x => x.Id == basketItemId);

        if (basketItem == null)
        {
            var message =
                string.Format(Resources.Messages.Errors.NotFound, Resources.DataDictionary.Basket);

            return (ErrorType.NotFound, message);
        }

        basketItem.ProductAmount.MinusQuantity();

        await unitOfWork.SaveChangesAsync();

        return BasketViewModel.FromBasket(basket);
    }

    public async Task<ResultContract<BasketViewModel>> TryUpdateBasketInfo(Guid basketId)
    {
        var basket =
            await basketRepository.GetByIdAsync(basketId);

        if (basket == null)
        {
            var message =
                string.Format(Resources.Messages.Errors.NotFound, Resources.DataDictionary.Basket);

            return (ErrorType.NotFound, message);
        }

        if(executionContextAccessor.UserId == null)
        {
            var message =
               string.Format(Resources.Messages.Errors.NotFound, Resources.DataDictionary.User);

            return (ErrorType.NotFound, message);
        }

        basket.SetOwner(executionContextAccessor.UserId.Value);

        await unitOfWork.SaveChangesAsync();

        return BasketViewModel.FromBasket(basket);
    }
}