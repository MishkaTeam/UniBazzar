using Domain.Aggregates.Customers;
using System.ComponentModel.DataAnnotations;

namespace Application.Aggregates.Customer
{
    public class UpdateCustomerViewModel : CreateCustomerViewModel 
    {
        [Display(ResourceType = typeof(Resources.DataDictionary),
                   Name = nameof(Resources.DataDictionary.Name))]
        public string FirstName { get; set; }

        [Display(ResourceType = typeof(Resources.DataDictionary),
           Name = nameof(Resources.DataDictionary.Family))]
        public string LastName { get; set; }

        [Display(ResourceType = typeof(Resources.DataDictionary),
           Name = nameof(Resources.DataDictionary.NationalCode))]
        public string NationalCode { get; set; }

        [Display(ResourceType = typeof(Resources.DataDictionary),
           Name = nameof(Resources.DataDictionary.Id))]
        public Guid Id { get;  set; }

    }
}
