using BuildingBlocks.Domain.Aggregates;
using Modules.WalletOps.Domain.Aggregates.WalletTrx.Enums;
using Modules.WalletOps.Domain.Aggregates.WalletTrx.Extenstions;
using Modules.WalletOps.Domain.Exceptions;
using Modules.WalletOps.Domain.ValueObjects;

namespace Modules.WalletOps.Domain.Aggregates.WalletTrx;

public class Wallet : Entity
{
    private readonly List<Transaction> _transactions = [];
    private readonly List<HeldFund> _heldFunds = [];

    public Money Balance { get; private set; } = Money.Zero("IRR");
    public Money WithdrawableBalance  => Transactions.WithdrawableBalance();
    public Money HeldBalance => HeldFunds.HeldBalance();
    public Money NonWithdrawableBalance => Transactions.NonWithdrawableBalance();


    public WalletStatusType Status { get; private set; }
    public IReadOnlyCollection<Transaction> Transactions => _transactions.AsReadOnly();
    public IReadOnlyCollection<HeldFund> HeldFunds => _heldFunds.AsReadOnly();

    private Wallet()
    {
        Status = WalletStatusType.Active;
    }

    public static Wallet CreateWallet()
    {        
        var wallet = new Wallet();
        return wallet;
    }

    public void WithdrawableDeposit(Money amount)
    {
        EnsureWalletIsActive();

        _transactions.Add(Transaction.WithdrawableDeposit(Id, amount));
    }

    public void NonWithdrawableDeposit(Money amount)
    {
        EnsureWalletIsActive();

        _transactions.Add(Transaction.NonWithdrawableDeposit(Id, amount));
    }

    public void Withdraw(Money amount)
    {
        EnsureWalletIsActive();
        EnsureSufficientFunds(amount);

        _transactions.Add(Transaction.Withdrawal(Id, amount));
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


}