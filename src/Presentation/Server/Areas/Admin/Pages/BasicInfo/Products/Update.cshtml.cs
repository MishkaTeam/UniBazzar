using Application.Aggregates.Products;
using Application.Aggregates.Products.ViewModels;
using Application.Aggregates.Units;
using BuildingBlocks.Domain.Context;
using Domain.Aggregates.Products.Enums;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Server.Areas.Admin.Pages.BasicInfo.Products;

public class UpdateModel
	(ProductsApplication productsApplication, UnitsApplication unitsApplication, IExecutionContextAccessor execution) : BasePageModel
{
	[BindProperty]
	public UpdateProductViewModel UpdateViewModel { get; set; } = new();
	public List<SelectListItem> ProductTypeList { get; set; } = [];
	public List<SelectListItem> BaseUnitList { get; set; } = [];

	public async Task<IActionResult> OnGetAsync(Guid id)
	{
		if (id == Guid.Empty)
		{
			return RedirectToPage("../Index");
		}

		UpdateViewModel =
			await productsApplication.GetProductAsync(id);

		await FillSelectTagAsync();

		return Page();
	}

	public async Task<IActionResult> OnPostAsync()
	{
		if (ModelState.IsValid)
		{
			UpdateViewModel.CategoryId = Guid.NewGuid();
			UpdateViewModel.StoreId = execution.StoreId;

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