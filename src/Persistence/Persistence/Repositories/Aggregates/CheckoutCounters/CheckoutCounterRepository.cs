using BuildingBlocks.Persistence;
using Domain.Aggregates.CheckoutCounters;

namespace Persistence.Repositories.Aggregates.CheckoutCounters;

public class CheckoutCounterRepository(UniBazzarContext uniBazzarContext,
                    IExecutionContextAccessor executionContext)
                    : RepositoryBase<CheckoutCounter>(uniBazzarContext, executionContext), ICheckoutCounterRepository
{
}
