using BuildingBlocks.Domain.Aggregates;
using Modules.WalletOps.Domain.Aggregates.WalletTrx.Enums;
using Modules.WalletOps.Domain.Exceptions;
using Modules.WalletOps.Domain.ValueObjects;

namespace Modules.WalletOps.Domain.Aggregates.WalletTrx;

public class Wallet : Entity
{
    private readonly List<Transaction> _transactions = [];
    private readonly List<HeldFund> _heldFunds = [];

    public Money WithdrawableBalance { get; private set; }
    public Money NonWithdrawableBalance { get; private set; }
    public Money HeldBalance { get; private set; }
    public Money TotalBalance => WithdrawableBalance + NonWithdrawableBalance;
    public Money AvailableBalance => TotalBalance - HeldBalance;

    public WalletStatusType Status { get; private set; }
    public IReadOnlyCollection<Transaction> Transactions => _transactions.AsReadOnly();
    public IReadOnlyCollection<HeldFund> HeldFunds => _heldFunds.AsReadOnly();
    public string CurrencyCode { get; private set; }

    protected Wallet()
    {

    }
    private Wallet(string currencyCode)
    {
        WithdrawableBalance = Money.Zero(currencyCode);
        NonWithdrawableBalance = Money.Zero(currencyCode);
        HeldBalance = Money.Zero(currencyCode);
        Status = WalletStatusType.Active;
        CurrencyCode = currencyCode;
    }

    public static Wallet CreateWallet(string currencyCode = "IRR")
    {
        return new Wallet(currencyCode);
    }

    public static Wallet EmptyWallet()
    {
        var wallet = new Wallet("IRR")
        {
            Status = WalletStatusType.DeActivate
        };
        return wallet;
    }

    public void DepositWithdrawable(Money amount, string description, string? operationId)
    {
        EnsureWalletIsActive();

        if (amount is null || amount.Amount == 0)
            throw new ArgumentException("Deposit amount cannot be zero.", nameof(amount));

        WithdrawableBalance += amount;
        _transactions.Add(Transaction.CreateDepositWithdrawable(Id, amount, description, operationId));
    }


    public void DepositNonWithdrawable(Money amount, string description, string? operationId)
    {
        EnsureWalletIsActive();
        if (amount is null || amount.Amount == 0)
            throw new ArgumentException("Deposit amount cannot be zero.", nameof(amount));

        NonWithdrawableBalance += amount;
        _transactions.Add(Transaction.CreateNonWithdrawableDeposit(Id, amount, description, operationId));
    }


    public void Withdraw(Money amount, string? operationId)
    {
        EnsureWalletIsActive();
        EnsureSufficientWithdrawableFunds(amount);

        WithdrawableBalance -= amount;

        _transactions.Add(Transaction.CreateWithdrawal(Id, amount, "Withdrawal from wallet", operationId));
    }

    public void Purchase(Money amount, string? operationId)
    {
        EnsureWalletIsActive();
        EnsureSufficientAvailableBalance(amount);

        var nonWithdrawableToSpend = amount.Min(NonWithdrawableBalance);
        var withdrawableToSpend = amount - nonWithdrawableToSpend;

        NonWithdrawableBalance -= nonWithdrawableToSpend;
        WithdrawableBalance -= withdrawableToSpend;

        _transactions.Add(Transaction.CreatePurchase(Id, amount, "Purchase", operationId));
    }


    public HeldFund BlockFunds(Money amount, string reason, string? operationId)
    {
        EnsureWalletIsActive();
        EnsureSufficientAvailableBalance(amount);

        var heldFund = new HeldFund(Id, amount, reason, operationId);
        _heldFunds.Add(heldFund);
        HeldBalance += amount;

        return heldFund;
    }


    public void SettleBlockedFund(Guid heldFundId, string? operationId)
    {
        var heldFund = GetActiveHeldFund(heldFundId);

        var amountToSettle = heldFund.Amount;
        var nonWithdrawableToSpend = amountToSettle.Min(NonWithdrawableBalance);
        var withdrawableToSpend = amountToSettle - nonWithdrawableToSpend;

        NonWithdrawableBalance -= nonWithdrawableToSpend;
        WithdrawableBalance -= withdrawableToSpend;

        HeldBalance -= amountToSettle;
        heldFund.FinalizeFund(operationId);

        _transactions.Add(Transaction.CreatePurchase(Id, amountToSettle, $"Settlement for hold: {heldFund.Id}", operationId));
    }

    public void ReleaseBlockedFund(Guid heldFundId, string? operationId)
    {
        var heldFund = GetActiveHeldFund(heldFundId);

        HeldBalance -= heldFund.Amount;
        heldFund.Release(operationId);
    }

    public void Freeze()
    {
        Status = WalletStatusType.Frozen;
    }

    public void Activate()
    {
        Status = WalletStatusType.Active;
    }

    private void EnsureWalletIsActive()
    {
        if (Status != WalletStatusType.Active)
            throw new WalletDeactivateException("Wallet is not active.");
    }

    private void EnsureSufficientAvailableBalance(Money amount)
    {
        if (AvailableBalance < amount)
            throw new InvalidBalanceException("Insufficient available funds.");
    }

    private void EnsureSufficientWithdrawableFunds(Money amount)
    {
        var availableWithdrawable = WithdrawableBalance - HeldBalance;
        if (availableWithdrawable < amount)
            throw new InvalidBalanceException("Insufficient withdrawable funds.");
    }

    private HeldFund GetActiveHeldFund(Guid heldFundId)
    {
        var heldFund = _heldFunds.FirstOrDefault(h => h.Id == heldFundId);
        if (heldFund == null)
            throw new Exception("Held fund not found.");
        if (heldFund.Status != HeldStatusType.Held)
            throw new Exception($"Held fund is not in a valid state for this operation. Current state: {heldFund.Status}");

        return heldFund;
    }
}