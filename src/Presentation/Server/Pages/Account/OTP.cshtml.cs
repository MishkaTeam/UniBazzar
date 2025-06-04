using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Application.Aggregates.Customers;
using Application.ViewModels.Authentication;
using Constants;
using Domain.Aggregates.Users.Enums;
using Framework.DataType;
using Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Resources;
using Resources.Messages;
using Server.Infrastructure.Extentions.ServiceCollections;

namespace Server.Pages.Account;

public class OTP(CustomerApplication customerApplication) : BasePageModel
{
    [BindProperty]
    [Display
    (ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.OtpCode))]
    [Required
    (AllowEmptyStrings = false,
        ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.Required))]
    [MaxLength(4)]
    [MinLength(4)]
    public string OtpCode { get; set; }

    public string Mobile { get; set; }

    public void OnGet(string mobile)
    {
        Mobile = mobile;
    }

    public async Task<IActionResult> OnPost(string mobile, string returnUrl = null)
    {
        if (ModelState.IsValid
            && OtpCode == "1111")
        {
            return await LoginCustomer(mobile: mobile, returnUrl);
        }

        return Page();
    }

    private async Task<IActionResult> LoginCustomer(string mobile, string returnUrl)
    {
        ResultContract<CustomerViewModel> userResult = default!;
        CustomerViewModel? customer = default;

        userResult = await customerApplication.LoginWithMobileAsync(mobile);

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
            new(ClaimTypes.Role, RoleType.Customer.ToString())
        };

        var claimsIdentity = new ClaimsIdentity(claims, AuthenticationConstant.AUTHENTICATION_SCHEME);

        await HttpContext.SignInAsync(AuthenticationConstant.AUTHENTICATION_SCHEME,
            new ClaimsPrincipal(claimsIdentity));

        return RedirectToPage(returnUrl ?? "/");
    }
}