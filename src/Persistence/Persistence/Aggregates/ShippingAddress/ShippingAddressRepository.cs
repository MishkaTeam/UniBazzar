using Domain.Aggregates.ShippingAddress;
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
        public void AddShippingAddress(Domain.Aggregates.ShippingAddress.ShippingAddress entity)
        {
            uniBazzarContext.Add(entity);
        }

        public Task<List<Domain.Aggregates.ShippingAddress.ShippingAddress>> GetAllShippingAddressAsync()
        {
            return uniBazzarContext.shippingAddresses.ToListAsync();
        }

        public Task<Domain.Aggregates.ShippingAddress.ShippingAddress> GetShippingAddressAsync(Guid id)
        {
           return uniBazzarContext.shippingAddresses.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Remove(Domain.Aggregates.ShippingAddress.ShippingAddress entity)
        {
            uniBazzarContext.Remove(entity);
        }
    }
}
