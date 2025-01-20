
using Domain.Aggregates.Customers;
using System.ComponentModel.DataAnnotations;
namespace Application.Aggregates.Customer
{
    public class CreateCustomerViewModel
    {
        [Display(ResourceType = typeof(Resources.DataDictionary),
          Name = nameof(Resources.DataDictionary.Name))]
        public string FirstName { get; private set; }

        [Display(ResourceType = typeof(Resources.DataDictionary),
          Name = nameof(Resources.DataDictionary.Family))]
        public string LastName { get; private set; }

        [Display(ResourceType = typeof(Resources.DataDictionary),
          Name = nameof(Resources.DataDictionary.NationalCode))]
        public string NationalCode { get; private set; }

        [Display(ResourceType = typeof(Resources.DataDictionary),
           Name = nameof(Resources.DataDictionary.Mobile))]
        public string Mobile { get; set; }

        [Display(ResourceType = typeof(Resources.DataDictionary),
           Name = nameof(Resources.DataDictionary.Email))]
        public string Email { get; set; }

       
        [Display(ResourceType = typeof(Resources.DataDictionary),
           Name = nameof(Resources.DataDictionary.Password))]
        public string Password { get; set; }

		[Display(ResourceType = typeof(Resources.DataDictionary),
				   Name = nameof(Resources.DataDictionary.Id))]
		public Guid Id { get; protected set; }

        [Display(ResourceType = typeof(Resources.DataDictionary),
                   Name = nameof(Resources.DataDictionary.ConfirmPassword))]
        public string ConfirmPassword { get; set; }
    }
}
