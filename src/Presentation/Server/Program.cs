using Microsoft.EntityFrameworkCore;
using Persistence;
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
            services.AddServerSideBlazor()
            .AddInteractiveServerComponents();
            services.AddAuthenticationCookie();
            services.AddHttpClient();
            services.AddDomainApplications();
            services.AddHttpContextAccessor();
            services.AddScoped<IExecutionContextAccessor, ExecutionContextAccessor>();
            services.AddDomainRepositories();
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

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapBlazorHub();
            app.MapRazorPages();

            app.Run();
        }
    }
}
