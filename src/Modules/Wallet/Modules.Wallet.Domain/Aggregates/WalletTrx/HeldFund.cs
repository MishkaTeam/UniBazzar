using BuildingBlocks.Domain.Aggregates;
using Modules.WalletOps.Domain.ValueObjects;

namespace Modules.WalletOps.Domain.Aggregates.WalletTrx;


public class HeldFund : Entity
{
    public Guid WalletId { get; private set; }
    public Money Amount { get; private set; }
    public string Reason { get; private set; }
    public HeldStatusType Status { get; private set; }

    private HeldFund() { } // For EF Core

    public HeldFund(Guid walletId, Money amount, string reason)
    {
        WalletId = walletId;
        Amount = amount;
        Reason = reason;
        Status = HeldStatusType.Held;
    }

    public void Release()
    {
        EnsureIsHeld();
        Status = HeldStatusType.Released;
    }

    public void FinalizeFund()
    {
        EnsureIsHeld();
        Status = HeldStatusType.Finalized;
    }

    private void EnsureIsHeld()
    {
        if (Status != HeldStatusType.Held)
            throw new InvalidOperationException($"Cannot perform this action on a fund with status {Status}");
    }
}