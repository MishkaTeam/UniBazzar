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

    /// <summary>
    /// این پراپرتی میتواند نال باشد
    /// اما وقتی پر میشود نمیتواند تکراری باشد
    /// </summary>
    public string? OperationId { get; private set; }

    private Transaction(Guid walletId, Money amount, TransactionType type, string description, string? operationId)
    {
        WalletId = walletId;
        Amount = amount;
        Type = type;
        Description = description;
        OperationId = operationId;
    }

    public static Transaction CreateDepositWithdrawable(Guid walletId, Money amount, string description, string? operationId)
    {
        return new Transaction(walletId, amount, TransactionType.Withdrawable_Deposit, description, operationId);
    }

    internal static Transaction CreateNonWithdrawableDeposit(Guid walletId, Money amount, string description, string? operationId)
    {
        return new Transaction(walletId, amount, TransactionType.Non_Withdrawable_Deposit, description, operationId);
    }

    internal static Transaction CreateWithdrawal(Guid walletId, Money amount, string description, string? operationId)
    {
        return new Transaction(walletId, amount, TransactionType.Withdrawal, description, operationId);
    }

    internal static Transaction CreatePurchase(Guid walletId, Money amount, string description, string? operationId)
    {
        return new Transaction(walletId, amount, TransactionType.Purchase, description, operationId);
    }

    // For EF Core
    private Transaction() { }
}