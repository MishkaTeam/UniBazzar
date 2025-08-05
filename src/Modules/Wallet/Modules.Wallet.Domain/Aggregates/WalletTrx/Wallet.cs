using BuildingBlocks.Domain.Aggregates;
using Modules.Wallet.Domain.Exceptions;
using Modules.WalletOps.Domain.Exceptions;
using Modules.WalletOps.Domain.ValueObjects;

namespace Modules.WalletOps.Domain.Aggregates.WalletTrx;

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
    private readonly List<HeldFund> _heldFunds = new();

    public WalletOwnerType OwnerType { get; private set; }
    public Money Balance { get; private set; }
    public WalletStatus Status { get; private set; }
    public IReadOnlyCollection<Transaction> Transactions => _transactions.AsReadOnly();
    public IReadOnlyCollection<HeldFund> HeldFunds => _heldFunds.AsReadOnly();

    private Wallet(WalletOwnerType ownerType, Money initialBalance)
    {
        OwnerType = ownerType;
        Balance = initialBalance;
        Status = WalletStatus.Active;
    }

    public static Wallet CreatePersonalWallet(Money? initialBalance = null)
    {
        initialBalance ??= Money.Create(0, "IRR");
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

    internal void HoldFundsForTransfer(Money amount)
    {
        EnsureWalletIsActive();
        EnsureSufficientFunds(amount);

        Balance -= amount;
        _transactions.Add(Transaction.CreateHold(Id, amount));
    }


    internal void RevertHeldFunds(Money amount)
    {
        // This operation can be performed even on a frozen wallet to ensure consistency
        Balance += amount;
        _transactions.Add(Transaction.CreateHold(Id, amount));
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