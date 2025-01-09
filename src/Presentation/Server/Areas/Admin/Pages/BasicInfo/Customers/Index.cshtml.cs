using Application.Aggregates.Units.ViewModels;
using Application.Aggregates.Units;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Application.Aggregates.Customer;

namespace Server.Areas.Admin.Pages.BasicInfo.Customers
{
    public class IndexModel(CustomerApplication customerApplication) : PageModel
    {
        //public List<UnitViewModel> ViewModel { get; set; } = [];
        //public async Task OnGet()
        //{
        //    ViewModel = await customerApplication.GetUnits();
        //}
    }
}
