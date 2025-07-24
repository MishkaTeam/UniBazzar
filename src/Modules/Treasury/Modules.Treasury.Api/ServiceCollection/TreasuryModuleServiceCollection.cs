using Microsoft.EntityFrameworkCore;
using Modules.Treasury.Api.TreasuryAbstraction;
using Modules.Treasury.Api.TreasuryApi;
using Modules.Treasury.Application.Aggregates;
using Modules.Treasury.Domain;
using Modules.Treasury.Domain.Aggregates.Counterparties.Data;
using Modules.Treasury.Domain.Aggregates.Receipts.Data;
using Modules.Treasury.Persistence;
using Modules.Treasury.Persistence.Repositories;

namespace Modules.Treasury.Api.ServiceCollection;

public static class TreasuryModuleServiceCollection
{
    public static IServiceCollection AddTreasuryModule(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<TreasuryDbContext>(opt =>
        {
            var connection = connectionString;
            opt.UseSqlite(connection);
            opt.EnableSensitiveDataLogging();
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IReceiptRepository, ReceiptRepository>();
        services.AddScoped<IReceiptsApi, ReceiptsApi>();
        services.AddScoped<ReceiptsApplication>();

        services.AddScoped<ICounterpartyRepository, CounterpartyRepository>();

        return services;
    }
}
