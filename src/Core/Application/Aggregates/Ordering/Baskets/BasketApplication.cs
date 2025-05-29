using Application.Aggregates.Ordering.Baskets.ViewModels.Baskets;
using Application.Aggregates.Orders.ViewModels;
using Application.Aggregates.Orders.ViewModels.BasketItems;
using Domain;
using Domain.Aggregates.Ordering.Baskets;
using Domain.Aggregates.Ordering.Baskets.Data;
using Domain.Aggregates.Ordering.ValueObjects;
using Framework.DataType;
using Mapster;

namespace Application.Aggregates.Orders;

public class BasketApplication(IBasketRepository basketRepository, IUnitOfWork unitOfWork)
{
    public async Task<ResultContract<InitializeBasketViewModel>> InitializeBasket(InitializeBasketRequestModel request)
    {
        var basket = Basket.Initialize(request.Platform, request.OwnerId);
        await basketRepository.AddAsync(basket);
        await unitOfWork.CommitAsync();
        return new InitializeBasketViewModel(basket.Id, basket.OwnerId);
    }

    public async Task<ResultContract<BasketItemViewModel>> AddItem(AddBasketItemRequestModel basketItemRequest)
    {
        var basket = await basketRepository.GetByIdAsync(basketItemRequest.BasketId);
        var product = ProductType.Create(basketItemRequest.ProductId, basketItemRequest.ProductName);
        var amount = ProductAmount.Create(basketItemRequest.Quantity, basketItemRequest.BasePrice);
        var discount = DiscountAmount.Create(basketItemRequest.DiscountAmount, basketItemRequest.DiscountType);
        var basketItem = BasketItem.Create(basket.Id, basket.ReferenceNumber, product, amount, discount);
        basket.AddItem(basketItem);
        await unitOfWork.CommitAsync();
        return basketItem.Adapt<BasketItemViewModel>();
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
            OwnerId = basket.OwnerId,
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
            OwnerId = basket.OwnerId,
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

    public async Task<ResultContract<BasketViewModel>> ChangeOwnerAsync(Guid basketId, Guid ownerId)
    {
        var basket =
            await basketRepository.GetByIdAsync(basketId);

        basket.SetOwner(ownerId);

        await unitOfWork.CommitAsync();

        return basket.Adapt<BasketViewModel>();
    }

    public async Task<ResultContract<bool>> Exist(Guid id)
    {
        var basket =
            await basketRepository.GetByIdAsync(id);

        return basket != null;
    }
}