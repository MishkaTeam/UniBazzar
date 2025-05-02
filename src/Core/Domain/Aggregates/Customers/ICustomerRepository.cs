using Domain.BuildingBlocks.Data;

namespace Domain.Aggregates.Customers;

public interface ICustomerRepository : IRepositoryBase<Customer>
{
    Task<Customer> GetWithEmail(string userName);
    Task<Customer> GetWithMobile(string userName);
}