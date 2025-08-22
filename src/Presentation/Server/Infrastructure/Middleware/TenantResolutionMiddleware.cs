using Domain.Aggregates.Stores;

namespace Server.Infrastructure.Middleware
{
    public class TenantResolutionMiddleware
    {
        private readonly RequestDelegate _next;

        public TenantResolutionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IStoreRepository storeRepository)
        {
            try
            {
                var host = context.Request.Host.Value;
                var storeId = await storeRepository.GetStoreByHostUrlAsync(host);
                
                if (storeId == Guid.Empty)
                {
                    // For development/testing, use a default store ID
                    // In production, you might want to redirect or show an error page
                    context.Items["TenantId"] = Guid.Parse("00000000-0000-0000-0000-000000000000");
                }
                else
                {
                    context.Items["TenantId"] = storeId;
                }
            }
            catch (Exception ex)
            {
                // Log the error but don't break the application
                // Use default store ID for development/testing
                context.Items["TenantId"] = Guid.Parse("00000000-0000-0000-0000-000000000000");
            }

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
}
    