using BuildingBlocks.Domain.Context;
using BuildingBlocks.Persistence;
using Domain.Aggregates.Customers.ShippingAddresses;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories.Aggregates.Customers;

public class ShippingAddressRepository
    (UniBazzarContext uniBazzarContext, IExecutionContextAccessor executionContextAccessor)
    : RepositoryBase<ShippingAddress>(uniBazzarContext, executionContextAccessor), IShippingAddressRepository
{
    public Task<List<ShippingAddress>> GetAllShippingAddressAsync(Guid CustomerId)
    {
        return uniBazzarContext.ShippingAddresses.Where(x => x.CustomerId == CustomerId).ToListAsync();
    }
}