using BuildingBlocks.Domain.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Modules.WalletOps.Application.Aggregates.WalletTrx;
using Modules.WalletOps.Domain.Aggregates.WalletTrx.Data;
using Modules.WalletOps.Persistence;
using Modules.WalletOps.Persistence.Repositories;

namespace Modules.WalletOps.Api.ServiceCollection;

public static class WalletOpsModuleServiceCollection
{
    public static IServiceCollection AddWalletOpsModule(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<WalletOpsDbContext>(opt =>
        {
            var connection = connectionString;
            opt.UseNpgsql(connection);
            opt.EnableSensitiveDataLogging();
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IWalletRepository, WalletRepository>();
        services.AddScoped<IWalletApi, WalletApi>();
        services.AddScoped<WalletApplication>();

        return services;
    }
}
