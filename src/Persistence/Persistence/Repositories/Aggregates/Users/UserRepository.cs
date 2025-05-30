using BuildingBlocks.Persistence;
using Domain.Aggregates.Users;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories.Aggregates.Users;

public class UserRepository(UniBazzarContext uniBazzarContext, IExecutionContextAccessor executionContextAccessor)
    : RepositoryBase<User>(uniBazzarContext, executionContextAccessor), IUserRepository
{

    public Task<User> GetUserWithMobile(string userName)
    {
        return DbSet.FirstOrDefaultAsync(x => x.Mobile == userName || x.UserName == userName);
    }

    public Task<User> GetUserWithUserName(string userName)
    {
        return DbSet.FirstOrDefaultAsync(x => x.UserName == userName);
    }

}
