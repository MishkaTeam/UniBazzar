using Application.Aggregates.HomeViews;
using Application.Aggregates.HomeViews.ViewModels.HomeViews;
using Application.Aggregates.Products;
using Application.Aggregates.Products.ViewModels;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Server.Pages.Product;

public class ProductViewModel(
    HomeViewsApplication homeViewsApplication,
    ProductsApplication productsApplication) : BasePageModel
{
    public List<ProductCardViewModel> ProductsViewModel { get; set; } = [];
    public HomeViewViewModel HomeView { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        if (id == Guid.Empty)
        {
            return RedirectToPage("/Index");
        }

        var homeViewResult =
            await homeViewsApplication.GetHomeViewAsync(id);

        if (homeViewResult.IsSuccessful == false)
        {
            return RedirectToPage("/Index");
        }

        HomeView = homeViewResult.Data!;

        var productViews =
            (await homeViewsApplication.GetProductItems(id)).Data!;

        var productsIds =
            productViews.Select(x => x.ProductId).ToList();

        var productCardsNotOrdered = await productsApplication
            .GetFullProductData(productsIds);

        foreach (var product in productViews)
        {
            var productCard =
                productCardsNotOrdered.FirstOrDefault(x => x.Id == product.ProductId);

            if (productCard != null)
            {
                ProductsViewModel.Add(productCard);
            }
        }

        return Page();
    }
}
