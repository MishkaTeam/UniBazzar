using Microsoft.Extensions.DependencyInjection;
using Persistence.EFCore;

namespace Persistence.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAuditing(this IServiceCollection services)
    {
        services.AddScoped<AuditSaveChangesInterceptor>();
        return services;
    }
}
