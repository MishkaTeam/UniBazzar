using BuildingBlocks.Domain.Aggregates;
using Modules.Wallet.Domain.Exceptions;
using Modules.Wallet.Domain.ValueObjects;

namespace Modules.Wallet.Domain.Aggregates;

// Domain/Aggregates/Transfer.cs
public enum TransferStatus
{
    Pending,
    Completed,
    Failed,
    Cancelled
}

public class Transfer : Entity
{
    public Guid SourceWalletId { get; private set; }
    public Guid DestinationWalletId { get; private set; }
    public Money Amount { get; private set; }
    public TransferStatus Status { get; private set; }
    public DateTime InitiatedAtUtc { get; private set; }
    public DateTime? FinalizedAtUtc { get; private set; }

    private Transfer(TransferId id, Guid sourceWalletId, Guid destinationWalletId, Money amount) 
    {
        if (sourceWalletId == destinationWalletId)
            throw new DomainException("Source and destination wallets cannot be the same.");

        SourceWalletId = sourceWalletId;
        DestinationWalletId = destinationWalletId;
        Amount = amount;
        Status = TransferStatus.Pending;
        InitiatedAtUtc = DateTime.UtcNow;
    }

    public static Transfer Initiate(Wallet sourceWallet, Wallet destinationWallet, Money amount)
    {
        // The business logic of holding funds is inside the wallet,
        // but it's triggered from here.
        sourceWallet.HoldFundsForTransfer(amount, TransferId.CreateNew());

        // This assumes the wallet's event will create the transfer.
        // A Domain Service is a better fit here to coordinate. Let's create one.
        // For now, let's keep it simple. We will refactor to a Domain Service.

        // Refactored approach: The Transfer is created first, then a service coordinates.
        return new Transfer(TransferId.CreateNew(), sourceWallet.Id, destinationWallet.Id, amount);
    }

    public void Complete()
    {
        if (Status != TransferStatus.Pending)
            throw new DomainException("Only pending transfers can be completed.");

        Status = TransferStatus.Completed;
        FinalizedAtUtc = DateTime.UtcNow;
    }

    public void Fail()
    {
        if (Status != TransferStatus.Pending)
            throw new DomainException("Only pending transfers can fail.");

        Status = TransferStatus.Failed;
        FinalizedAtUtc = DateTime.UtcNow;
    }

    // For EF Core
    private Transfer() { }
}
