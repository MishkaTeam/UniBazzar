using Domain.Aggregates.Customers;
using System.ComponentModel.DataAnnotations;

namespace Application.Aggregates.Customer
{
    public class UpdateCustomerViewModel : CreateCustomerViewModel 
    {
        [Display(ResourceType = typeof(Resources.DataDictionary),
           Name = nameof(Resources.DataDictionary.Id))]
        public Guid Id { get;  set; }

    }
}
