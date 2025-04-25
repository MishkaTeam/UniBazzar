namespace Application.Aggregates.Customer
{
    public class CustomerViewModel : UpdateCustomerViewModel
    {
        public Guid StoreId { get; set; }
    }
}
