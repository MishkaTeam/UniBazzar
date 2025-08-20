using BuildingBlocks.Domain.Data;

namespace Modules.WalletOps.Persistence;

public class UnitOfWork(WalletOpsDbContext walletOpsContext) : IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken? cancellationToken = null)
    {
        if (cancellationToken.HasValue)
            return walletOpsContext.SaveChangesAsync(cancellationToken.Value);
        else
            return walletOpsContext.SaveChangesAsync();
    }
}
