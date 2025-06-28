using Application.Aggregates.Ordering.Baskets.ViewModels.Baskets;
using Application.Aggregates.Orders.ViewModels;
using Application.Aggregates.Orders.ViewModels.BasketItems;
using Domain.Aggregates.Ordering.Baskets.Enums;
using Server.Areas.Pos.Components;

namespace Server.Areas.Pos.Pages;

public partial class Index
{
    private BasketViewModel? Basket { get; set; }


    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await LoadBasketAsync();

            if (Basket == null)
            {
                Basket = new();
            }

            await InvokeAsync(StateHasChanged);
        }
    }

    private async Task LoadBasketAsync()
    { 
        var localBasket =
            await sessionStorage.GetItemAsync<InitializeBasketViewModel>(SearchCustomer.LocalBasketKey);

        if (localBasket == null)
        {
            return;
        }

        var basketIsExist =
            (await basketApplication.Exist(localBasket.Id)).Data;

        if (basketIsExist == false)
        {
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

        await sessionStorage.SetItemAsync(SearchCustomer.LocalBasketKey, newBasket);

        return newBasket!;
    }

    private async Task<Guid> GetLocalCustomer()
    {
        var customerId =
            await sessionStorage.GetItemAsync<Guid?>(SearchCustomer.LocalCustomerKey);

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

        await sessionStorage.SetItemAsync(SearchCustomer.LocalCustomerKey, publicCustomerId);

        return publicCustomerId;
    }

    private async Task onProductSelection(Guid productId)
    {
        var localBasket =
            await sessionStorage.GetItemAsync<InitializeBasketViewModel>(SearchCustomer.LocalBasketKey);

        if (localBasket == null)
        {
            var customerId =
                await GetLocalCustomer();

            localBasket =
                await CreateLocalBasket(customerId);
        }

        var basketIsExist =
            (await basketApplication.Exist(localBasket!.Id)).Data;

        if (basketIsExist == false)
        {
            var customerId =
                await GetLocalCustomer();

            localBasket =
                await CreateLocalBasket(customerId);
        }

        var product =
            await productApplication.GetProductAsync(productId);

        // IMPORTANT: Test Data !!
        // Must get Price from PriceList and Discount
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