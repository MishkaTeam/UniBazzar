using Application.Aggregates.Categories;
using Application.Aggregates.Products.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Server.Pages.Shared.Components.ProductCard;

public class ProductCardViewComponent : ViewComponent
{
    public static string KEY = "ProductCard";
    public IViewComponentResult Invoke(ProductCardViewModel model)
    {

        return View("Default", model);
    }
}
