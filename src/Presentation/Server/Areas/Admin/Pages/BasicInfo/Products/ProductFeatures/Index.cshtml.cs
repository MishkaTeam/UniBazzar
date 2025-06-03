using Application.Aggregates.Products;
using Application.Aggregates.Products.ProductFeatures;
using Application.Aggregates.Products.ProductFeatures.ViewModels;
using Application.Aggregates.Products.ViewModels;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Server.Areas.Admin.Pages.BasicInfo.Products.ProductFeatures;

public class IndexModel
	(ProductsApplication productsApplication, ProductFeaturesApplication productFeaturesApplication) : BasePageModel
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
			await productFeaturesApplication.GetProductFeatures(productId);

		ViewModel = ViewModel.OrderByDescending(x => x.IsPinned)
							 .ThenBy(x => x.Order)
							 .ToList();

		return Page();
	}
}