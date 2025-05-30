using Domain.Aggregates.Stores;

namespace Server.Infrastructure.Middleware;

public class TenantResolutionMiddleware
{
    private readonly RequestDelegate _next;

    public TenantResolutionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IStoreRepository storeRepository)
    {
        var host = context.Request.Host.Value;
        var storeId = await storeRepository.GetStoreByHostUrlAsync(host); // فقط ID، نه Entity
        if (storeId == Guid.Empty)
            throw new Exception("Tenant not found");

        context.Items["TenantId"] = storeId;

        await _next(context);
    }
}


public static class UseTenantResolutionExtensions
{
    public static IApplicationBuilder UseTenantResolution(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<TenantResolutionMiddleware>();
    }
}
    