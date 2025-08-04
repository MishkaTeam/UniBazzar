using BuildingBlocks.Domain.Aggregates;
using Modules.Wallet.Domain.ValueObjects;

namespace Modules.Wallet.Domain.Entities;

// Domain/Entities/Transaction.cs
public enum TransactionType
{
    Deposit,
    Withdrawal,
    TransferOut,
    TransferIn
}

public class Transaction : Entity
{
    public Guid WalletId { get; private set; }
    public Money Amount { get; private set; }
    public DateTime OccurredOnUtc { get; private set; }
    public TransactionType Type { get; private set; }
    public Guid? AssociatedTransferId { get; private set; }

    private Transaction(TransactionId id, Guid walletId, Money amount, TransactionType type, Guid? transferId) : base(id)
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

    public static Transaction CreateTransfer(Guid walletId, Money amount, TransactionType type, Guid transferId)
    {
        if (type != TransactionType.TransferIn && type != TransactionType.TransferOut)
            throw new ArgumentException("Invalid transaction type for a transfer.");

        return new(TransactionId.CreateNew(), walletId, amount, type, transferId);
    }

    // For EF Core
    private Transaction() { }
}