using BuildingBlocks.Domain.Context;
using BuildingBlocks.Persistence;
using Framework.DataType;
using Microsoft.EntityFrameworkCore;
using Modules.WalletOps.Domain.Aggregates.WalletTrx;
using Modules.WalletOps.Domain.Aggregates.WalletTrx.Data;
using Persistence;

namespace Modules.WalletOps.Persistence.Repositories;

public class WalletRepository : RepositoryBase<Wallet>, IWalletRepository
{
    public WalletRepository(WalletOpsContext context, 
        IExecutionContextAccessor executionContext) : base(context, executionContext)
    {
    }

    public async Task<Wallet> TryGetCurrentUserBalance()
    {
        if (ExecutionContext.UserId.IsNullOrDefault()
            || ExecutionContext.StoreId.IsEmpty())
        {
            return Wallet.EmptyWallet();
        }

        var wallet = DbSet.FirstOrDefaultAsync(x => x.OwnerId == ExecutionContext.UserId);

        if (wallet == null)
        {
            DbSet.Add(Wallet.CreateWallet());
        }
        

    }
}
