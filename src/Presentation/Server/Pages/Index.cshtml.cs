using Application.Aggregates.Categories;
using Application.Aggregates.Products.ProductImages.ViewModel;
using Application.Aggregates.Products.ProductPriceLists.ViewModels;
using Application.Aggregates.Products.ViewModels;
using Application.Aggregates.Products;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Application.Aggregates.Categories.ViewModels;

namespace Server.Pages
{
    public class IndexModel (CategoriesApplication categoriesApplication ) : PageModel
    {

        public List<MenuCategoryViewModel> MenuCategory { get; set; } = [];
       

        public async Task OnGetAsync()
        {
            MenuCategory = await categoriesApplication.GetMenuCategoriesAsync();
        }
    }
}
