using BuildingBlocks.Persistence;
using Domain.Aggregates.branches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Aggregates.Branches
{
    public class BranchRepository
        (UniBazzarContext uniBazzarContext, IExecutionContextAccessor executionContextAccessor)
        : RepositoryBase<Branch>(uniBazzarContext, executionContextAccessor) , IbranchRepository
    {
    }
}
