using Domain.Aggregates.Units;

namespace Domain.Aggregates.Customers;

public interface ICustomerRepository
{
    void AddCustomer(Customer entity);
    Task<List<Customer>> GetAllCustomerAsync();
    Task<Customer> GetCustomerAsync(Guid id);
    void Remove(Customer entity);
}
