using Domain.Aggregates.Users;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Aggregates.User
{
    public class UserRepository(UniBazzarContext uniBazzarContext) : IUserRepository
    {
       
        public void AddUser(Domain.Aggregates.Users.User entity)
        {
            uniBazzarContext.Add(entity);
        }

        public Task<List<Domain.Aggregates.Users.User>> GetAllUsersAsync()
        {
            return uniBazzarContext.Users.ToListAsync();
        }

        public Task<Domain.Aggregates.Users.User> GetRootUsersAsync(Guid id)
        {
            return uniBazzarContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<Domain.Aggregates.Users.User> GetUserAsync(Guid id)
        {
            return uniBazzarContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Remove(Domain.Aggregates.Users.User entity)
        {
            uniBazzarContext.Users.Remove(entity);
        }
    }
}
