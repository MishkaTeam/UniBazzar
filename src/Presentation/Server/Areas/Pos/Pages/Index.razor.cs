using System.Data;
using Application.Aggregates.Orders.ViewModels;
using Domain.Aggregates.Ordering.Baskets.Enums;

namespace Server.Areas.Pos.Pages;

public partial class Index
{
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        var preBasket = await localStorage.GetItemAsync<InitializeBasketViewModel>("basket");

        if (preBasket == null)
        {
            preBasket = (await basketApplication.InitializeBasket(
                new InitializeBasketRequestModel()
                {
                    platform = Platform.POS,
                })).Data;

            await localStorage.SetItemAsync("basket", preBasket);
        }

        Console.WriteLine(preBasket?.ReferenceNumber);

        var basket = await basketApplication.GetByReferenceNumber(preBasket!.ReferenceNumber);


    }


    public Task onProductSelection(Guid productId)
    {
        Console.WriteLine(productId);
        return Task.CompletedTask;
    }
}