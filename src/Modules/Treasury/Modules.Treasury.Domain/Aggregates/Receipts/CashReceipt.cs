using BuildingBlocks.Domain.Aggregates;
using Modules.Treasury.Domain.SharedKernel;

namespace Modules.Treasury.Domain.Aggregates.Receipts;

public class CashReceipt : Entity
{
    public DateTime ReceivedDate { get; private init; }
    public string Description { get; private init; }
    public Money Amount { get; private init; }

    private CashReceipt(Guid id, DateTime receivedDate, string description, Money amount)
    {
        if (id == Guid.Empty) throw new ArgumentException("Id cannot be empty.");
        if (receivedDate == default) throw new ArgumentException("Received date is required.");
        if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Description is required.");
        Amount = amount ?? throw new ArgumentNullException(nameof(amount));

        Id = id;
        ReceivedDate = receivedDate;
        Description = description;
        Amount = amount;
    }

    public static CashReceipt Create(Guid id, DateTime receivedDate, string description, decimal amount, string currency)
    {
        var money = Money.Create(amount, currency);
        return new CashReceipt(id, receivedDate, description, money);
    }
}
