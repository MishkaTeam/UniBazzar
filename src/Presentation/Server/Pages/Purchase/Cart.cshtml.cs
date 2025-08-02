using System.Data;
using System.Reflection.Metadata.Ecma335;
using Application.Aggregates.Ordering.Baskets;
using Application.Aggregates.Ordering.Baskets.ViewModels.Baskets;
using Framework.DataType;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Pages
{
    [IgnoreAntiforgeryToken]
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

        public async Task<IActionResult> OnPostUpdateQuantityAsync([FromBody] UpdateQuantityRequestModel request)
        {
            if(request == null)
                return BadRequest();

            if (request.Quantity < 1)
                return BadRequest(new { message = "Invalid quantity" });

            if(string.IsNullOrEmpty(request.BasketItemId) && string.IsNullOrEmpty(request.BasketId))
                return BadRequest(new { message = "Invalid BasketId" });

            var res = await basketApplication.SetQuantity(Guid.Parse(request.BasketId), Guid.Parse(request.BasketItemId), request.Quantity);

            if (res == null || res.IsSuccessful == false)
                return new JsonResult(new { IsSuccessful = false, ErrorMessage = res?.ErrorMessage?.Message }); ;

            var quantity = res?.Data?.BasketItems.FirstOrDefault(x => x.Id == Guid.Parse(request.BasketItemId))?.Quantity ?? 1;

            return new JsonResult(res);
        }
    }
    public class UpdateQuantityRequestModel
    {
        public int Quantity { get; set; }
        public string BasketId { get; set; }
        public string BasketItemId { get; set; }

    }
}
