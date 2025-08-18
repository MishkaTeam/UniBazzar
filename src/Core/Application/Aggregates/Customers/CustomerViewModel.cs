using Application.Aggregates.Customers.ShippingAddresses;

namespace Application.Aggregates.Customers;


public class CustomerViewModel : UpdateCustomerViewModel
{
    public Guid StoreId { get; set; }
    public List<ShippingAddressViewModel> Addresses { get; set; } = [];
}