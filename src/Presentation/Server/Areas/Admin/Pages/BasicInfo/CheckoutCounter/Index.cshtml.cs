using Application.Aggregates.CheckoutCounter.ViewModels;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Areas.Admin.Pages.BasicInfo.CheckoutCounter
{
    public class IndexModel(Application.Aggregates.CheckoutCounter.CheckoutApplications checkoutCounter) : BasePageModel
    {
        public List<CheckoutCounterViewModels> ViewModels { get; set; } = [];
        public async Task OnGet()
        {
            ViewModels=await checkoutCounter.GetCheckoutCounters();
        }
    }
}
