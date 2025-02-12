using Application.Aggregates.Products;
using Application.Aggregates.Products.ProductFeatures.ViewModels;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Server.Areas.Admin.Pages.BasicInfo.Products.ProductFeatures;

public class DeleteModel
	(ProductsApplication productsApplication) : BasePageModel
{
	[BindProperty]
	public ProductFeatureViewModel DeleteViewModel { get; set; } = new();

	public async Task<IActionResult> OnGetAsync(Guid id)
	{
		if (id == Guid.Empty)
		{
			return RedirectToPage("../Index");
		}

		DeleteViewModel =
			await productsApplication.GetProductFeatureAsync(id);

		if (DeleteViewModel == null)
		{
			return RedirectToPage("../Index");
		}

		return Page();
	}

	public async Task<IActionResult> OnPostAsync()
	{
		await productsApplication.DeleteProductFeatureAsync(DeleteViewModel.Id);

		return RedirectToPage("Index",
			new { productId = DeleteViewModel.ProductId.ToString() });
	}
}