using Application.Aggregates.Products;
using Application.Aggregates.Products.ProductFeatures.ViewModels;
using Application.Aggregates.Products.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Areas.Admin.Pages.BasicInfo.Products.ProductFeatures;

public class IndexModel
	(ProductsApplication productsApplication) : PageModel
{
	public ProductViewModel ProductViewModel { get; set; } = new();
	public List<ProductFeatureViewModel> ViewModel { get; set; } = [];

	public async Task<IActionResult> OnGetAsync(Guid productId)
	{
		if (productId == Guid.Empty)
		{
			return RedirectToPage("../Index");
		}

		ProductViewModel = 
			await productsApplication.GetProductAsync(productId);

		ViewModel =
			await productsApplication.GetProductFeatures(productId);

		ViewModel = ViewModel.OrderByDescending(x => x.IsPinned)
							 .ThenBy(x => x.Order)
							 .ToList();

		return Page();
	}
}