using Application.Aggregates.Products;
using Application.Aggregates.Products.ProductFeatures.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Areas.Admin.Pages.BasicInfo.Products.ProductFeatures;

public class DeleteModel
	(ProductsApplication productsApplication) : PageModel
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

		return Page();
	}

	public async Task<IActionResult> OnPostAsync()
	{
		await productsApplication.DeleteProductFeatureAsync(DeleteViewModel.Id);

		return RedirectToPage("Index",
			new { id = DeleteViewModel.ProductId.ToString() });
	}
}