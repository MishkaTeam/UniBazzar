using Application.Aggregates.ShippingAddress;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Areas.Admin.Pages.BasicInfo.ShippingAddress
{
    public class IndexModel(ShippingAddressApplication shippingAddressApplication) : PageModel
    {
        public List<UpdateShippingAddressViewModel> ViewModel { get; set; } = [];
        public async Task OnGet()
        {
            ViewModel = await shippingAddressApplication.GetAllAddress();
        }
    }
}
