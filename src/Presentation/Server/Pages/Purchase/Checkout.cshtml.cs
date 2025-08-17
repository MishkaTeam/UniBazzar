using Application.Aggregates.Customers;
using Application.Aggregates.Ordering.Baskets;
using Application.Aggregates.Ordering.Baskets.ViewModels.Baskets;
using Application.Aggregates.Ordering.Orders;
using Application.Aggregates.Ordering.Orders.ProcessOrder;
using BuildingBlocks.Domain.Context;
using Constants;
using Framework.DataType;
using Infrastructure;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Modules.WalletOps.Api;
using Modules.WalletOps.Application.Contracts;

namespace Server.Pages;

public class CheckoutModel(BasketApplication basketApplication, 
    OrderApplication orderApplication,
    CustomerApplication customerApplication, 
    IWalletApi walletApi,
    IExecutionContextAccessor executionContextAccessor) : BasePageModel
{
    public BasketViewModel Basket { get; set; } = new();
    public CustomerViewModel Customer { get; set; } = new();
    public WalletBalanceResponseContract WalletBalance { get; set; } = new();

    public async Task OnGetAsync()
    {
        bool isSuccessful = await TryGetCheckoutData();
        if (!isSuccessful)
        {
            return;
        }
    }

    public async Task<IActionResult> OnPostCheckOutAsync(CancellationToken cancellationToken)
    {

        bool isSuccessful = await TryGetCheckoutData();
        if (!isSuccessful)
        {
            return Page() ;
        }

        var orderProcessResult = await orderApplication.ProcessOrderRequest(new ProcessOrderRequestModel {  BasketId = Basket.Id }, cancellationToken);

        if(!orderProcessResult.IsSuccessful)
        {
            return Page();
        }
        var order = await orderApplication.GetOrderAsync(orderProcessResult.Data?.OrderId ?? Guid.Empty);

        if (!order.IsSuccessful || order.Data == null || order.Data.Id == Guid.Empty)
        {
            return Page();
        }

        var walletResult = await walletApi.PurchaseAsync(new WalletPurchaseRequestContract
        {
            Amount = new MoneyContract { Amount = order.Data.OrderTotal, Currency = "IRR" },
            PurchaseType = PurchaseType.Credit,
            ReferenceId = order.Data.Id
        });


        if (!walletResult.IsSuccessful || walletResult.Data == null)
            return Page();

        if (walletResult.Data.ShouldRedirect)
            return Redirect(walletResult.Data.RedirectLink);

        return RedirectToPagePermanent("/Thankyou");
    }

    private async Task<bool> TryGetCheckoutData()
    {
        var basketIdValue = Request.Cookies.FirstOrDefault(x => x.Key == BasketConstants.BASKET).Value;
        if (basketIdValue == null)
            return false;

        var isBasketIdValid = Guid.TryParse(basketIdValue, out var basketId);
        var basketResult = await basketApplication.TryUpdateBasketInfo(basketId);

        if (!basketResult.IsSuccessful)
            return false;

        Basket = basketResult?.Data ?? new();

        if (executionContextAccessor.UserId == null)
        {
            return false;
        }

        Customer = await customerApplication.GetCustomerAsync(executionContextAccessor.UserId.Value);
        var walletResult = await walletApi.TryGetCurrentUserBalanceAsync();
        WalletBalance = walletResult?.Data ?? new();
        return true;
    }

}
