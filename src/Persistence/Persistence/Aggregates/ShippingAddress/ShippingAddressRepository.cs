using Domain.Aggregates.Customers;
using Domain.Aggregates.Customers.ShippingAddress;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Aggregates.ShippingAddress
{
    public class ShippingAddressRepository(UniBazzarContext uniBazzarContext) : IShippingAddressRepository
    {
        public void AddShippingAddress(Domain.Aggregates.Customers.ShippingAddress.ShippingAddress entity)
        {
            uniBazzarContext.Add(entity);
        }

        public Task<List<Domain.Aggregates.Customers.ShippingAddress.ShippingAddress>> GetAllShippingAddressAsync(Guid CustomerId)
        {
            return uniBazzarContext.ShippingAddresses.Where(x=> x.CustomerId == CustomerId).ToListAsync();
        }

        public Task<Domain.Aggregates.Customers.ShippingAddress.ShippingAddress> GetShippingAddressAsync(Guid id)
        {
           return uniBazzarContext.ShippingAddresses.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Remove(Domain.Aggregates.Customers.ShippingAddress.ShippingAddress entity)
        {
            uniBazzarContext.Remove(entity);
        }
    }
}
