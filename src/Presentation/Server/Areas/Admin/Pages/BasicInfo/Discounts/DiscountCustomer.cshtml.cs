using Application.Aggregates.Discounts;
using Application.Aggregates.Discounts.ViewModels;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Areas.Admin.Pages.BasicInfo.Discounts;

public class DiscountCustomerModel(DiscountCustomerApplication application) : BasePageModel
{

	[BindProperty]
	public DiscountCustomerViewModel ViewModel { get; set; } = new();

	public List<DetailsAndDeleteDiscountCustomerViewModel> DiscountCustomers { get; set; } = [];

	public async Task OnGet(Guid discountId)
	{
		DiscountCustomers = await application.GetAllDiscountProductByDiscountId(discountId);
	}

	public async Task<IActionResult> OnPost(Guid discountId)
	{
		ViewModel.DiscountId = discountId;
		await application.CreateDiscountProduct(ViewModel);

		return Page();
	}

	public async Task<IActionResult> OnPostDelete(Guid Id)
	{
		await application.DeleteDiscountProductAsync(Id);

		return Page();
	}
}
