using Modules.Treasury.Domain;
using Modules.Treasury.Persistence;

namespace Modules.Treasury.Api.ServiceCollection;

public static class TreasuryModuleServiceCollection
{
    public static IServiceCollection AddTreasuryModule(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }

}
