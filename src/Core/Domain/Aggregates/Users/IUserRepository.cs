
using Domain.Aggregates.Customers;

namespace Domain.Aggregates.Users
{
    public interface IUserRepository
    {
        void AddUser(User entity);
        Task<List<User>> GetAllUsersAsync();
        Task<User> GetUserAsync(Guid id);
        Task<User> GetRootUsersAsync(Guid id);
        void Remove(User entity);
    }
}
