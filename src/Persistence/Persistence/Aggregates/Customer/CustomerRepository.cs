
using Domain.Aggregates.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Aggregates.Customer
{
    public class CustomerRepository(UniBazzarContext uniBazzarContext) : ICustomerRepository
    {
        public void AddCustomer(Domain.Aggregates.Customers.Customer entity)
        {
            uniBazzarContext.Add(entity);
        }

        public Task<List<Domain.Aggregates.Customers.Customer>> GetAllCustomerAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Aggregates.Customers.Customer> GetCustomerAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Domain.Aggregates.Customers.Customer entity)
        {
            throw new NotImplementedException();
        }
    }
}
