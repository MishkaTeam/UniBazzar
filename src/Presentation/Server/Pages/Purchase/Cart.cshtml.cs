using Application.Aggregates.Ordering.Baskets;
using Application.Aggregates.Ordering.Baskets.ViewModels.Baskets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Pages
{
    public class CartModel(BasketApplication basketApplication) : PageModel
    {

        public BasketViewModel Basket { get; set; } = new();

        public async Task OnGetAsync()
        {
            await GetBasketAsync();
        }

        private async Task GetBasketAsync()
        {
            var basketCookie = Request.Cookies.FirstOrDefault(x => x.Key == "basketId");
            if (basketCookie.Value != null)
            {
                var parseResult = Guid.TryParse(basketCookie.Value, out var basketId);
                if (parseResult)
                {
                    Basket = (await basketApplication.GetBasket(basketId)).Data ?? new();
                }
            }
        }
    }
}
