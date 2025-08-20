using Blazored.LocalStorage;
using Blazored.SessionStorage;
using BuildingBlocks.Domain.Context;
using BuildingBlocks.Persistence.Extensions;
using Framework.Observability;
using Framework.Storage;
using Microsoft.EntityFrameworkCore;
using Modules.Treasury.Api.ServiceCollection;
using Persistence;
using Server.Infrastructure;
using Server.Infrastructure.Extensions.ServiceCollections;
using Server.Infrastructure.Extentions.ServiceCollections;
using Server.Infrastructure.Middleware;
using Server.Infrastructure.Services;
using Modules.WalletOps.Api.ServiceCollection;
using System.Reflection;

namespace Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            Assembly entryAssembly = Assembly.GetExecutingAssembly();

            builder.AddObservability(entryAssembly.GetName().Name ?? "UniBazzar", entryAssembly.GetName().Version?.ToString() ?? "1.0.0",
                enrichment =>
            {
                enrichment.WithCustomContext(serviceProvider =>
                {
                    var executionContext = serviceProvider.GetService<IExecutionContextAccessor>();

                    if (executionContext != null)
                    {
                        return new Dictionary<string, object?>
                        {
                            { "StoreId", executionContext.StoreId },
                            { "UserId", executionContext.UserId.HasValue ? executionContext.UserId.Value : "NotAuthenticated" }
                        };
                    }

                    return new Dictionary<string, object?>();
                });
            });


            builder.AddConfiguration();

            var services = builder.Services;

            // Add Razor Pages
            services.AddRazorPagesWithAuth();

            // Add Api Controllers
            services.AddControllers();

            // Add Blazor Server
            services.AddServerSideBlazor()
                    .AddInteractiveServerComponents();
            services.AddBlazorBootstrap();
            services.AddBlazoredLocalStorage();
            services.AddBlazoredSessionStorage();

            // Add General Services
            services.AddAuthenticationCookie();
            services.AddHttpContextAccessor();
            services.AddHttpClient();

            services.AddDomainApplications();
            services.AddDomainRepositories();
            services.AddUnitOfWork();
            services.AddScoped<IExecutionContextAccessor, ExecutionContextAccessor>();
            services.AddScoped<StorageService>();

            services.AddAuditing();
            services.AddS3Storage(new StorageConfig()
            {
                Endpoint = builder.Configuration.GetSection("StorageConfig:Endpoint")?.Value,
                AccessKey = builder.Configuration.GetSection("StorageConfig:AccessKey")?.Value,
                SecretKey = builder.Configuration.GetSection("StorageConfig:SecretKey")?.Value,

            });
            string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<UniBazzarContext>(opt =>
            {
                var connection = connectionString;
                opt.UseNpgsql(connection);
                opt.EnableSensitiveDataLogging();
            });

            services.AddTreasuryModule(connectionString);
            services.AddWalletOpsModule(connectionString);
            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseObservability();

            app.UseStaticFiles();
            app.UseCultureHandler();
            app.UseTenantResolution();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapBlazorHub();
            app.MapControllers();
            app.MapRazorPages();

            app.Run();
        }
    }
}
