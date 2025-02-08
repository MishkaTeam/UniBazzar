using Application.Aggregates.Products;
using Application.Aggregates.Products.ProductFeatures.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Areas.Admin.Pages.BasicInfo.Products.ProductFeatures;

public class UpdateModel
	(ProductsApplication productsApplication) : PageModel
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