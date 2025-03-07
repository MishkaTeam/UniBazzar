using Application.Aggregates.Orders.ViewModels;
using Application.Aggregates.Orders.ViewModels.BasketItems;
using Domain;
using Domain.Aggregates.Ordering.Baskets;
using Domain.Aggregates.Ordering.Baskets.Data;
using Mapster;

namespace Application.Aggregates.Orders;

public class BasketApplication(IBasketRepository basketRepository, IUnitOfWork unitOfWork)
{
    public async Task<InitializeBasketViewModel> InitializeBasket(InitializeBasketRequestModel request)
    {
        var basket = Basket.Initialize(request.platform);
        await basketRepository.AddAsync(basket);
        await unitOfWork.CommitAsync();
        return new(basket.ReferenceNumber);
    }

    public async Task<BasketItemViewModel> AddItem(AddBasketItemRequestModel basketItemRequest)
    {
        var basket = await basketRepository.GetByIdAsync(basketItemRequest.BasketId);
        var basketItem = BasketItem.Create(basket.Id, basket.ReferenceNumber,basketItemRequest.Product, basketItemRequest.Quantity);
        await basketRepository.AddItemToBasket(basketItem);
        await unitOfWork.CommitAsync();
        return basketItem.Adapt<BasketItemViewModel>(); 
    }
}