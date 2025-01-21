
using System.ComponentModel.DataAnnotations;


namespace Application.Aggregates.Customer
{
	public class CustomerViewModel : CreateCustomerViewModel
    {
        
        [Display(ResourceType = typeof(Resources.DataDictionary),
           Name = nameof(Resources.DataDictionary.Mobile))]
        public string Mobile { get; set; }

        [Display(ResourceType = typeof(Resources.DataDictionary),
           Name = nameof(Resources.DataDictionary.Email))]
        public string Email { get; set; }

        [Display(ResourceType = typeof(Resources.DataDictionary),
                   Name = nameof(Resources.DataDictionary.Id))]
        public Guid Id { get; protected set; }


    }
}
