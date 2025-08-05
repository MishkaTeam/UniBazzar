using BuildingBlocks.Domain.Aggregates;
using Modules.WalletOps.Domain.Aggregates.WalletTrx.Enums;
using Modules.WalletOps.Domain.Exceptions;
using Modules.WalletOps.Domain.ValueObjects;

namespace Modules.WalletOps.Domain.Aggregates.WalletTrx;

public class Wallet : Entity
{
    private readonly List<Transaction> _transactions = new();
    private readonly List<HeldFund> _heldFunds = new();

    public WalletOwnerType OwnerType { get; private set; }
    public Money Balance { get; private set; }
    public WalletStatusType Status { get; private set; }
    public IReadOnlyCollection<Transaction> Transactions => _transactions.AsReadOnly();
    public IReadOnlyCollection<HeldFund> HeldFunds => _heldFunds.AsReadOnly();

    private Wallet(WalletOwnerType ownerType, Money initialBalance)
    {
        OwnerType = ownerType;
        Balance = initialBalance;
        Status = WalletStatusType.Active;
    }

    public static Wallet CreateWallet(Money? initialBalance = null)
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
        if (Status == WalletStatusType.Frozen) return;
        Status = WalletStatusType.Frozen;
    }

    public void Activate()
    {
        if (Status == WalletStatusType.Active) return;
        Status = WalletStatusType.Active;
    }

    private void EnsureWalletIsActive()
    {
        if (Status != WalletStatusType.Active)
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