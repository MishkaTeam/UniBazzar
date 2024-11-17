using Domain;

namespace Persistence;

public class UnitOfWork(UniBazzarContext uniBazzarContext) : IUnitOfWork
{
    public Task<int> CommitAsync(CancellationToken? cancellationToken = null)
    {
        if (cancellationToken.HasValue)
            return uniBazzarContext.SaveChangesAsync(cancellationToken.Value);
        else
            return uniBazzarContext.SaveChangesAsync();
    }
}
