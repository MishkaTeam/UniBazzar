using BuildingBlocks.Domain.Data;

namespace Modules.WalletOps.Domain.Aggregates.WalletTrx.Data;

public interface IWalletRepository : IRepositoryBase<Wallet>
{
    Task<Wallet?> TryGetCurrentUserBalance();
}
