using Application.Aggregates.Products;
using Application.Aggregates.Products.ViewModels;
using Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Areas.Admin.Pages.BasicInfo.Products;

public class IndexModel
	(ProductsApplication productsApplication) : BasePageModel
{
	public List<ProductViewModel> ViewModel { get; set; } = [];

	public async Task OnGetAsync()
	{
		ViewModel =
			await productsApplication.GetProducts();
	}
}