using BuildingBlocks.Domain.Context;
using BuildingBlocks.Domain.Data;
using Framework.DataType;
using Modules.WalletOps.Application.Contracts;
using Modules.WalletOps.Domain.Aggregates.WalletTrx;
using Modules.WalletOps.Domain.Aggregates.WalletTrx.Data;
using Modules.WalletOps.Domain.ValueObjects;

namespace Modules.WalletOps.Application.Aggregates.WalletTrx;

public class WalletApplication
    (IExecutionContextAccessor executionContextAccessor,
    IWalletRepository walletRepository,
    IUnitOfWork unitOfWork)
{
    public async Task<ResultContract<WalletPurchaseResponseContract>> PurchaseAsync(WalletPurchaseRequestContract requestContract)
    {
        return requestContract.PurchaseType switch
        {
            PurchaseType.Credit => await PurchaseCredit(requestContract),
            PurchaseType.Bank => PurchaseBank(requestContract),
            _ => throw new NotImplementedException(),
        };
    }

    private ResultContract<WalletPurchaseResponseContract> PurchaseBank(WalletPurchaseRequestContract requestContract)
    {
        throw new NotImplementedException();
    }

    private async Task<ResultContract<WalletPurchaseResponseContract>> PurchaseCredit(WalletPurchaseRequestContract requestContract)
    {
        // TODO: ADD LOCK
        var wallet = await GetCurrentUserWallet();
        // TODO : LOG
        Money heldAmount;
        Money bankAmount = Money.Zero(requestContract.Amount.Currency);
        bool generateBankLink = false;
        if (requestContract.Amount.ToMoney() > wallet.AvailableBalance)
        {
            // TODO : LOG
            bankAmount = requestContract.Amount.ToMoney() - wallet.AvailableBalance;
            heldAmount = wallet.AvailableBalance;
            generateBankLink = true;
        }
        else
        {
            // TODO : LOG
            heldAmount = requestContract.Amount.ToMoney();
        }

        // TODO : LOG
        var fund = wallet.BlockFunds(heldAmount, $"بلوکه کردن پول برای خرید رفرنس {requestContract.ReferenceId}");
        // TODO : LOG

        await unitOfWork.SaveChangesAsync();

        if (generateBankLink)
        {
            // TODO : LOG    
            var link = $"https://localhost:7052/Bank?Amount={bankAmount.Amount}";
            // TODO : LOG    

            return new WalletPurchaseResponseContract { RedirectLink = link };

        }

        // TODO : LOG
        wallet.SettleBlockedFund(fund.Id);
        await unitOfWork.SaveChangesAsync();
        // TODO : LOG

        return new WalletPurchaseResponseContract { RedirectLink = null };
    }

    public async Task<ResultContract<WalletBalanceResponseContract>> TryGetCurrentUserBalance()
    {
        var wallet = await GetCurrentUserWallet();
        return WalletBalanceResponseContract.FromWallet(wallet);
    }

    private async Task<Wallet> GetCurrentUserWallet()
    {
        var wallet = await walletRepository.TryGetCurrentUserBalance();
        if (wallet == null)
        {
            var newWallet = Wallet.CreateWallet();
            await walletRepository.AddAsync(newWallet);
            await unitOfWork.SaveChangesAsync();
            return newWallet;
        }

        return wallet;
    }
}
