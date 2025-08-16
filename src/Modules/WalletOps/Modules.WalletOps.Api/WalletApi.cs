using Framework.DataType;
using Modules.WalletOps.Application.Aggregates.WalletTrx;
using Modules.WalletOps.Application.Contracts;

namespace Modules.WalletOps.Api;

internal class WalletApi(WalletApplication walletApplication) : IWalletApi
{
    public Task<ResultContract<WalletBalanceResponseContract>> GetCurrentUserBalance()
    {
        return walletApplication.GetCurrentUserBalance();
    }
}
