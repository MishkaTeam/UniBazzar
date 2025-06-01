using Modules.Treasury.Domain;

namespace Modules.Treasury.Persistence;

public class UnitOfWork(TreasuryDbContext dbContext) : IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken? cancellationToken = null)
    {
        if (cancellationToken.HasValue)
            return dbContext.SaveChangesAsync(cancellationToken.Value);
        else
            return dbContext.SaveChangesAsync();
    }
}
