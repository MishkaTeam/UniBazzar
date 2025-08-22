using BuildingBlocks.Persistence;
using BuildingBlocks.Domain.Context;
using Domain.Aggregates.SiteSettings;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Persistence.Repositories.Aggregates.SiteSettings;

public class SiteSettingRepository : RepositoryBase<SiteSetting>, ISiteSettingRepository
{
    public SiteSettingRepository(UniBazzarContext context, IExecutionContextAccessor executionContextAccessor) 
        : base(context, executionContextAccessor)
    {
    }

    public override async Task<SiteSetting?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public override async Task<IEnumerable<SiteSetting>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet
            .ToListAsync(cancellationToken);
    }

    public override async Task<SiteSetting?> GetAsync(Expression<Func<SiteSetting, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .FirstOrDefaultAsync(predicate, cancellationToken);
    }
}
