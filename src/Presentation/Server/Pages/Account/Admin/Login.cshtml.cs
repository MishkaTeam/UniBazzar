using Application.Aggregates.Customers;
using Application.Aggregates.Users;
using Application.ViewModels.Authentication;
using Domain.Aggregates.Users.Enums;
using Framework.DataType;
using Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Server.Infrastructure.Extentions.ServiceCollections;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace Server.Pages.Account.Admin;

public class LoginModel(UserApplication userApplication) : BasePageModel
{
    [BindProperty]
    public LoginViewModel Model { get; set; } = new();

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost(string returnUrl = null)
    {
        if (!ModelState.IsValid)
            return Page();
        
        return await LoginUser(null);

    }
    private async Task<IActionResult> LoginUser(string returnUrl)
    {
        ResultContract<UserViewModel> userResult = default!;
        UserViewModel? user = default;

        userResult = await userApplication.LoginWithUserNameAsync(Model);

        if (!userResult.IsSuccessful)
        {
            AddPageError(userResult.ErrorMessage?.Message);
            return Page();
        }

        user = userResult.Data;

        if (user == null || user.Id == Guid.Empty)
        {
            AddPageError("کاربر یافت نشد");
            return Page();
        }

        var claims = new List<Claim>
            {
                new(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new(ClaimTypes.Sid, user.Id.ToString()),
                new(ClaimTypes.NameIdentifier, user.Mobile),
                new(ClaimTypes.GroupSid, user.StoreId.ToString()),
                new(ClaimTypes.Role,user.Role.ToString())
            };

        var claimsIdentity = new ClaimsIdentity(claims, AuthenticationConstant.AUTHENTICATION_SCHEME);

        await HttpContext.SignInAsync(AuthenticationConstant.AUTHENTICATION_SCHEME, new ClaimsPrincipal(claimsIdentity));

        return RedirectToPage(returnUrl ?? "/Index");
    }

}
