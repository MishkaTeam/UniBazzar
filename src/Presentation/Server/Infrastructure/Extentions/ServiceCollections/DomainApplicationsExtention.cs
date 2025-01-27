using Application.Aggregates.Products;
using Application.Aggregates.Units;
using Domain;
using Domain.Aggregates.Products;
using Domain.Aggregates.Products.ProductFeatures;
using Domain.Aggregates.Units;
using Persistence;
using Persistence.Aggregates.Products;
using Persistence.Aggregates.Units;

namespace Server.Infrastructure.Extentions.ServiceCollections;

public static class DomainExtensions
{
	public static IServiceCollection AddDomainApplications(this IServiceCollection services)
	{
		services.AddScoped<UnitsApplication>();
		services.AddScoped<ProductsApplication>();
		return services;
	}

	public static IServiceCollection AddDomainRepositories(this IServiceCollection services)
	{
		services.AddScoped<IUnitRepository, UnitRepository>();
		services.AddScoped<IProductRepository, ProductRepository>();
		services.AddScoped<IProductFeatureRepository, ProductRepository>();
		return services;
	}

	public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
	{
		services.AddScoped<IUnitOfWork, UnitOfWork>();
		return services;
	}
}
