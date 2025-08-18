using BuildingBlocks.Domain.Aggregates;
using Modules.WalletOps.Domain.ValueObjects;

namespace Modules.WalletOps.Domain.Aggregates.WalletTrx;


public class HeldFund : Entity
{
    public Guid WalletId { get; private set; }
    public Money Amount { get; private set; }
    public string Reason { get; private set; }
    public HeldStatusType Status { get; private set; }

    /// <summary>
    /// این پراپرتی میتواند نال باشد
    /// اما وقتی پر میشود نمیتواند تکراری باشد
    /// </summary>
    public string? OperationId { get; private set; }

    private HeldFund() { } // For EF Core

    public HeldFund(Guid walletId, Money amount, string reason, string? operationId)
    {
        WalletId = walletId;
        Amount = amount;
        Reason = reason;
        Status = HeldStatusType.Held;
        OperationId = operationId;
    }

    public void Release(string? operationId)
    {
        EnsureIsHeld();
        Status = HeldStatusType.Released;
        OperationId = operationId;
    }

    public void FinalizeFund(string? operationId)
    {
        EnsureIsHeld();
        Status = HeldStatusType.Finalized;
        OperationId = operationId;
    }

    private void EnsureIsHeld()
    {
        if (Status != HeldStatusType.Held)
            throw new InvalidOperationException($"Cannot perform this action on a fund with status {Status}");
    }
}