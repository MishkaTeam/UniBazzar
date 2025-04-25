namespace Domain.Aggregates.Customers;

public interface ICustomerRepository
{
    void AddCustomer(Customer entity);
    Task<List<Customer>> GetAllCustomersAsync();
    Task<Customer> GetCustomerAsync(Guid id);
    Task<Customer> GetRootCustomersAsync(Guid id);
    Task<Customer> GetWithEmail(string userName);
    Task<Customer> GetWithMobile(string userName);
    void Remove(Customer entity); 
}
