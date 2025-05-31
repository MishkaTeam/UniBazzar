using Framework.DataType;
using Modules.Treasury.Application.Contracts;
using Modules.Treasury.Domain.Aggregates.Receipts.Data;

namespace Modules.Treasury.Application.Aggregatesl;

public class ReceiptsApplication(IReceiptRepository receiptRepository)
{
    public async Task<ResultContract<Guid>> Create(Customer customer, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
