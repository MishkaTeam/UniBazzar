using Application.Aggregates.Categories;
using Application.Aggregates.Categories.ViewModels;
using Application.Aggregates.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Areas.Admin.Pages.BasicInfo.Categories;

public class DeleteModel
	(CategoriesApplication categoriesApplication) : PageModel
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