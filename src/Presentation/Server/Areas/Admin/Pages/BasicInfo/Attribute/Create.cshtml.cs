using Application.Aggregates.Attribute;
using Application.Aggregates.Attribute.ViewModels;
using Application.Aggregates.Categories;
using Application.Aggregates.Categories.ViewModels;
using Application.Aggregates.Units;
using BuildingBlocks.Persistence;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Server.Areas.Admin.Pages.BasicInfo.Attribute;

public class CreateModel(
    AttributeApplication attributeApplication,
    CategoriesApplication categoriesApplication ) : BasePageModel
{

    [BindProperty]
    public CreateAttributeViewModel CreateViewModel { get; set; } = new();
    public List<MenuCategoryViewModel> Categories { get; set; } = [];

    public async Task OnGet()
    {
        Categories =
            await categoriesApplication.GetMenuCategoriesAsync();
    }
    public async Task<IActionResult> OnPost()
    {

        if (ModelState.IsValid)
        {
            await attributeApplication.CreateAsync(CreateViewModel);
        }
        else
        {
            Categories = await categoriesApplication.GetMenuCategoriesAsync();
        }

        return RedirectToPage("Index");
    }
}
