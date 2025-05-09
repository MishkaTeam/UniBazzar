namespace Application.Aggregates.Customers;


public class CustomerViewModel : UpdateCustomerViewModel
{
    public Guid StoreId { get; set; }
}