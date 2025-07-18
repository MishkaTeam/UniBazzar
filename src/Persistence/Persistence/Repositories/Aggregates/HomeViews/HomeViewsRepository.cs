using BuildingBlocks.Persistence;
using Domain.Aggregates.Cms.HomeViews;
using Domain.Aggregates.Cms.HomeViews.Data;

namespace Persistence.Repositories.Aggregates.HomeViews;

public class HomeViewsRepository
    (UniBazzarContext context,
    IExecutionContextAccessor executionContext)
    : RepositoryBase<HomeView>(context, executionContext), IHomeViewRepository
{
}
