namespace Domain.Aggregates.Customers;

public interface ICustomerRepository
{
    void AddCustomer(Customer entity);
    Task<List<Customer>> GetAllCustomersAsync();
    Task<Customer> GetCustomerAsync(Guid id);
    Task<Customer> GetRootCustomersAsync(Guid id);
    void Remove(Customer entity); 
}
