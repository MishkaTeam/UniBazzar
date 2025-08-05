using BuildingBlocks.Domain.Aggregates;
using Modules.WalletOps.Domain.ValueObjects;

namespace Modules.WalletOps.Domain.Aggregates.WalletTrx;


/// <summary>
/// There is 3 main type that if any of them got any error during the action, 
/// the state goes to any of error types 
/// </summary>
internal enum HeldStatus
{
    Held,
    Released,
    Expire,
    Finalized,

    Held_Error,
    Release_Error,
    Expire_Error,
    Finalized_Error
}


internal class HeldFund : Entity
{
    public Guid WalletId { get; private set; }
    public Money Amount { get; private set; }
    public HeldStatus Status { get; private set; }
    public Wallet Wallet { get; private set; }


    private HeldFund() { } // For EF or serialization

    public HeldFund(Guid walletId, Money amount)
    {
        WalletId = walletId;
        Amount = amount;
        Status = HeldStatus.Held;
    }

    public void Release()
    {
        if (Status != HeldStatus.Held)
            throw new InvalidOperationException($"Cannot release fund in status {Status}");

        Status = HeldStatus.Released;
    }

    public void Release_Error()
    {
        Status = HeldStatus.Release_Error;
    }

    public void Expire()
    {
        if (Status != HeldStatus.Held)
            throw new InvalidOperationException($"Cannot expire fund in status {Status}");

        Status = HeldStatus.Expire;
    }

    public void ExpireError()
    {
        Status = HeldStatus.Expire_Error;
    }

    public void FinalizeFund()
    {
        if (Status != HeldStatus.Held)
            throw new InvalidOperationException($"Cannot finalize fund in status {Status}");

        Status = HeldStatus.Finalized;
    }

    public void ErrorFinalizeFund()
    {
        Status = HeldStatus.Finalized_Error;
    }


    public bool IsError => Status switch
    {
        HeldStatus.Held_Error or
        HeldStatus.Release_Error or
        HeldStatus.Expire_Error or
        HeldStatus.Finalized_Error => true,
        _ => false
    };

    public bool IsNormal => Status switch
    {
        HeldStatus.Released or
        HeldStatus.Expire or
        HeldStatus.Finalized => true,
        _ => false
    };

}
