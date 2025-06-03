using Domain;

namespace Persistence;

public class UnitOfWork(UniBazzarContext uniBazzarContext) : IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken? cancellationToken = null)
    {
        if (cancellationToken.HasValue)
            return uniBazzarContext.SaveChangesAsync(cancellationToken.Value);
        else
            return uniBazzarContext.SaveChangesAsync();
    }
}
