using BuildingBlocks.Domain.SeedWork;

namespace BuildingBlocks.Persistence.Extensions;

public static class DBContextExtensions
{

    public static IQueryable<TEntity> StoreFilter<TEntity>
        (this IQueryable<TEntity> entity, Guid tenantId) where TEntity : class, IEntity
    {
        return entity.Where(x => x.StoreId == tenantId);
    }

    public static IQueryable<TEntity> UserFilter<TEntity>
        (this IQueryable<TEntity> entity, Guid userId) where TEntity : class, IEntity
    {
        return entity.Where(x => x.OwnerId == userId);
    }

}