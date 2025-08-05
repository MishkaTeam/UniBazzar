using BuildingBlocks.Domain.Aggregates;
using Modules.WalletOps.Domain.Aggregates.WalletTrx.Enums;
using Modules.WalletOps.Domain.ValueObjects;

namespace Modules.WalletOps.Domain.Aggregates.WalletTrx;


public class HeldFund : Entity
{
    public Guid WalletId { get; private set; }
    public Money Amount { get; private set; }
    public HeldStatusType Status { get; private set; }
    public Wallet Wallet { get; private set; }


    private HeldFund() { } // For EF or serialization

    public HeldFund(Guid walletId, Money amount)
    {
        WalletId = walletId;
        Amount = amount;
        Status = HeldStatusType.Held;
    }

    public void Release()
    {
        if (Status != HeldStatusType.Held)
            throw new InvalidOperationException($"Cannot release fund in status {Status}");

        Status = HeldStatusType.Released;
    }

    public void Release_Error()
    {
        Status = HeldStatusType.Release_Error;
    }

    public void Expire()
    {
        if (Status != HeldStatusType.Held)
            throw new InvalidOperationException($"Cannot expire fund in status {Status}");

        Status = HeldStatusType.Expire;
    }

    public void ExpireError()
    {
        Status = HeldStatusType.Expire_Error;
    }

    public void FinalizeFund()
    {
        if (Status != HeldStatusType.Held)
            throw new InvalidOperationException($"Cannot finalize fund in status {Status}");

        Status = HeldStatusType.Finalized;
    }

    public void ErrorFinalizeFund()
    {
        Status = HeldStatusType.Finalized_Error;
    }


    public bool IsError => Status switch
    {
        HeldStatusType.Held_Error or
        HeldStatusType.Release_Error or
        HeldStatusType.Expire_Error or
        HeldStatusType.Finalized_Error => true,
        _ => false
    };

    public bool IsNormal => Status switch
    {
        HeldStatusType.Released or
        HeldStatusType.Expire or
        HeldStatusType.Finalized => true,
        _ => false
    };

}
