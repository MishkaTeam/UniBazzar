
using Domain.Aggregates.Customers;
using BuildingBlocks.Domain.Data;

namespace Domain.Aggregates.Users
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<User> GetUserWithMobile(string userName);
        Task<User> GetUserWithUserName(string userName);
    }
}
