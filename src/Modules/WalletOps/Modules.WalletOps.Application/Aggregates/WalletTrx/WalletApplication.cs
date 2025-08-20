using BuildingBlocks.Domain.Context;
using BuildingBlocks.Domain.Data;
using Framework.DataType;
using Microsoft.Extensions.Logging;
using Modules.WalletOps.Application.Contracts;
using Modules.WalletOps.Domain.Aggregates.WalletTrx;
using Modules.WalletOps.Domain.Aggregates.WalletTrx.Data;
using Modules.WalletOps.Domain.ValueObjects;
using OpenTelemetry.Trace;

namespace Modules.WalletOps.Application.Aggregates.WalletTrx;

public class WalletApplication
    (IExecutionContextAccessor executionContextAccessor,
    IWalletRepository walletRepository,
    IUnitOfWork unitOfWork,
    ILogger<WalletApplication> logger)
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
        logger.LogInformation("Get Current User Wallet with Id {UserId} and {WalletId}", executionContextAccessor.UserId, wallet.Id);
        Money heldAmount;
        Money bankAmount = Money.Zero(requestContract.Amount.Currency);
        bool generateBankLink = false;
        if (requestContract.Amount.ToMoney() > wallet.AvailableBalance)
        {
            logger
                .LogInformation("Available balance is less than purchase amount | AvailableBalance: {AvailableBalance}, | purchase amount: {PurchaseAmount}"
                , wallet.AvailableBalance
                , requestContract.Amount);

            bankAmount = requestContract.Amount.ToMoney() - wallet.AvailableBalance;
            heldAmount = wallet.AvailableBalance;
            generateBankLink = true;
        }
        else
        {
            logger
                .LogInformation("All Available Balance will be held | AvailableBalance: {AvailableBalance}", wallet.AvailableBalance);
            
            heldAmount = requestContract.Amount.ToMoney();
        }

        var fund = wallet.BlockFunds(heldAmount, $"بلوکه کردن پول برای خرید رفرنس {requestContract.ReferenceId}", requestContract.OperationId);

        logger
            .LogInformation("{HeldFundAmount} will be held", fund.Amount);

        await unitOfWork.SaveChangesAsync();

        if (generateBankLink)
        {
            var link = $"https://localhost:7052/Bank?Amount={bankAmount.Amount}";
            logger
                .LogInformation("GenerateBankLink is {GenerateBankLink} Link Generated : {Link}", generateBankLink, link);

            return new WalletPurchaseResponseContract { RedirectLink = link };
        }

        wallet.SettleBlockedFund(fund.Id, requestContract.OperationId);
        await unitOfWork.SaveChangesAsync();
        logger
            .LogInformation("SettleBlockedFund For {RequestId} With {Amount}", requestContract.ReferenceId, fund.Amount);

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
