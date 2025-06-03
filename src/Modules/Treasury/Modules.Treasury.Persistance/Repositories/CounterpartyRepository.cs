using BuildingBlocks.Persistence;
using Modules.Treasury.Domain.Aggregates.Counterparties;
using Modules.Treasury.Domain.Aggregates.Counterparties.Data;

namespace Modules.Treasury.Persistence.Repositories;

internal class CounterpartyRepository : RepositoryBase<Counterparty>, ICounterpartyRepository
{
    public CounterpartyRepository(TreasuryDbContext context, IExecutionContextAccessor executionContext)
                    : base(context, executionContext)
    {
    }
}
