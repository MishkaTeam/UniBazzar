using BuildingBlocks.Domain.Aggregates;
using Modules.WalletOps.Domain.Aggregates.WalletTrx.Enums;
using Modules.WalletOps.Domain.ValueObjects;

namespace Modules.WalletOps.Domain.Aggregates.WalletTrx;

public class Transaction : Entity
{
    public Guid WalletId { get; private set; }
    public Money Amount { get; private set; }
    public TransactionType Type { get; private set; }
    public string Description { get; private set; }

    private Transaction(Guid walletId, Money amount, TransactionType type, string description)
    {
        WalletId = walletId;
        Amount = amount;
        Type = type;
        Description = description;
    }

    public static Transaction Create(Guid walletId, Money amount, TransactionType type, string description)
    {
        return new Transaction(walletId, amount, type, description);
    }

    // For EF Core
    private Transaction() { }
}