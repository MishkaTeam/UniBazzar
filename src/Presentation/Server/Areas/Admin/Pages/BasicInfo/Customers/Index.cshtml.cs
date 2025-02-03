using Microsoft.AspNetCore.Mvc.RazorPages;
using Application.Aggregates.Customer;

namespace Server.Areas.Admin.Pages.BasicInfo.Customers
{
    public class IndexModel(CustomerApplication customerApplication) : PageModel
    {
        public List<CustomerViewModel> ViewModel { get; set; } = [];
        public async Task OnGet()
        {
            ViewModel = await customerApplication.GetAllCustomer();
        }
    }
}
