using Application.Aggregates.Categories;
using Application.Aggregates.Customer;
using Application.Aggregates.Products;
using Application.Aggregates.ShippingAddress;
using Application.Aggregates.Stores;
using Application.Aggregates.Units;
using Application.Aggregates.Users;
using Domain;
using Domain.Aggregates.Categories;
using Domain.Aggregates.Customers;
using Domain.Aggregates.Products;
using Domain.Aggregates.Products.ProductFeatures;
using Domain.Aggregates.ShippingAddress;
using Domain.Aggregates.Stores;
using Domain.Aggregates.Units;
using Domain.Aggregates.Users;
using Persistence;
using Persistence.Aggregates.Categories;
using Persistence.Aggregates.Customer;
using Persistence.Aggregates.Products;
using Persistence.Aggregates.ShippingAddress;
using Persistence.Aggregates.Stores;
using Persistence.Aggregates.Units;
using Persistence.Aggregates.Users;

namespace Server.Infrastructure.Extensions.ServiceCollections;

public static class DomainExtensions
{
	public static IServiceCollection AddDomainApplications(this IServiceCollection services)
	{
		services.AddScoped<StoresApplication>();

		services.AddScoped<ProductsApplication>();

		services.AddScoped<UnitsApplication>();

		services.AddScoped<CategoriesApplication>();

		services.AddScoped<CategoryRepository>();
		
		services.AddScoped<CustomerApplication>();
		
		services.AddScoped<ShippingAddressApplication>();

		services.AddScoped<UserApplication>();
		return services;
	}

	public static IServiceCollection AddDomainRepositories(this IServiceCollection services)
	{
		services.AddScoped<IStoreRepository, StoreRepository>();

		services.AddScoped<IProductRepository, ProductRepository>();
		
		services.AddScoped<IProductFeatureRepository, ProductRepository>();

		services.AddScoped<IUnitRepository, UnitRepository>();

		services.AddScoped<ICategoryRepository, CategoryRepository>();

		services.AddScoped<ICustomerRepository, CustomerRepository>();
		
		services.AddScoped<IShippingAddressRepository, ShippingAddressRepository>();

		services.AddScoped<IUserRepository, UserRepository>();

		return services;
	}

	public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
	{
		services.AddScoped<IUnitOfWork, UnitOfWork>();

		return services;
	}
}