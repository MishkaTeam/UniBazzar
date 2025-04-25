using Application.Aggregates.Customer;
using Application.Aggregates.Users;
using Application.ViewModels.Authentication;
using Framework.DataType;
using Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Server.Infrastructure.Extentions.ServiceCollections;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace Server.Pages.Account;

public class LoginModel(CustomerApplication customerApplication) : BasePageModel
{
    [BindProperty]
    public LoginViewModel Model { get; set; } = new();

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
            return Page();

        var isMobile = Model.UserName.IsValidMobile();
        ResultContract<CustomerViewModel> userResult = default!;
        CustomerViewModel? customer = default;

        userResult = await customerApplication.LoginWithMobileAsync(Model);

        if (!userResult.IsSuccessful)
        {
            AddPageError(userResult.ErrorMessage?.Message);
            return Page();
        }

        customer = userResult.Data;

        if (customer == null || customer.Id == Guid.Empty)
        {
            AddPageError("کاربر یافت نشد");
            return Page();
        }

        var claims = new List<Claim>
            {
                new(ClaimTypes.Name, $"{customer.FirstName} {customer.LastName}"),
                new(ClaimTypes.Sid, customer.Id.ToString()),
                new(ClaimTypes.NameIdentifier, customer.Mobile),
                new(ClaimTypes.GroupSid, customer.StoreId.ToString()),

            };

        var claimsIdentity = new ClaimsIdentity(claims, AuthenticationConstant.AUTHENTICATION_SCHEME);

        await HttpContext.SignInAsync(AuthenticationConstant.AUTHENTICATION_SCHEME, new ClaimsPrincipal(claimsIdentity));

        return RedirectToPage("/Index");

    }
}
