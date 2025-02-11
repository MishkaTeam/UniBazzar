using Microsoft.EntityFrameworkCore;
using Persistence;
using Server.Infrastructure.Extensions.ServiceCollections;
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

			services.AddRazorPages();
			services.AddServerSideBlazor()
            .AddInteractiveServerComponents();
			services.AddDomainApplications();
            services.AddDomainRepositories();
			services.AddUnitOfWork();
            services.AddDbContext<UniBazzarContext>(opt => opt.UseSqlite("Data Source=Database.db"));

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

            app.UseAuthorization();

			app.MapBlazorHub();
            app.MapRazorPages();

            app.Run();
        }
    }
}
