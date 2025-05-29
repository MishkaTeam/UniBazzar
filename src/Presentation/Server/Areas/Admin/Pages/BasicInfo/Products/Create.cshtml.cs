using Application.Aggregates.Categories;
using Application.Aggregates.Categories.ViewModels;
using Application.Aggregates.Products;
using Application.Aggregates.Products.ViewModels;
using Application.Aggregates.Units;
using Domain.Aggregates.Products.Enums;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Persistence;

namespace Server.Areas.Admin.Pages.BasicInfo.Products;

public class CreateModel
	(ProductsApplication productsApplication,
	 UnitsApplication unitsApplication,
	 IExecutionContextAccessor execution,
	 CategoriesApplication categoriesApplication) : BasePageModel
{
	[BindProperty]
	public CreateProductViewModel CreateViewModel { get; set; } = new();
	public List<SelectListItem> ProductTypeList { get; set; } = [];
	public List<SelectListItem> BaseUnitList { get; set; } = [];

	public List<MenuCategoryViewModel> Categories { get; set; } = [];

	public async Task OnGetAsync()
	{
		Categories = await categoriesApplication.GetMenuCategoriesAsync();
        await FillSelectTagAsync();
	}

	public async Task<IActionResult> OnPostAsync()
	{
		if (ModelState.IsValid)
		{
			CreateViewModel.StoreId = execution.StoreId;
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