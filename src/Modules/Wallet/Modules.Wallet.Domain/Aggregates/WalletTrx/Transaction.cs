using BuildingBlocks.Domain.Aggregates;
using Modules.WalletOps.Domain.ValueObjects;

namespace Modules.WalletOps.Domain.Aggregates.WalletTrx;

// Domain/Entities/Transaction.cs
public enum TransactionType
{
    Deposit,
    Withdrawal,
    Hold
}

public class Transaction : Entity
{
    public Guid WalletId { get; private set; }
    public Money Amount { get; private set; }
    public TransactionType Type { get; private set; }
    public Wallet Wallet { get; private set; }

    private Transaction(TransactionId id, Guid walletId, Money amount, TransactionType type, Guid? transferId) 
    {
        if (amount.Amount <= 0)
            throw new ArgumentException("Transaction amount must be positive.", nameof(amount));

        WalletId = walletId;
        Amount = amount;
        Type = type;
        OccurredOnUtc = DateTime.UtcNow;
        AssociatedTransferId = transferId;
    }

    public static Transaction CreateDeposit(Guid walletId, Money amount) =>
        new(TransactionId.CreateNew(), walletId, amount, TransactionType.Deposit, null);

    public static Transaction CreateWithdrawal(Guid walletId, Money amount) =>
        new(TransactionId.CreateNew(), walletId, amount, TransactionType.Withdrawal, null);

    public static Transaction CreateHold(Guid walletId, Money amount) =>
    new(TransactionId.CreateNew(), walletId, amount, TransactionType.Hold, null);


    // For EF Core
    private Transaction() { }
}