using BuildingBlocks.Domain.Aggregates;
using Modules.Wallet.Domain.Entities;
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

    public Guid OwnerId { get; private set; } // Can be UserId or OrganizationId
    public WalletOwnerType OwnerType { get; private set; }
    public Money Balance { get; private set; }
    public WalletStatus Status { get; private set; }
    public IReadOnlyCollection<Transaction> Transactions => _transactions.AsReadOnly();

    private Wallet(WalletId id, Guid ownerId, WalletOwnerType ownerType, Money initialBalance) : base(id)
    {
        OwnerId = ownerId;
        OwnerType = ownerType;
        Balance = initialBalance;
        Status = WalletStatus.Active;
    }

    public static Wallet CreatePersonalWallet(Guid userId, Money initialBalance)
    {
        var wallet = new Wallet(WalletId.CreateNew(), userId, WalletOwnerType.Personal, initialBalance);
        wallet.AddDomainEvent(new WalletCreated(wallet.Id, wallet.OwnerId, wallet.OwnerType));
        return wallet;
    }

    public static Wallet CreateOrganizationWallet(OrganizationId organizationId, Money initialBalance)
    {
        var wallet = new Wallet(WalletId.CreateNew(), organizationId.Value, WalletOwnerType.Organization, initialBalance);
        wallet.AddDomainEvent(new WalletCreated(wallet.Id, wallet.OwnerId, wallet.OwnerType));
        return wallet;
    }

    public void Deposit(Money amount)
    {
        EnsureWalletIsActive();

        Balance += amount;
        _transactions.Add(Transaction.CreateDeposit(Id, amount));

        AddDomainEvent(new WalletCharged(Id, amount));
    }

    public void Withdraw(Money amount)
    {
        EnsureWalletIsActive();
        EnsureSufficientFunds(amount);

        Balance -= amount;
        _transactions.Add(Transaction.CreateWithdrawal(Id, amount));

        AddDomainEvent(new WalletWithdrawn(Id, amount));
    }

    internal void HoldFundsForTransfer(Money amount, TransferId transferId)
    {
        EnsureWalletIsActive();
        EnsureSufficientFunds(amount);

        Balance -= amount;
        _transactions.Add(Transaction.CreateTransfer(Id, amount, TransactionType.TransferOut, transferId));

        AddDomainEvent(new FundsHeldForTransfer(Id, transferId, amount));
    }

    internal void ReceiveTransferredFunds(Money amount, TransferId transferId)
    {
        EnsureWalletIsActive();

        Balance += amount;
        _transactions.Add(Transaction.CreateTransfer(Id, amount, TransactionType.TransferIn, transferId));

        AddDomainEvent(new FundsReceivedFromTransfer(Id, transferId, amount));
    }

    internal void RevertHeldFunds(Money amount, TransferId transferId)
    {
        // This operation can be performed even on a frozen wallet to ensure consistency
        Balance += amount;
        // Optionally, add a reversing transaction or log this event

        AddDomainEvent(new TransferFailedAndFundsReverted(Id, transferId, amount));
    }

    public void Freeze()
    {
        if (Status == WalletStatus.Frozen) return;
        Status = WalletStatus.Frozen;
        AddDomainEvent(new WalletStatusChanged(Id, Status));
    }

    public void Activate()
    {
        if (Status == WalletStatus.Active) return;
        Status = WalletStatus.Active;
        AddDomainEvent(new WalletStatusChanged(Id, Status));
    }

    private void EnsureWalletIsActive()
    {
        if (Status != WalletStatus.Active)
            throw new DomainException("Operation cannot be performed on a non-active wallet.");
    }

    private void EnsureSufficientFunds(Money amount)
    {
        if (Balance.IsGreaterThanOrEqualTo(amount)) return;

        AddDomainEvent(new InsufficientFundsAttempted(Id, amount));
        throw new DomainException("Insufficient funds for this operation.");
    }

    // For EF Core
    private Wallet() { }
}