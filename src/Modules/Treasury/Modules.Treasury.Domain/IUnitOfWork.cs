namespace Modules.Treasury.Domain;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken? cancellationToken = null);
}
