using Application.Aggregates.Products;
using Application.Aggregates.Products.ViewModels;
using Application.Aggregates.Units;
using Domain.Enumerations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Server.Areas.Admin.Pages.BasicInfo.Products;

public class CreateModel(ProductsApplication productsApplication, UnitsApplication unitsApplication) : PageModel
{
	[BindProperty]
	public CreateProductViewModel CreateViewModel { get; set; } = new();
	public List<SelectListItem> ProductTypeList { get; set; } = [];
	public List<SelectListItem> BaseUnitList { get; set; } = [];

	public async Task OnGetAsync()
	{
		await FillSelectTagAsync();
	}

	public async Task<IActionResult> OnPostAsync()
	{
		if (ModelState.IsValid)
		{
			CreateViewModel.ActivePriceListId = Guid.NewGuid();
			CreateViewModel.CategoryId = Guid.NewGuid();
			CreateViewModel.StoreId = Guid.NewGuid();
			CreateViewModel.BrandId = Guid.NewGuid();

			await productsApplication.CreateProductAsync(CreateViewModel);
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
				Selected = true,
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