using Application.Aggregates.Discounts;
using Application.Aggregates.Units.ViewModels;
using Application.Aggregates.Units;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Application.Aggregates.Discounts.ViewModels;

namespace Server.Areas.Admin.Pages.BasicInfo.Discounts;

public class IndexModel(DiscountApplication application) : BasePageModel
{
    public List<DiscountViewModel> ViewModel { get; set; } = [];

    public async Task OnGet()
    {
        ViewModel = await application.GetAllDiscount();
    }
}
