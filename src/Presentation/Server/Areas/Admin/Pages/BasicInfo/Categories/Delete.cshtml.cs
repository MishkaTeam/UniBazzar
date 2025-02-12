using Application.Aggregates.Categories;
using Application.Aggregates.Categories.ViewModels;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Server.Areas.Admin.Pages.BasicInfo.Categories;

public class DeleteModel
	(CategoriesApplication categoriesApplication) : BasePageModel
{
	[BindProperty]
	public CategoryViewModel DeleteViewModel { get; set; } = new();

	public async Task<IActionResult> OnGetAsync(Guid id)
	{
		if (id == Guid.Empty)
		{
			return RedirectToPage("Index");
		}

		DeleteViewModel =
			await categoriesApplication.GetCategoryAsync(id);

		if (DeleteViewModel == null)
		{
			return RedirectToPage("Index");
		}

		return Page();
	}

	public async Task<IActionResult> OnPostAsync()
	{
		await categoriesApplication.DeleteCategoryAsync(DeleteViewModel.Id);

		return RedirectToPage("Index",
			new { parentId = DeleteViewModel.ParentId.ToString() });
	}
}