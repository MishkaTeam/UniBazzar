using Microsoft.AspNetCore.Http;
using Serilog.Core;
using Serilog.Events;

namespace Framework.Observability.Enrichers;

public class CustomContextEnricher : ILogEventEnricher
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly Func<IServiceProvider, IDictionary<string, object?>> _contextProvider;
   public CustomContextEnricher(
        IHttpContextAccessor httpContextAccessor,
        Func<IServiceProvider, IDictionary<string, object?>> contextProvider)
    {
        _httpContextAccessor = httpContextAccessor;
        _contextProvider = contextProvider;
    }

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext is null) return;

        var contextData = _contextProvider.Invoke(httpContext.RequestServices);

        if (contextData is null) return;

        foreach (var item in contextData)
        {
            var property = propertyFactory.CreateProperty(item.Key, item.Value);
            logEvent.AddPropertyIfAbsent(property);
        }
    }
}