using BuildingBlocks.Persistence;
using Domain.Aggregates.branches;

namespace Persistence.Repositories.Aggregates.Branches
{
    public class BranchRepository
        (UniBazzarContext uniBazzarContext, IExecutionContextAccessor executionContextAccessor)
        : RepositoryBase<Branch>(uniBazzarContext, executionContextAccessor) , IbranchRepository
    {
    }
}
