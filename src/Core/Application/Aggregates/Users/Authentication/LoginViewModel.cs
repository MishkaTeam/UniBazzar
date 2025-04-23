using System.ComponentModel.DataAnnotations;

namespace Application.Aggregates.Users.Authentication;

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
