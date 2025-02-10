using Application.Aggregates.Products;
using Application.Aggregates.Products.ViewModels;
using Application.Aggregates.Units;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Server.Areas.Admin.Pages.BasicInfo.Products;

public class DeleteModel
	(ProductsApplication productsApplication, UnitsApplication unitsApplication) : BasePageModel
{
	[BindProperty]
	public ProductViewModel DeleteViewModel { get; set; } = new();
	public List<SelectListItem> ProductTypeList { get; set; } = [];
	public List<SelectListItem> BaseUnitList { get; set; } = [];

	public async Task<IActionResult> OnGetAsync(Guid id)
	{
		if (id == Guid.Empty)
		{
			return RedirectToPage("../Index");
		}

		DeleteViewModel = 
			await productsApplication.GetProductAsync(id);

		await FillSelectTagAsync();

		return Page();
	}

	public async Task<IActionResult> OnPostAsync()
	{
		if (DeleteViewModel.Id == Guid.Empty)
		{
			AddToastError
				(message: Resources.Messages.Errors.IdIsNull);

			return RedirectToPage(pageName: "Index");

		}
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