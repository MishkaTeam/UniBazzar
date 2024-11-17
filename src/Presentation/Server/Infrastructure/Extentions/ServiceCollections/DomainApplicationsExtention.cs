using Application.Aggregates.Units;
using Domain;
using Domain.Aggregates.Units;
using Persistence;
using Persistence.Aggregates.Units;

namespace Server.Infrastructure.Extentions.ServiceCollections;

public static class DomainExtensions
{
	public static IServiceCollection AddDomainApplications(this IServiceCollection services)
	{
		services.AddScoped<UnitsApplication>();
		return services;
	}

	public static IServiceCollection AddDomainRepositories(this IServiceCollection services)
	{
		services.AddScoped<IUnitRepository,UnitRepository>();
		return services;
	}

	public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
	{
		services.AddScoped<IUnitOfWork, UnitOfWork>();
		return services;
	}
}
