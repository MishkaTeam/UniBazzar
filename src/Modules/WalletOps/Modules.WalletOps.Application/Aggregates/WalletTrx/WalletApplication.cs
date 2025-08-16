using Framework.DataType;
using Modules.WalletOps.Application.Contracts;
using Modules.WalletOps.Domain.Aggregates.WalletTrx.Data;

namespace Modules.WalletOps.Application.Aggregates.WalletTrx;

public class WalletApplication(IWalletRepository walletRepository)
{
    public async Task<ResultContract<WalletBalanceResponseContract>> GetCurrentUserBalance()
    {
        return walletRepository.TryGetCurrentUserBalance();
    }
}
