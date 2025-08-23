using BuildingBlocks.Persistence;
using Domain.Aggregates.SiteSettings;

namespace Persistence.Repositories;

public class SiteSettingRepository(UniBazzarContext context, IExecutionContextAccessor executionContext) 
    : RepositoryBase<SiteSetting>(context, executionContext), ISiteSettingRepository
{
}
