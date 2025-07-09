using Application.Aggregates.Ordering.Baskets.ViewModels.BasketItems;
using Application.Aggregates.Ordering.Baskets.ViewModels.Baskets;
using Application.Aggregates.Ordering.Baskets.ViewModels.InitializeBasket;
using BlazorBootstrap;
using Domain.Aggregates.Ordering.Baskets.Enums;
using Microsoft.AspNetCore.Components.Web;
using Server.Areas.Pos.Components;

namespace Server.Areas.Pos.Pages;

public partial class Index
{
    private BasketViewModel? Basket { get; set; } = new();
    private Grid<BasketItemViewModel> grid = default!;
    private CancellationTokenSource? debounceCts;

    private bool _isLoading = false;
    private long _tempAffectedQuantity = 0;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await LoadBasketAsync();

            if (Basket == null)
            {
                Basket = new();
            }

            await grid.RefreshDataAsync();

            await InvokeAsync(StateHasChanged);
        }
    }

    private bool TryEnableLoading()
    {
        if (_isLoading == true)
        {
            return false;
        }

        _isLoading = true;

        return true;
    }

    private void DisableLoading()
    {
        _isLoading = false;
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

    private async Task DeleteLocalBasket()
    {
        await sessionStorage.RemoveItemAsync(SearchCustomer.LocalBasketKey);
    }

    private async Task onProductSelection(Guid productId)
    {
        if (TryEnableLoading() == false)
        {
            return;
        }
        
        var localBasket =
            await sessionStorage.GetItemAsync<InitializeBasketViewModel>(SearchCustomer.LocalBasketKey);

        if (localBasket == null)
        {
            var customerId =
                await GetLocalCustomer();

            localBasket =
                await CreateLocalBasket(customerId);
        }

        var basket =
            (await basketApplication.GetBasket(localBasket!.Id)).Data;

        if (basket == null)
        {
            var customerId =
                await GetLocalCustomer();

            localBasket =
                await CreateLocalBasket(customerId);

            basket =
                (await basketApplication.GetBasket(localBasket!.Id)).Data;
        }

        var basketItem =
            basket?.BasketItems.FirstOrDefault(x => x.ProductId == productId);

        if (basketItem != null)
        {
            Basket = (await basketApplication
                .PlusQuantity(basket!.Id, basketItem.Id)).Data;

            await grid.RefreshDataAsync();
            await InvokeAsync(StateHasChanged);

            return;
        }

        var product =
            await productApplication.GetProductAsync(productId);

        // IMPORTANT: Test Data !!
        // Must get Price from PriceList and Discount
        var newBasket = (await basketApplication.AddItem(new AddBasketItemRequestModel()
        {
            BasketId = localBasket!.Id,
            ProductId = productId,
            ProductName = product.Name,
            Quantity = 1,
            BasePrice = 0,
            DiscountAmount = 0,
            DiscountType = DiscountType.Price,
        })).Data;

        Basket = newBasket;

        DisableLoading();

        await grid.RefreshDataAsync();
        await InvokeAsync(StateHasChanged);
    }

    private async Task RemoveBasketItem(Guid basketId, Guid basketItemId)
    {
        Basket = (await basketApplication
                .RemoveItem(basketId, basketItemId)).Data;

        await grid.RefreshDataAsync();
    }

    private async Task<GridDataProviderResult<BasketItemViewModel>>EmployeesDataProvider(GridDataProviderRequest<BasketItemViewModel> request)
    {
        if (Basket is null)
            Basket = new BasketViewModel();

        return await Task.FromResult(request.ApplyTo(Basket.BasketItems));
    }

    private async Task AffecteQuantity(Guid basketId, Guid basketItemId, int affectedQuantity)
    {
        if (debounceCts is not null)
        {
            debounceCts.Cancel();
            debounceCts.Dispose();
        }

        debounceCts = new CancellationTokenSource();
        var token = debounceCts.Token;

        _tempAffectedQuantity += affectedQuantity;

        var basketItem = Basket!.BasketItems
            .FirstOrDefault(x => x.Id == basketItemId);

        if (basketItem?.Quantity + affectedQuantity <= 0)
        {
            basketItem!.Quantity = 1;

            _tempAffectedQuantity = 0;

            return;
        }

        basketItem!.Quantity += affectedQuantity;

        await Task.Delay(500);

        if (token.IsCancellationRequested)
            return;

        Basket = (await basketApplication
            .SetAffectedQuantity(basketId, basketItemId, _tempAffectedQuantity)).Data;

        _tempAffectedQuantity = 0;

        await grid.RefreshDataAsync();
    }

    private async Task SetQuantity(Guid basketId, Guid basketItemId, long quantity)
    {
        Basket = (await basketApplication
            .SetQuantity(basketId, basketItemId, quantity)).Data;

        await grid.RefreshDataAsync();
    }

    private async Task OnEnterQuantity(KeyboardEventArgs e, Guid basketId, Guid basketItemId, long quantity)
    {
        if (e.Key == "Enter")
        {
            await SetQuantity
                (basketId, basketItemId, quantity);

            await grid.RefreshDataAsync();
        }
    }

    private async Task SetBasePrice(Guid basketId, Guid basketItemId, decimal basePrice)
    {
        Basket = (await basketApplication
            .SetBasePrice(basketId, basketItemId, basePrice)).Data;

        await grid.RefreshDataAsync();
    }

    private async Task OnEnterBasePrice(KeyboardEventArgs e, Guid basketId, Guid basketItemId, decimal basePrice)
    {
        if (e.Key == "Enter")
        {
            await SetBasePrice
                (basketId, basketItemId, basePrice);

            await grid.RefreshDataAsync();
        }
    }

    private async Task SetDiscountValue(Guid basketId, Guid basketItemId, decimal discountValue)
    {
        var basket =
            (await basketApplication.GetBasket(basketId)).Data;

        var basketItem =
            basket?.BasketItems.FirstOrDefault(x => x.Id == basketItemId);

        if (basketItem?.DiscountType == DiscountType.Price &&
            discountValue > basketItem.BasePrice)
        {
            discountValue = basketItem.BasePrice;
        }

        if (basketItem?.DiscountType == DiscountType.Percent &&
            discountValue > 100)
        {
            discountValue = 100;
        }
        else if (basketItem?.DiscountType == DiscountType.Percent &&
            discountValue <= 0)
        {
            discountValue = 1;
        }

        Basket = (await basketApplication
            .SetDiscountValue(basketId, basketItemId, discountValue)).Data;

        await grid.RefreshDataAsync();
    }

    private async Task OnEnterDiscountValue(KeyboardEventArgs e, Guid basketId, Guid basketItemId, decimal basePrice)
    {
        if (e.Key == "Enter")
        {
            await SetDiscountValue
                (basketId, basketItemId, basePrice);

            await grid.RefreshDataAsync();
        }
    }

    private async Task ChangeDiscountType(Guid basketId, Guid basketItemId)
    {
        var basket =
            (await basketApplication.GetBasket(basketId)).Data;

        var basketItem =
            basket?.BasketItems.FirstOrDefault(x => x.Id == basketItemId);

        var discountType = DiscountType.None;
        var discountValue = 0m;

        if (basketItem?.DiscountType == DiscountType.Price)
        {
            discountType =
                DiscountType.Percent;

            discountValue =
                (basketItem.DiscountValue * 100) / basketItem.BasePrice;

            if (discountValue > 100)
            {
                discountValue = 100;
            }
        }
        else if (basketItem?.DiscountType == DiscountType.Percent)
        {
            discountType =
                DiscountType.Price;

            discountValue =
                (basketItem.DiscountValue * basketItem.BasePrice) / 100;

            if (discountValue > basketItem.BasePrice)
            {
                discountValue = basketItem.BasePrice;
            }
        }

        Basket = (await basketApplication
            .UpdateDiscount(basketId, basketItemId, discountValue, discountType)).Data;

        await grid.RefreshDataAsync();
        await InvokeAsync(StateHasChanged);
    }

    private async Task SetTotalDiscountValue(Guid basketId, decimal discountValue)
    {
        var basket =
            (await basketApplication.GetBasket(basketId)).Data;

        if (basket?.TotalDiscountType == DiscountType.Price &&
            discountValue > basket.SubtotalBeforeBasketDiscount)
        {
            discountValue = basket.SubtotalBeforeBasketDiscount;
        }

        if (basket?.TotalDiscountType == DiscountType.Percent &&
            discountValue > 100)
        {
            discountValue = 100;
        }
        else if (basket?.TotalDiscountType == DiscountType.Percent &&
            discountValue < 0)
        {
            discountValue = 1;
        }

        Basket = (await basketApplication
            .UpdateTotalDiscount(basketId, discountValue, Basket!.TotalDiscountType)).Data;

        await grid.RefreshDataAsync();
    }

    private async Task OnEnterTotalDiscountValue(KeyboardEventArgs e, Guid basketId, decimal basePrice)
    {
        if (e.Key == "Enter")
        {
            await SetTotalDiscountValue
                (basketId, basePrice);

            await grid.RefreshDataAsync();
        }
    }

    private async Task ChangeTotalDiscountType(Guid basketId)
    {
        var basket =
            (await basketApplication.GetBasket(basketId)).Data;

        var discountType = DiscountType.None;
        var discountValue = 0m;

        if (basket?.TotalDiscountType == DiscountType.Price)
        {
            discountType =
                DiscountType.Percent;

            discountValue =
                (basket.TotalDiscountAmount * 100) / basket.SubtotalBeforeBasketDiscount;

            if (discountValue > 100)
            {
                discountValue = 100;
            }
        }
        else if (basket?.TotalDiscountType == DiscountType.Percent)
        {
            discountType =
                DiscountType.Price;

            discountValue =
                (basket.TotalDiscountAmount * basket.SubtotalBeforeBasketDiscount) / 100;

            if (discountValue > basket.SubtotalBeforeBasketDiscount)
            {
                discountValue = basket.SubtotalBeforeBasketDiscount;
            }
        }

        Basket = (await basketApplication
            .UpdateTotalDiscount(basketId, discountValue, discountType)).Data;

        await grid.RefreshDataAsync();
        await InvokeAsync(StateHasChanged);
    }

    private async Task SetDescription(Guid basketId, string description)
    {
        Basket = (await basketApplication
            .UpdateDescription(basketId, description)).Data;

        await grid.RefreshDataAsync();
        await InvokeAsync(StateHasChanged);
    }

    private async Task Checkout(Guid basketId)
    {
        if (basketId == Guid.Empty)
        {
            return;
        }

        var result =
            await basketApplication.CheckoutBasket(basketId);

        if (result.IsSuccessful == false)
        {
            return;
        }

        // Need to complete Treasury module !!!

        //var processOrder =
        //    new ProcessOrderRequestModel() { BasketId = basketId };

        //var cancellationToken =
        //    new CancellationToken();

        //await orderApplication.ProcessOrderRequest(processOrder, cancellationToken);

        await DeleteLocalBasket();

        Basket = new();

        await grid.RefreshDataAsync();
        await InvokeAsync(StateHasChanged);
    }
}