using BuildingBlocks.Domain.Data;
using Framework.DataType;
using Modules.WalletOps.Application.Contracts;
using Modules.WalletOps.Domain.Aggregates.WalletTrx;
using Modules.WalletOps.Domain.Aggregates.WalletTrx.Data;

namespace Modules.WalletOps.Application.Aggregates.WalletTrx;

public class WalletApplication(IWalletRepository walletRepository, IUnitOfWork unitOfWork)
{
    public async Task<ResultContract<WalletBalanceResponseContract>> TryGetCurrentUserBalance()
    {
        var wallet = await walletRepository.TryGetCurrentUserBalance();
        if (wallet == null)
        {
            var newWallet = Wallet.CreateWallet();
            await walletRepository.AddAsync(newWallet);
            await unitOfWork.SaveChangesAsync();
            return WalletBalanceResponseContract.FromWallet(newWallet);
        }

        return WalletBalanceResponseContract.FromWallet(wallet);
    }
}
