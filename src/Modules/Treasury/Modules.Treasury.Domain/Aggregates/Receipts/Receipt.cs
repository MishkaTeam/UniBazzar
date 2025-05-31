using BuildingBlocks.Domain.Aggregates;

namespace Modules.Treasury.Domain.Aggregates.Receipts;

public class Receipt : Entity
{
    public Guid CustomerId { get; private init; }
    private readonly List<CashReceipt> _receipts = new();

    public IReadOnlyList<CashReceipt> AllReceipts => _receipts.AsReadOnly();

    private Receipt(Guid customerId)
    {
        if (customerId == Guid.Empty) throw new ArgumentException("CustomerId cannot be empty.");
        CustomerId = customerId;
    }

    public static Receipt Create(Guid customerId) => new(customerId);

    public void AddReceipt(Guid id, DateTime receivedDate, string description, decimal amount, string currency)
    {
        var receipt = CashReceipt.Create(id, receivedDate, description, amount, currency);
        _receipts.Add(receipt);
    }

    public decimal CalculateTotalReceived(string currency)
    {
        return _receipts
            .Where(r => r.Amount.Currency == currency)
            .Sum(r => r.Amount.Amount);
    }
}