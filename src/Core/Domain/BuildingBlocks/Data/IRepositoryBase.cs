using System.Linq.Expressions;

namespace Domain.BuildingBlocks.Data;

public interface IRepositoryBase<TEntity>
{
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task RemoveAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task RemoveRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    Task<bool> RemoveByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> GetSomeAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}