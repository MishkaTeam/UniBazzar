using Application.Aggregates.Customers;
using Infrastructure;

namespace Server.Areas.Admin.Pages.BasicInfo.Customers
{
	public class IndexModel(CustomerApplication customerApplication) : BasePageModel
    {
        public List<UpdateCustomerViewModel> ViewModel { get; set; } = [];
        public async Task OnGet()
        {
            ViewModel = await customerApplication.GetAllCustomer();
        }
    }
}
