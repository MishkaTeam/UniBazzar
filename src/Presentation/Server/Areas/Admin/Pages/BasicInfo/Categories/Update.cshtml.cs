using Application.Aggregates.Categories;
using Application.Aggregates.Categories.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Areas.Admin.Pages.BasicInfo.Categories;

public class UpdateModel
	(CategoriesApplication categoriesApplication) : PageModel
{
	[BindProperty]
	public CategoryViewModel UpdateViewModel { get; set; } = new();

	public async Task<IActionResult> OnGetAsync(Guid id)
	{
		if (id == Guid.Empty)
		{
			return RedirectToPage("Index");
		}

		UpdateViewModel =
			await categoriesApplication.GetCategoryAsync(id);

		if (UpdateViewModel == null)
		{
			return RedirectToPage("Index");
		}

		return Page();
	}

	public async Task<IActionResult> OnPostAsync()
	{
		if (ModelState.IsValid)
		{
			await categoriesApplication.UpdateCategoryAsync(UpdateViewModel);
		}

		return RedirectToPage("Index",
			new { parentId = UpdateViewModel.ParentId });
	}
}