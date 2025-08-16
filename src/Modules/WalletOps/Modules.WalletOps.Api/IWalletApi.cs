using Framework.DataType;
using Modules.WalletOps.Application.Contracts;

namespace Modules.WalletOps.Api
{
    public interface IWalletApi
    {
        Task<ResultContract<WalletBalanceResponseContract>> TryGetCurrentUserBalanceAsync();
    }
}
