using Application.Aggregates.Discounts;
using Application.Aggregates.Discounts.ViewModels;
using Domain.Aggregates.Discounts.DsiscounProducts;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Areas.Admin.Pages.BasicInfo.Discounts;

public class DiscountProductModel(DiscountProductApplication application) : BasePageModel
{

	[BindProperty]
	public DiscountProductViewModel ViewModel { get; set; } = new();

	public List<DetailsAndDeleteDiscountProductViewModel> DiscountProducts { get; set; } = [];

	public async Task OnGetAsync(Guid discountId)
	{
		DiscountProducts = await application.GetAllDiscountProductByDiscountId(discountId);
	}

	public async Task<IActionResult> OnPost(Guid discountId)
	{
		ViewModel.DiscountId = discountId;
		await application.CreateDiscountProduct(ViewModel);

		DiscountProducts = await application.GetAllDiscountProductByDiscountId(discountId);

		return Page();
	}

	public async Task<IActionResult> OnPostDelete(Guid Id, Guid discountId)
	{
		await application.DeleteDiscountProductAsync(Id);

		DiscountProducts = await application.GetAllDiscountProductByDiscountId(discountId);

		return Page();
	}
}
