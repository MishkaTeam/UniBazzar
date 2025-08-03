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
    public WalletId WalletId { get; private set; }
    public Money Amount { get; private set; }
    public DateTime OccurredOnUtc { get; private set; }
    public TransactionType Type { get; private set; }
    public TransferId? AssociatedTransferId { get; private set; }

    private Transaction(TransactionId id, WalletId walletId, Money amount, TransactionType type, TransferId? transferId) : base(id)
    {
        if (amount.Amount <= 0)
            throw new ArgumentException("Transaction amount must be positive.", nameof(amount));

        WalletId = walletId;
        Amount = amount;
        Type = type;
        OccurredOnUtc = DateTime.UtcNow;
        AssociatedTransferId = transferId;
    }

    public static Transaction CreateDeposit(WalletId walletId, Money amount) =>
        new(TransactionId.CreateNew(), walletId, amount, TransactionType.Deposit, null);

    public static Transaction CreateWithdrawal(WalletId walletId, Money amount) =>
        new(TransactionId.CreateNew(), walletId, amount, TransactionType.Withdrawal, null);

    public static Transaction CreateTransfer(WalletId walletId, Money amount, TransactionType type, TransferId transferId)
    {
        if (type != TransactionType.TransferIn && type != TransactionType.TransferOut)
            throw new ArgumentException("Invalid transaction type for a transfer.");

        return new(TransactionId.CreateNew(), walletId, amount, type, transferId);
    }

    // For EF Core
    private Transaction() { }
}