using BuildingBlocks.Domain.Context;
using BuildingBlocks.Persistence;
using BuildingBlocks.Persistence.Extensions;
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

    public Task<bool> IsCustomerExists(string mobile)
    {
        return DbSet.AnyAsync(x => x.Mobile == mobile);
    }

    public Task<Customer> GetWithEmail(string userName)
    {
        return DbSet.FirstOrDefaultAsync(x => x.Email == userName);
    }

    public async override Task<Customer> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(x => x.Addresses)
            .StoreFilter(tenantId: ExecutionContext.StoreId)
            .FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);
    }
}