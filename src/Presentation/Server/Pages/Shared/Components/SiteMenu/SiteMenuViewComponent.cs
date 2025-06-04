using Application.Aggregates.Categories;
using Application.Aggregates.Categories.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Server.Pages.Shared.Components.SiteMenu
{
    public class SiteMenuViewComponent(CategoriesApplication categoriesApplication) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var menuCategories = await categoriesApplication.GetMenuCategoriesAsync();

            return View("Default", menuCategories);
        }

    }
}
