using Framework.DataType;
using Modules.Treasury.Application.Contracts;

namespace Modules.Treasury.Api.TreasuryAbstraction;

public interface IReceiptsApi
{
    Task<ResultContract<Guid>> CreateCashReceiptAsync(ReceiptCustomer customer, decimal price, CancellationToken cancellationToken);
}
