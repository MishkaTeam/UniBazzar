using BuildingBlocks.Domain.Aggregates;
using Modules.Wallet.Domain.Entities;
using Modules.Wallet.Domain.Exceptions;
using Modules.Wallet.Domain.ValueObjects;

namespace Modules.Wallet.Domain.Aggregates;

// Domain/Aggregates/Wallet.cs
public enum WalletStatus
{
    Active,
    Frozen
}

public enum WalletOwnerType
{
    Personal,       // B2C
    Organization    // B2B
}

public class Wallet : Entity
{
    private readonly List<Transaction> _transactions = new();

    public WalletOwnerType OwnerType { get; private set; }
    public Money Balance { get; private set; }
    public WalletStatus Status { get; private set; }
    public IReadOnlyCollection<Transaction> Transactions => _transactions.AsReadOnly();

    private Wallet(WalletOwnerType ownerType, Money initialBalance) 
    {
        OwnerType = ownerType;
        Balance = initialBalance;
        Status = WalletStatus.Active;
    }

    public static Wallet CreatePersonalWallet(Money initialBalance)
    {
        var wallet = new Wallet(WalletOwnerType.Personal, initialBalance);
        return wallet;
    }

    public void Deposit(Money amount)
    {
        EnsureWalletIsActive();

        Balance += amount;
        _transactions.Add(Transaction.CreateDeposit(Id, amount));
    }

    public void Withdraw(Money amount)
    {
        EnsureWalletIsActive();
        EnsureSufficientFunds(amount);

        Balance -= amount;
        _transactions.Add(Transaction.CreateWithdrawal(Id, amount));
    }

    internal void HoldFundsForTransfer(Money amount, Guid transferId)
    {
        EnsureWalletIsActive();
        EnsureSufficientFunds(amount);

        Balance -= amount;
        _transactions.Add(Transaction.CreateTransfer(Id, amount, TransactionType.TransferOut, transferId));
    }

    internal void ReceiveTransferredFunds(Money amount, Guid transferId)
    {
        EnsureWalletIsActive();

        Balance += amount;
        _transactions.Add(Transaction.CreateTransfer(Id, amount, TransactionType.TransferIn, transferId));
    }

    internal void RevertHeldFunds(Money amount, Guid transferId)
    {
        // This operation can be performed even on a frozen wallet to ensure consistency
        Balance += amount;
        // Optionally, add a reversing transaction or log this event
    }

    public void Freeze()
    {
        if (Status == WalletStatus.Frozen) return;
        Status = WalletStatus.Frozen;
    }

    public void Activate()
    {
        if (Status == WalletStatus.Active) return;
        Status = WalletStatus.Active;
    }

    private void EnsureWalletIsActive()
    {
        if (Status != WalletStatus.Active)
            throw new WalletDeactivateException("Operation cannot be performed on a non-active wallet.");
    }

    private void EnsureSufficientFunds(Money amount)
    {
        if (Balance.IsGreaterThanOrEqualTo(amount)) return;

        throw new InvalidBalanceException("Insufficient funds for this operation.");
    }

    // For EF Core
    private Wallet() { }
}