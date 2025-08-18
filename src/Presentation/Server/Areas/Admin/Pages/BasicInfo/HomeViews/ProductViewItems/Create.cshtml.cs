using Application.Aggregates.HomeViews;
using Application.Aggregates.HomeViews.ViewModels.ProductViewItems;
using Application.Aggregates.Products;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Server.Areas.Admin.Pages.BasicInfo.HomeViews.ProductViewItems;

public class CreateModel
    (HomeViewsApplication homeViewsApplication) : BasePageModel
{
    [BindProperty]
    public CreateProductViewItemViewModel CreateViewModel { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(Guid homeViewId)
    {
        if (homeViewId == Guid.Empty)
        {
            return RedirectToPage("../Index");
        }

        var result =
            await homeViewsApplication.GetHomeViewAsync(homeViewId);

        if (result.IsSuccessful == false)
        {
            return RedirectToPage("../Index");
        }

        CreateViewModel.HomeViewId = homeViewId;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var result =
            await homeViewsApplication.AddProductItem(CreateViewModel);

        if (result.IsSuccessful == false)
        {
            AddPageError
                (result.ErrorMessage!.Message);

            return Page();
        }

        return RedirectToPage("Index",
            new { homeViewId = CreateViewModel.HomeViewId.ToString() });
    }
}
