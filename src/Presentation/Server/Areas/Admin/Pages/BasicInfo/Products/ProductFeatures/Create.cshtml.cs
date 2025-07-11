using Application.Aggregates.Products.ProductFeatures;
using Application.Aggregates.Products.ProductFeatures.ViewModels;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Server.Areas.Admin.Pages.BasicInfo.Products.ProductFeatures;

public class CreateModel
	(ProductFeaturesApplication productsApplication) : BasePageModel
{
	[BindProperty]
	public CreateProductFeatureViewModel CreateViewModel { get; set; } = new();

	public IActionResult OnGet(Guid productId)
	{
		if (productId == Guid.Empty)
		{
			return RedirectToPage("../Index");
		}

		CreateViewModel.ProductId = productId;

		return Page();
	}

	public async Task<IActionResult> OnPostAsync()
	{
		if (ModelState.IsValid)
		{
			await productsApplication.CreateProductFeatureAsync(CreateViewModel);
		}

		return RedirectToPage("Index",
			new { productId = CreateViewModel.ProductId.ToString() });
	}
}