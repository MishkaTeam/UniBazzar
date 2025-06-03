using BuildingBlocks.Domain.Aggregates;
using Modules.Treasury.Domain.SharedKernel;

namespace Modules.Treasury.Domain.Aggregates.Receipts;

public class CashReceipt : Entity
{
    public long ReceivedDate { get; private init; }
    public string? Description { get; private init; }
    public Money Amount { get; private init; }

    private CashReceipt(long receivedDate, string? description, Money amount)
    {
        if (receivedDate == default) throw new ArgumentException("Received date is required.");
        Amount = amount ?? throw new ArgumentNullException(nameof(amount));

        ReceivedDate = receivedDate;
        Description = description;
        Amount = amount;
    }

    public static CashReceipt Create(long receivedDate, string? description, decimal amount, string currency)
    {
        var money = Money.Create(amount, currency);
        return new CashReceipt(receivedDate, description, money);
    }
}
