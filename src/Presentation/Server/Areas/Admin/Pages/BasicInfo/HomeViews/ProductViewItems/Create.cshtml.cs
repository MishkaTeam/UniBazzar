using Application.Aggregates.HomeViews;
using Application.Aggregates.HomeViews.ViewModels.ProductViewItems;
using Application.Aggregates.Products;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Server.Areas.Admin.Pages.BasicInfo.HomeViews.ProductViewItems;

public class CreateModel(
    HomeViewsApplication homeViewsApplication,
    ProductsApplication productsApplication) : BasePageModel
{
    [BindProperty]
    public CreateProductViewItemViewModel CreateViewModel { get; set; } = new();
    public List<SelectListItem> ProductList { get; set; } = new();

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
            await homeViewsApplication.AddProductItem(CreateViewModel);

        if (result.IsSuccessful == false)
        {
            AddPageError
                (result.ErrorMessage!.Message);

            await FillSelectTag();

            return Page();
        }

        return RedirectToPage("Index",
            new { homeViewId = CreateViewModel.HomeViewId.ToString() });
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
