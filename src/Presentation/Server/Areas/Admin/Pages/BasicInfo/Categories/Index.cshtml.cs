using Application.Aggregates.Categories;
using Application.Aggregates.Categories.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Areas.Admin.Pages.BasicInfo.Categories;

public class IndexModel
	(CategoriesApplication categoriesApplication) : PageModel
{
	[BindProperty]
	public List<CategoryViewModel> ViewModel { get; set; } = [];

	public async Task OnGetAsync(Guid? parentId)
	{
		if (parentId.HasValue == false || parentId == null)
		{
			ViewModel =
				await categoriesApplication.GetCategoriesAsync();
		}
		else
		{
			ViewModel =
				await categoriesApplication.GetSubCategoriesAsync(parentId.Value);
		}
	}
}