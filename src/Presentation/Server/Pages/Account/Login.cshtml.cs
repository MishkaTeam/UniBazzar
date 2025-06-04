using System.ComponentModel.DataAnnotations;
using Application.Aggregates.Customers;
using Constants;
using Framework.DataType;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Resources;
using Resources.Messages;

namespace Server.Pages.Account;

public class LoginModel(CustomerApplication customerApplication) : BasePageModel
{
    [BindProperty]
    [Display
    (ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.Mobile))]
    [Required
    (AllowEmptyStrings = false,
        ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.Required))]
    [RegularExpression
    (RegularExpression.CellPhoneNumber,
        ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.CellPhoneNumber))]
    public string Mobile { get; set; }

    public string ReturnUrl { get; set; }
    public void OnGet(string? returnUrl = null)
    {
        ReturnUrl = returnUrl ?? Url.Content("/");
    }

    public async Task<IActionResult> OnPost(string? returnUrl = null)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var isCustomerExsists = await customerApplication.IsExistsAsync(Mobile);
                if (isCustomerExsists is { IsSuccessful: true, Data: true })
                {
                    await SendOTP(Mobile);
                    return RedirectToPage("OTP", new { mobile = Mobile, returnUrl });
                }
                else if (isCustomerExsists is { IsSuccessful: true, Data: false })
                {
                    await customerApplication.CreateAsync(new CreateCustomerViewModel()
                    {
                        Mobile = Mobile,
                        LastName = Mobile
                    });
                    return RedirectToPage("OTP", new { mobile = Mobile, returnUrl });
                }
                else if (isCustomerExsists is { IsSuccessful: false })
                {
                    
                }
            }
            catch (Exception e)
            {
                return Page();
            }
        }
        return Page();
    }

    private Task SendOTP(string mobile)
    {
        return Task.CompletedTask;

    }
}