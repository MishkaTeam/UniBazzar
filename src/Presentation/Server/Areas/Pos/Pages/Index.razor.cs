using Application.Aggregates.Ordering.Baskets.ViewModels.Baskets;
using Application.Aggregates.Orders.ViewModels;
using Application.Aggregates.Orders.ViewModels.BasketItems;
using Domain.Aggregates.Ordering.Baskets.Enums;
using Server.Areas.Pos.Components;

namespace Server.Areas.Pos.Pages;

public partial class Index
{
    public static string LocalBasketKey { get; } = "Basket";
    public BasketViewModel? Basket { get; private set; }


    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await LoadBasketAsync();
            await InvokeAsync(StateHasChanged);
        }
    }

    private async Task LoadBasketAsync()
    {
        var customerId =
            await GetLocalCustomer();

        var localBasket =
            await localStorage.GetItemAsync<InitializeBasketViewModel>(LocalBasketKey);

        if (localBasket == null)
        {
            await CreateLocalBasket(customerId);

            return;
        }

        var basketIsExist =
            (await basketApplication.Exist(localBasket.Id)).Data;

        if (basketIsExist == false)
        {
            await CreateLocalBasket(customerId);

            return;
        }

        if (localBasket.OwnerId != customerId)
        {
            var result =
                await basketApplication.ChangeOwnerAsync(localBasket!.Id, customerId);

            var basket =
                new InitializeBasketViewModel(result.Data!.Id, result.Data.OwnerId);

            await localStorage.SetItemAsync(LocalBasketKey, basket);

            Basket = result.Data;

            return;
        }

        Basket = (await basketApplication.GetBasket(localBasket.Id)).Data;
    }

    private async Task<InitializeBasketViewModel> CreateLocalBasket(Guid ownerId)
    {
        var newBasket = (await basketApplication.InitializeBasket(
            new InitializeBasketRequestModel()
            {
                Platform = Platform.POS,
                OwnerId = ownerId,
            })).Data;

        await localStorage.SetItemAsync(LocalBasketKey, newBasket);

        return newBasket!;
    }

    private async Task<Guid> GetLocalCustomer()
    {
        var customerId =
            await localStorage.GetItemAsync<Guid?>(SearchCustomer.LocalCustomerKey);

        if (customerId.HasValue == false)
        {
            var publicCustomerId = await SetPublicCustomer();

            return publicCustomerId;
        }

        var customerIsExist =
            (await customerApplication.Exist(customerId.Value)).Data;

        if (customerIsExist == false)
        {
            var publicCustomerId = await SetPublicCustomer();

            return publicCustomerId;
        }

        return customerId.Value;
    }

    private async Task<Guid> SetPublicCustomer()
    {
        var publicCustomerId =
            (await customerApplication.GetPublicCustomer()).Data;

        await localStorage.SetItemAsync(SearchCustomer.LocalCustomerKey, publicCustomerId);

        return publicCustomerId;
    }

    public async Task onProductSelection(Guid productId)
    {
        var localBasket =
            await localStorage.GetItemAsync<InitializeBasketViewModel>(LocalBasketKey);

        if (localBasket == null)
        {
            var customerId =
                await GetLocalCustomer();

            await CreateLocalBasket(customerId);
        }

        var product =
            await productApplication.GetProductAsync(productId);

        var basketItem = (await basketApplication.AddItem(new AddBasketItemRequestModel()
        {
            BasketId = localBasket!.Id,
            ProductId = productId,
            ProductName = product.Name,
            Quantity = 1,
            BasePrice = 2000,
            DiscountAmount = 500,
            DiscountType = DiscountType.Price,
        })).Data;

        Basket = (await basketApplication.GetBasket(localBasket.Id)).Data;

        await InvokeAsync(StateHasChanged);
    }
}