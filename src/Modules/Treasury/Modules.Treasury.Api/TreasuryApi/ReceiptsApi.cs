using Framework.DataType;
using Modules.Treasury.Api.TreasuryAbstraction;
using Modules.Treasury.Application.Aggregatesl;
using Modules.Treasury.Application.Contracts;
using Modules.Treasury.Domain.Aggregates.Receipts;
namespace Modules.Treasury.Api.TreasuryApi;

internal class ReceiptsApi(ReceiptsApplication receiptsApplication) : IReceiptsApi
{
    public Task<ResultContract<Guid>> CreateAsync(Customer customer, CancellationToken cancellationToken)
    {
        return receiptsApplication.Create(customer, cancellationToken);

    }
}
