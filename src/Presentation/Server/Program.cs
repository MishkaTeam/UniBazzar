using Blazored.LocalStorage;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Extensions;
using Server.Infrastructure;
using Server.Infrastructure.Extensions.ServiceCollections;
using Server.Infrastructure.Extentions.ServiceCollections;
using Server.Infrastructure.Middleware;


namespace Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.AddConfiguration();

            var services = builder.Services;

            services.AddRazorPagesWithAuth();
            services.AddControllers();
            services.AddServerSideBlazor()
            .AddInteractiveServerComponents();
            services.AddBlazorBootstrap();
            services.AddBlazoredLocalStorage();
            services.AddAuthenticationCookie();
            services.AddHttpClient();
            services.AddDomainApplications();
            services.AddHttpContextAccessor();
            services.AddScoped<IExecutionContextAccessor, ExecutionContextAccessor>();
            services.AddDomainRepositories();
            services.AddAuditing();

            services.AddUnitOfWork();
            services.AddDbContext<UniBazzarContext>(opt =>
            {
                var connection = builder.Configuration.GetConnectionString("DefaultConnection");
                opt.UseNpgsql(connection);
                opt.EnableSensitiveDataLogging();
            });

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
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
