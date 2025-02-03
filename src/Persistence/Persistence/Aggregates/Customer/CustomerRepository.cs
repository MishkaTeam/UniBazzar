
using Domain.Aggregates.Customers;
using Domain.Aggregates.ShippingAddress;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Aggregates.Customer
{
    public class CustomerRepository(UniBazzarContext uniBazzarContext) : ICustomerRepository
    {
        public void AddCustomer(Domain.Aggregates.Customers.Customer entity)
        {
            uniBazzarContext.Add(entity);
        }

        public Task<List<Domain.Aggregates.Customers.Customer>> GetAllCustomersAsync()
        {
            return uniBazzarContext.customers.ToListAsync();
        }

        public Task<Domain.Aggregates.Customers.Customer> GetCustomerAsync(Guid id)
        {
            return uniBazzarContext.customers.FirstOrDefaultAsync(x => x.Id == id); 
        }

        public Task<Domain.Aggregates.Customers.Customer> GetRootCustomersAsync(Guid id)
        {
            return uniBazzarContext.customers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Remove(Domain.Aggregates.Customers.Customer entity)
        {
            uniBazzarContext.customers.Remove(entity);
        }
    }
}
