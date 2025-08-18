using System.ComponentModel.DataAnnotations;

namespace Application.Aggregates.Customers.ShippingAddresses
{
    public class UpdateShippingAddressViewModel : CreateShippingAddressViewModel
    {
      
        public Guid Id { get; set; }
    }
}
