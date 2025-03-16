using Application.Aggregates.Categories;
using Application.Aggregates.Categories.ViewModels;
using Infrastructure;

namespace Server.Areas.Admin.Pages.BasicInfo.Categories;

public class IndexModel
	(CategoriesApplication categoriesApplication) : BasePageModel
{
	public List<CategoryViewModel> ViewModel { get; set; } = [];
	public CategoryViewModel? ParentViewModel { get; set; } = new();

	public async Task OnGetAsync(Guid? parentId)
	{
		if (parentId.HasValue == false || parentId == null)
		{

			var res = await categoriesApplication.GetMenuCategoriesAsync();
			ViewModel =
				await categoriesApplication.GetRootCategoriesAsync();

			ParentViewModel = null;
		}
		else
		{
			ViewModel =
				await categoriesApplication.GetSubCategoriesAsync(parentId.Value);

			ParentViewModel =
				await categoriesApplication.GetCategoryAsync(parentId.Value);
		}
	}
}