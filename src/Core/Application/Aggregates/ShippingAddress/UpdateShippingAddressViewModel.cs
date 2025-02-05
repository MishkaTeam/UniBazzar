using System.ComponentModel.DataAnnotations;

namespace Application.Aggregates.ShippingAddress
{
    public class UpdateShippingAddressViewModel 
    {
        [Display(ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Country))]
        public string Country { get; set; }

        [Display(ResourceType = typeof(Resources.DataDictionary),
         Name = nameof(Resources.DataDictionary.Province))]
        public string Province { get; set; }

        [Display(ResourceType = typeof(Resources.DataDictionary),
                 Name = nameof(Resources.DataDictionary.City))]
        public string City { get; set; }

        [Display(ResourceType = typeof(Resources.DataDictionary),
         Name = nameof(Resources.DataDictionary.Address))]
        public string Address { get; set; }

        [Display(ResourceType = typeof(Resources.DataDictionary),
         Name = nameof(Resources.DataDictionary.PostalCode))]
        public string PostalCode { get; set; }

        public Guid CustomerId { get; set; }

        public Guid Id { get; set; }
    }
}
