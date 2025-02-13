using Application.Aggregates.ShippingAddress;
using Infrastructure;

namespace Server.Areas.Admin.Pages.BasicInfo.ShippingAddress
{
	public class IndexModel(ShippingAddressApplication shippingAddressApplication) : BasePageModel
    {
        public List<UpdateShippingAddressViewModel> ViewModel { get; set; } = [];
        public Guid CustomerId { get; set; }
        public async Task OnGet(Guid customerId)
        {
            CustomerId = customerId;
            ViewModel = await shippingAddressApplication.GetAllAddress(CustomerId);
        }
    }
}
