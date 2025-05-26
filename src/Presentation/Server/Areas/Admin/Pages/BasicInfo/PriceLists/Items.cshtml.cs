using Application.Aggregates.PriceLists.ViewModels.PriceListItem;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Areas.Admin.Pages.BasicInfo.PriceLists
{

    public class ItemsModel : PageModel
    {

        [BindProperty]
        public CreatePriceListItemViewModel CreatePriceListItem { get; set; } = new();

        public void OnGet()
        {
        }
    }
}
