using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.Authentication;

public class LoginViewModel
{
    [Display(ResourceType = typeof(Resources.DataDictionary),
             Name = nameof(Resources.DataDictionary.EmailOrMobile))]
    [Required]
    public string UserName { get; set; }

    [Display(ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Password))]
    [Required]
    public string Password { get; set; }
}

public class RegisterViewModel
{
    [Display(ResourceType = typeof(Resources.DataDictionary),
         Name = nameof(Resources.DataDictionary.CellPhonenumber))]
    [Required]
    public string Mobile { get; set; }

    [Display(ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Password))]
    [Required]
    public string Password { get; set; }
}