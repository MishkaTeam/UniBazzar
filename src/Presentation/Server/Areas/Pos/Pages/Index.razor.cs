using Application.Aggregates.Orders.ViewModels;
using Domain.Aggregates.Ordering.Baskets.Enums;

namespace Server.Areas.Pos.Pages;

public partial class Index
{
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        var basket = await basketApplication.InitializeBasket(
            new InitializeBasketRequestModel()
            {
                platform = Platform.POS,
            });

        await localStorage.SetItemAsync("basket", basket.Data);
    }


    public Task onProductSelection(Guid productId)
    {
        Console.WriteLine(productId);
        return Task.CompletedTask;
    }
}