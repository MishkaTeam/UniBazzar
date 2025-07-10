using BuildingBlocks.Persistence.EFCore;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Persistence.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAuditing(this IServiceCollection services)
    {
        services.AddScoped<AuditSaveChangesInterceptor>();
        services.AddScoped<StoreIdSaveChangesInterceptor>();
        services.AddScoped<OwnerIdSaveChangesInterceptor>();
        services.AddScoped<StoreQueryInterceptor>();

        return services;
    }
}
