using BuildingBlocks.Domain.Aggregates;
using Modules.Treasury.Domain.Aggregates.Counterparties;

namespace Modules.Treasury.Domain.Aggregates.Receipts;

public class Receipt : Entity
{
    public Guid? OrderId { get; private set; }
    public Counterparty CounterpartyId { get; private init; }
    private readonly List<CashReceipt> _receipts = new();

    public IReadOnlyList<CashReceipt> AllReceipts => _receipts.AsReadOnly();


    private Receipt(Counterparty counterpartyId)
    {
        CounterpartyId = counterpartyId;
    }

    public static Receipt Create(Counterparty customerId) => new(customerId);

    public void AddCashReceipt(long receivedDate, decimal amount, string currency, string? description = null)
    {
        var receipt = CashReceipt.Create(receivedDate, description!, amount, currency);
        _receipts.Add(receipt);
    }

    public decimal CalculateTotalReceived(string currency)
    {
        return _receipts
            .Where(r => r.Amount.Currency == currency)
            .Sum(r => r.Amount.Amount);
    }

    public void SetOrderId(Guid? orderId)
    {
        OrderId = orderId;
    }
}