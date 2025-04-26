using BuildingBlocks.Persistence;
using Domain.Aggregates.CheckoutCounter;

namespace Persistence.Aggregates.CheckoutCounters;

public class CheckoutCounterRepository(UniBazzarContext uniBazzarContext,
                    IExecutionContextAccessor executionContext)
                    : RepositoryBase<CheckoutCounter>(uniBazzarContext, executionContext), ICheckoutCounterRepository
{
}
