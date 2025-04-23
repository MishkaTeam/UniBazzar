using Application.Aggregates.Users;
using Application.Aggregates.Users.Authentication;
using Framework.DataType;
using Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Server.Infrastructure.Extentions.ServiceCollections;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace Server.Pages.Account;

public class LoginModel(UserApplication userApplication) : BasePageModel
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
        ResultContract<UserViewModel> userResult = default!; 
        UserViewModel? user = default;

        if (isMobile)
            userResult = await userApplication.LoginWithMobileAsync(Model);
        else
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
                new(ClaimTypes.NameIdentifier, user.UserName)
            };

        var claimsIdentity = new ClaimsIdentity(claims, AuthenticationConstant.AUTHENTICATION_SCHEME);

        await HttpContext.SignInAsync(AuthenticationConstant.AUTHENTICATION_SCHEME, new ClaimsPrincipal(claimsIdentity));

        return RedirectToPage("/Index");

    }
}
