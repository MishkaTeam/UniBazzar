using BuildingBlocks.Persistence;
using Domain.Aggregates.Customers;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories.Aggregates.Customers;

public class CustomerRepository
    (UniBazzarContext context, IExecutionContextAccessor executionContext)
    : RepositoryBase<Customer>(context, executionContext), ICustomerRepository
{
    public Task<Customer> GetWithMobile(string userName)
    {
        return DbSet.FirstOrDefaultAsync(x => x.Mobile == userName);
    }

    public Task<Customer> GetWithEmail(string userName)
    {
        return DbSet.FirstOrDefaultAsync(x => x.Email == userName);
    }
}