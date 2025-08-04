using Modules.Wallet.Domain.Aggregates;
using Modules.Wallet.Domain.Exceptions;

namespace Modules.Wallet.Domain.Services;

// Domain/Services/TransferService.cs
public class TransferService
{
    public Transfer InitiateTransfer(Wallet sourceWallet, Wallet destinationWallet, Money amount)
    {
        // 1. Create the transfer record first to represent the intent
        var transfer = Transfer.Initiate(sourceWallet, destinationWallet, amount);

        // 2. Execute the operation on the source wallet
        try
        {
            sourceWallet.HoldFundsForTransfer(amount, transfer.Id);
            // If this succeeds, the system is in a consistent state.
            // The transfer is pending and funds are held.
            return transfer;
        }
        catch (DomainException)
        {
            // If holding funds fails, we fail the transfer immediately.
            transfer.Fail();
            // The TransferFailed event will be handled to notify, but no funds need to be reverted yet.
            throw; // Re-throw the original exception
        }
    }

    public void FinalizeTransfer(Transfer transfer, Wallet sourceWallet, Wallet destinationWallet)
    {
        if (transfer.Status != TransferStatus.Pending)
            throw new InvalidOperationException("Transfer is not pending.");

        try
        {
            // 3. Complete the operation on the destination wallet
            destinationWallet.ReceiveTransferredFunds(transfer.Amount, transfer.Id);

            // 4. Mark the transfer as complete
            transfer.Complete();
        }
        catch (DomainException)
        {
            // If receiving funds fails, we must fail the transfer and revert the funds.
            transfer.Fail();
            sourceWallet.RevertHeldFunds(transfer.Amount, transfer.Id);
            throw; // Re-throw the exception
        }
    }
}