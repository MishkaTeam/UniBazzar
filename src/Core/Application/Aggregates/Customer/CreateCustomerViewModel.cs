
using System.ComponentModel.DataAnnotations;
namespace Application.Aggregates.Customer
{
    public class CreateCustomerViewModel
    {

        [Display(ResourceType = typeof(Resources.DataDictionary),
           Name = nameof(Resources.DataDictionary.Mobile))]
        public string Mobile { get; set; }

        [Display(ResourceType = typeof(Resources.DataDictionary),
           Name = nameof(Resources.DataDictionary.Email))]
        public string Email { get; set; }

        public string IsMobileVerified { get; set; }

        public string IsEmailVerified { get; set; }

        [Display(ResourceType = typeof(Resources.DataDictionary),
           Name = nameof(Resources.DataDictionary.Password))]
        public string Password { get; set; }
    }
}
