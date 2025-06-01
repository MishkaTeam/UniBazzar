namespace BuildingBlocks.Domain.Data;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken? cancellationToken = null);
}
