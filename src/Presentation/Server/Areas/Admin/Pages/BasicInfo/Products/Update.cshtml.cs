using Application.Aggregates.Products;
using Application.Aggregates.Products.ViewModels;
using Application.Aggregates.Units;
using Domain.Enumerations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Server.Areas.Admin.Pages.BasicInfo.Products;

public class UpdateModel
	(ProductsApplication productsApplication, UnitsApplication unitsApplication) : PageModel
{
	[BindProperty]
	public UpdateProductViewModel UpdateViewModel { get; set; } = new();
	public List<SelectListItem> ProductTypeList { get; set; } = [];
	public List<SelectListItem> BaseUnitList { get; set; } = [];

	public async Task OnGetAsync(Guid id)
	{
		UpdateViewModel =
			await productsApplication.GetProductAsync(id);

		await FillSelectTagAsync();
	}

	public async Task<IActionResult> OnPostAsync()
	{
		if (ModelState.IsValid)
		{
			UpdateViewModel.ActivePriceListId = Guid.NewGuid();
			UpdateViewModel.CategoryId = Guid.NewGuid();
			UpdateViewModel.StoreId = Guid.NewGuid();
			UpdateViewModel.BrandId = Guid.NewGuid();

			await productsApplication.UpdateProductAsync(UpdateViewModel);
		}
		else
		{
			await FillSelectTagAsync();
		}

		return RedirectToPage("Index");
	}

	private async Task FillSelectTagAsync()
	{
		ProductTypeList.AddRange(new List<SelectListItem>()
		{
			new SelectListItem()
			{
				Selected = false,
				Text = nameof(ProductType.Product),
				Value = ((int)ProductType.Product).ToString()
			},
			new SelectListItem()
			{
				Selected = false,
				Text = nameof(ProductType.Service),
				Value = ((int)ProductType.Service).ToString()
			}
		});

		ProductTypeList.FirstOrDefault
			(x => x.Text == UpdateViewModel.ProductType.ToString())!.Selected = true;

		//ProductTypeList = 
		//	ProductTypeList.OrderBy(x => x.Selected == false).ToList();

		var baseUnitList = 
			await unitsApplication.GetUnits();

		BaseUnitList = baseUnitList.Select(unit => new SelectListItem
		{
			Disabled = false,
			Text = unit.Title,
			Value = unit.Id.ToString()
		}).ToList();
	}
}