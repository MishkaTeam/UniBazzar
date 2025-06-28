using Application.Aggregates.Products.ProductFeatures;
using Application.Aggregates.Products.ProductFeatures.ViewModels;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Server.Areas.Admin.Pages.BasicInfo.Products.ProductFeatures;

public class UpdateModel
	(ProductFeaturesApplication productsApplication) : BasePageModel
{
	[BindProperty]
	public ProductFeatureViewModel UpdateViewModel { get; set; } = new();

	public async Task<IActionResult> OnGetAsync(Guid id)
	{
		if (id == Guid.Empty)
		{
			return RedirectToPage("../Index");
		}

		UpdateViewModel =
			await productsApplication.GetProductFeatureAsync(id);

		if (UpdateViewModel == null)
		{
			return RedirectToPage("../Index");
		}

		return Page();
	}

	public async Task<IActionResult> OnPostAsync()
	{
		if (ModelState.IsValid)
		{
			await productsApplication.UpdateProductFeatureAsync(UpdateViewModel);
		}

		return RedirectToPage("Index",
			new { productId = UpdateViewModel.ProductId });
	}
}