using BuildingBlocks.Domain.Context;
using BuildingBlocks.Persistence;
using Modules.Treasury.Domain.Aggregates.Receipts;
using Modules.Treasury.Domain.Aggregates.Receipts.Data;

namespace Modules.Treasury.Persistence.Repositories;

public class ReceiptRepository : RepositoryBase<Receipt>, IReceiptRepository
{
    public ReceiptRepository(TreasuryDbContext context, IExecutionContextAccessor executionContext) 
                    : base(context, executionContext)
    {
    }
}
