using Application.Aggregates.Categories;
using Application.Aggregates.Categories.ViewModels;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Areas.Admin.Pages.BasicInfo.Categories;

public class CreateModel
	(CategoriesApplication categoriesApplication) : BasePageModel
{
	[BindProperty]
	public CreateCategoryViewModel CreateViewModel { get; set; } = new();

	public void OnGet(Guid? parentId)
	{
		if (parentId.HasValue == false || parentId == null)
		{
			CreateViewModel.ParentId = null;
		}
		else
		{
			CreateViewModel.ParentId = parentId;
		}
	}

	public async Task<IActionResult> OnPostAsync()
	{
		if (ModelState.IsValid)
		{
			await categoriesApplication.CreateCategoryAsync(CreateViewModel);
		}

		return RedirectToPage("Index",
			new { parentId = CreateViewModel.ParentId.ToString() });
	}
}