using Framework.DataType;
using Modules.Treasury.Api.TreasuryAbstraction;
using Modules.Treasury.Application.Aggregates;
using Modules.Treasury.Application.Contracts;
namespace Modules.Treasury.Api.TreasuryApi;

internal class ReceiptsApi(ReceiptsApplication receiptsApplication) : IReceiptsApi
{
    public Task<ResultContract<Guid>> CreateCashReceiptAsync(ReceiptCustomer customer, decimal price, Guid orderId, CancellationToken cancellationToken)
    {
        return receiptsApplication.CreateCashReceiptAsync(orderId, customer, price, cancellationToken);
    }
}
