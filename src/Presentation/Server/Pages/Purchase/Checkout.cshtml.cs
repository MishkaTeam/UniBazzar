using Application.Aggregates.Customers;
using Application.Aggregates.Ordering.Baskets;
using Application.Aggregates.Ordering.Baskets.ViewModels.Baskets;
using BuildingBlocks.Domain.Context;
using Constants;
using Infrastructure;
using Modules.WalletOps.Api;
using Modules.WalletOps.Application.Contracts;

namespace Server.Pages;

public class CheckoutModel(BasketApplication basketApplication, 
    CustomerApplication customerApplication, 
    IWalletApi walletApi,
    IExecutionContextAccessor executionContextAccessor) : BasePageModel
{
    public BasketViewModel Basket { get; set; } = new();
    public CustomerViewModel Customer { get; set; } = new();
    public WalletBalanceResponseContract WalletBalance { get; set; } = new();

    public async Task OnGetAsync()
    {
        var basketIdValue = Request.Cookies.FirstOrDefault(x => x.Key == BasketConstants.BASKET).Value;
        if (basketIdValue == null)
            return;

        var isBasketIdValid = Guid.TryParse(basketIdValue, out var basketId);
        var basketResult = await basketApplication.TryUpdateBasketInfo(basketId);

        if (!basketResult.IsSuccessful)
            return;

        Basket = basketResult?.Data ?? new();

        if(executionContextAccessor.UserId == null)
        {
            return;
        }

        Customer = await customerApplication.GetCustomerAsync(executionContextAccessor.UserId.Value);
        var walletResult = await walletApi.TryGetCurrentUserBalanceAsync();
        WalletBalance = walletResult?.Data ?? new();
    }
}
