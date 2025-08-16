using BuildingBlocks.Domain.Data;

namespace Persistence;

public class UnitOfWork(WalletOpsContext walletOpsContext) : IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken? cancellationToken = null)
    {
        if (cancellationToken.HasValue)
            return walletOpsContext.SaveChangesAsync(cancellationToken.Value);
        else
            return walletOpsContext.SaveChangesAsync();
    }
}
