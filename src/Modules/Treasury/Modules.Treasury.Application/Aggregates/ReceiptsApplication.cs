using BuildingBlocks.Domain.SeedWork;
using Framework.DataType;
using Modules.Treasury.Application.Contracts;
using Modules.Treasury.Domain;
using Modules.Treasury.Domain.Aggregates.Counterparties;
using Modules.Treasury.Domain.Aggregates.Counterparties.Enums;
using Modules.Treasury.Domain.Aggregates.Receipts;
using Modules.Treasury.Domain.Aggregates.Receipts.Data;

namespace Modules.Treasury.Application.Aggregates;

public class ReceiptsApplication(IReceiptRepository receiptRepository, IUnitOfWork unitOfWork)
{

    public async Task<ResultContract<Guid>> CreateCashReceiptAsync(Guid orderId, ReceiptCustomer customer, decimal price, CancellationToken cancellationToken)
    {
        var counterParty = Counterparty.CreateFromSales(sourceCustomerID: customer.CustomerId, fullName: customer.CustomerName, counterpartyType: CounterpartyType.Individual);
        var receipt = Receipt.Create(counterParty);
        receipt.SetOrderId(orderId: orderId);
        receipt.AddCashReceipt(DateTimeUtility.GetCurrentUnixUTCTimeSeconds(), price, "IRR");
        await receiptRepository.AddAsync(receipt, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return receipt.Id;
    }
}
