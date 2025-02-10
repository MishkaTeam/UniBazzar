using Application.Aggregates.ShippingAddress;
using Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Areas.Admin.Pages.BasicInfo.ShippingAddress
{
    public class IndexModel(ShippingAddressApplication shippingAddressApplication) : BasePageModel
    {
        public List<UpdateShippingAddressViewModel> ViewModel { get; set; } = [];
        public async Task OnGet()
        {
            ViewModel = await shippingAddressApplication.GetAllAddress();
        }
    }
}
