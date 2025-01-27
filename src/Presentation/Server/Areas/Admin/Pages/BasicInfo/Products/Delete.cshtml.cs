using Application.Aggregates.Products;
using Application.Aggregates.Products.ViewModels;
using Application.Aggregates.Units;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Server.Areas.Admin.Pages.BasicInfo.Products;

public class DeleteModel
	(ProductsApplication productsApplication, UnitsApplication unitsApplication) : PageModel
{
	[BindProperty]
	public ProductViewModel DeleteViewModel { get; set; } = new();
	public List<SelectListItem> ProductTypeList { get; set; } = [];
	public List<SelectListItem> BaseUnitList { get; set; } = [];

	public async Task OnGetAsync(Guid id)
	{
		DeleteViewModel = 
			await productsApplication.GetProductAsync(id);

		await FillSelectTagAsync();
	}

	public async Task<IActionResult> OnPostAsync()
	{
		await productsApplication.DeleteProductAsync(DeleteViewModel.Id);

		return RedirectToPage("Index");
	}

	private async Task FillSelectTagAsync()
	{
		ProductTypeList.Add(new SelectListItem()
		{
			Selected = true,
			Text = DeleteViewModel.ProductType.ToString(),
			Value = ((int)DeleteViewModel.ProductType).ToString()
		});

		var baseUnit = 
			await unitsApplication.GetUnitAsync(DeleteViewModel.UnitId);

		BaseUnitList.Add(new SelectListItem()
		{
			Disabled = false,
			Text = baseUnit.Title,
			Value = baseUnit.Id.ToString(),
		});
	}
}