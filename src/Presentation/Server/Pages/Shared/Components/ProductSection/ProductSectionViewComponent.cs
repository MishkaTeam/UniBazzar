using Application.Aggregates.HomeViews.ViewModels.HomeViews;
using Application.Aggregates.Products;
using Application.Aggregates.Products.ViewModels;
using Domain.Aggregates.Cms.HomeViews.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Server.Pages.Shared.Components.ProductSection;

public class ProductSectionViewComponent
    (ProductsApplication productsApplication) : ViewComponent
{
    public static string KEY = "ProductSection";

    public async Task<IViewComponentResult> InvokeAsync
        (IndexHomeViewViewModel homeView)
    {
        if (homeView.IsActive == false ||
            homeView.Type != ViewType.Product)
        {
            return null!;
        }

        homeView.ProductViews =
            homeView.ProductViews.OrderBy(x => x.Ordering).ToList();

        var productsIds =
            homeView.ProductViews.Select(x => x.ProductId).ToList();

        var productCardsNotOrdered = await productsApplication
            .GetFullProductData(productsIds);

        var orderedProductCards = new List<ProductCardViewModel>();

        foreach (var product in homeView.ProductViews)
        {
            var productCard =
                productCardsNotOrdered.FirstOrDefault(x => x.Id == product.ProductId);

            if (productCard != null)
            {
                orderedProductCards.Add(productCard);
            }
        }

        return View("Default", (homeView, orderedProductCards));
    }
}
