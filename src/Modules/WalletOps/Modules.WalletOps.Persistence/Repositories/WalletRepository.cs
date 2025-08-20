using BuildingBlocks.Domain.Context;
using BuildingBlocks.Persistence;
using Framework.DataType;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Modules.WalletOps.Domain.Aggregates.WalletTrx;
using Modules.WalletOps.Domain.Aggregates.WalletTrx.Data;

namespace Modules.WalletOps.Persistence.Repositories;

public class WalletRepository(WalletOpsDbContext context,
    IExecutionContextAccessor executionContext,
    ILogger<WalletRepository> logger) : RepositoryBase<Wallet>(context, executionContext), IWalletRepository
{
    public async Task<Wallet?> TryGetCurrentUserBalance()
    {
        if (ExecutionContext.UserId.IsNullOrDefault()
    || ExecutionContext.StoreId.IsEmpty())
        {
            return null;
        }

        var wallet = await DbSet.FirstOrDefaultAsync(x => x.OwnerId == ExecutionContext.UserId);

        if (wallet == null)
        {
            logger.LogWarning("No Wallet found for user");
        }

        return wallet;
    }
}
