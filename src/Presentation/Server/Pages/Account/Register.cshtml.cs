using Application.Aggregates.Customers;
using Application.Aggregates.Users;
using Application.ViewModels.Authentication;
using Framework.DataType;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Server.Infrastructure.Extentions.ServiceCollections;

namespace Server.Pages;

public class RegisterModel(CustomerApplication customer) : BasePageModel
{
    [BindProperty]
    public RegisterViewModel Model { get; set; } = new();

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        try
        {
            if (!ModelState.IsValid)
                return Page();

            var isMobile = Model.Mobile.IsValidMobile();
            if (!isMobile)
            {
                AddToastError(string.Format(Resources.Messages.Errors.Invalid, Resources.DataDictionary.Mobile));
                return Page();
            }

            var res = await customer.CreateAsync(mobile: Model.Mobile, password: Model.Password);
           

            AddToastSuccess(Resources.Messages.Successes.Success);
            return RedirectToPage(AuthenticationConstant.LOGIN_PAGE_PATH);
        }
        catch (Exception ex)
        {
            AddPageError(Resources.Messages.Errors.InternalError);
        }

        return Page();

    }

}
