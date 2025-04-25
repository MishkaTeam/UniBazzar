using Application.Aggregates.Categories;
using Application.Aggregates.Categories.ViewModels;
using Application.Aggregates.Products;
using Application.Aggregates.Products.ProductImages.ViewModel;
using Application.Aggregates.Products.ProductPriceLists.ViewModels;
using Application.Aggregates.Products.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Persistence;
using Server.Infrastructure;

namespace Server.Pages
{
    public class IndexModel (CategoriesApplication categoriesApplication, IExecutionContextAccessor executionContextAccessor) : PageModel
    {

        public List<MenuCategoryViewModel> MenuCategory { get; set; } = [];
       

        public async Task OnGetAsync()
        {
            var sss = executionContextAccessor.UserId;
            var s = executionContextAccessor.StoreId;
            MenuCategory = await categoriesApplication.GetMenuCategoriesAsync();
        }
    }
}
