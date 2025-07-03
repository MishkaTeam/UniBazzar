using Application.Aggregates.Attribute;
using Application.Aggregates.Attribute.ViewModels;
using Application.Aggregates.Categories;
using Application.Aggregates.Categories.ViewModels;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Server.Areas.Admin.Pages.BasicInfo.Attribute;

public class UpdateModel(
    AttributeApplication attributeApplication,
    CategoriesApplication categoriesApplication) : BasePageModel
{
    [BindProperty]
    public UpdateAttributeViewModel UpdateViewModel { get; set; } = new();

    public List<MenuCategoryViewModel> Categories { get; set; } = [];


    public async Task OnGet(Guid Id)
    {
        UpdateViewModel =
            await attributeApplication.GetAttributeAsync(Id);

        Categories =
            await categoriesApplication.GetMenuCategoriesAsync();
    }

    public async Task<IActionResult> OnPost()
    {
        await attributeApplication.UpdateAsync(UpdateViewModel);
        return RedirectToPage("Index");
    }
}
