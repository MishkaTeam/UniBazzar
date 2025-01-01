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

        public List<ShippingAddress> ShippingAddresses { get; set; }

        [Display(ResourceType = typeof(Resources.DataDictionary),
           Name = nameof(Resources.DataDictionary.NationalCode))]
        public string NationalCode { get; set; }

        [Display(ResourceType = typeof(Resources.DataDictionary),
           Name = nameof(Resources.DataDictionary.Id))]
        public Guid Id { get; protected set; }

        public int Ordering { get; protected set; }

        public Guid InsertedBy { get; protected set; }

        public int Version { get; protected set; }

        public Guid UpdatedBy { get; protected set; }

        public Guid TenantId { get; protected set; }

        public Guid OwnerId { get; protected set; }

        public Guid StoreId { get; protected set; }

        public long InsertDateTime { get; protected set; }

        public long UpdateDateTime { get; protected set; }
    }
}
