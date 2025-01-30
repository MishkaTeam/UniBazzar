using Application.Aggregates.Products;
using Application.Aggregates.Products.ProductFeatures.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Areas.Admin.Pages.BasicInfo.Products.ProductFeatures;

public class CreateModel
	(ProductsApplication productsApplication) : PageModel
{
	[BindProperty]
	public CreateProductFeatureViewModel CreateViewModel { get; set; } = new();

	public IActionResult OnGet(Guid id)
	{
		if (id == Guid.Empty)
		{
			return RedirectToPage("../Index");
		}

		CreateViewModel.ProductId = id;

		return Page();
	}

	public async Task<IActionResult> OnPostAsync()
	{
		if (ModelState.IsValid)
		{
			await productsApplication.CreateProductFeatureAsync(CreateViewModel);
		}

		return RedirectToPage("Index",
			new { id = CreateViewModel.ProductId.ToString() });
	}
}