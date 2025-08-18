using Application.Aggregates.HomeViews;
using Application.Aggregates.HomeViews.ViewModels.ProductViewItems;
using Application.Aggregates.Products;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Server.Areas.Admin.Pages.BasicInfo.HomeViews.ProductViewItems;

public class UpdateModel(
    HomeViewsApplication homeViewsApplication,
    ProductsApplication productsApplication) : BasePageModel
{
    [BindProperty]
    public UpdateProductViewItemViewModel UpdateViewModel { get; set; } = new();
    public List<SelectListItem> ProductList { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(Guid homeViewId, Guid productItemId)
    {
        if (homeViewId == Guid.Empty)
        {
            return RedirectToPage("../Index");
        }

        if (productItemId == Guid.Empty)
        {
            return RedirectToPage("Index",
                new { homeViewId = homeViewId.ToString() });
        }

        var homeViewResult =
            await homeViewsApplication.GetHomeViewAsync(homeViewId);

        if (homeViewResult.IsSuccessful == false)
        {
            return RedirectToPage("../Index");
        }

        var productItemResult =
            await homeViewsApplication.GetProductItem(homeViewId, productItemId);

        if (productItemResult.IsSuccessful == false)
        {
            return RedirectToPage("Index",
                new { homeViewId = homeViewId.ToString() });
        }

        UpdateViewModel =
            productItemResult.Data!;

        await FillSelectTag();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await FillSelectTag();

            return Page();
        }

        var result =
            await homeViewsApplication.UpdateProductItem(UpdateViewModel);

        if (result.IsSuccessful == false)
        {
            await FillSelectTag();

            AddPageError
                (result.ErrorMessage!.Message);

            return Page();
        }

        return RedirectToPage("Index",
            new { homeViewId = UpdateViewModel.HomeViewId.ToString() });
    }


    private async Task FillSelectTag()
    {
        var products =
            await productsApplication.GetProducts();

        foreach (var product in products)
        {
            ProductList.Add(new SelectListItem()
            {
                Text = product.Name,
                Value = product.Id.ToString(),
            });
        }
    }
}
