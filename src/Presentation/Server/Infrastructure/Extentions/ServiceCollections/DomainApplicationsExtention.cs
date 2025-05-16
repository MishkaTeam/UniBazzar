using Application.Aggregates.Branches;
using Application.Aggregates.Categories;
using Application.Aggregates.CheckoutCounter;
using Application.Aggregates.Customers;
using Application.Aggregates.Customers.ShippingAddresses;
using Application.Aggregates.Products;
using Application.Aggregates.Stores;
using Application.Aggregates.Units;
using Application.Aggregates.Users;
using Application.CustomerSearch;
using Application.ProductSearch;
using Domain;
using Domain.Aggregates.branches;
using Domain.Aggregates.Categories;
using Domain.Aggregates.CheckoutCounter;
using Domain.Aggregates.Customers;
using Domain.Aggregates.Customers.ShippingAddresses;
using Domain.Aggregates.Products;
using Domain.Aggregates.Products.ProductFeatures;
using Domain.Aggregates.Products.ProductImages;
using Domain.Aggregates.Products.ProductPriceLists;
using Domain.Aggregates.Stores;
using Domain.Aggregates.Units;
using Domain.Aggregates.Users;
using Domain.CustomerSearch.Data;
using Domain.ProductSearch.Data;
using Persistence;
using Persistence.Aggregates.Branches;
using Persistence.Aggregates.Categories;
using Persistence.Aggregates.CheckoutCounters;
using Persistence.Aggregates.Customers;
using Persistence.Aggregates.Products;
using Persistence.Aggregates.ShippingAddresses;
using Persistence.Aggregates.Stores;
using Persistence.Aggregates.Units;
using Persistence.Aggregates.Users;
using Persistence.CustomerSearch;
using Persistence.ProductSearch;

namespace Server.Infrastructure.Extensions.ServiceCollections;

public static class DomainExtensions
{
    public static IServiceCollection AddDomainApplications(this IServiceCollection services)
    {
        services.AddScoped<UserApplication>();

        services.AddScoped<StoresApplication>();
        services.AddScoped<BranchesApplication>();

        services.AddScoped<CustomerApplication>();
        services.AddScoped<ShippingAddressApplication>();

        services.AddScoped<ProductsApplication>();
        services.AddScoped<ProductImagesApplication>();
        services.AddScoped<ProductFeaturesApplication>();
        services.AddScoped<ProductPriceListsApplication>();

        services.AddScoped<UnitsApplication>();

        services.AddScoped<CategoriesApplication>();

        services.AddScoped<CategoryRepository>();


        services.AddScoped<ProductSearchApplication>();
        services.AddScoped<CustomerSearchApplication>();

		services.AddScoped<UserApplication>();

		services.AddScoped<CheckoutCounterApplication>();

		return services;
	}

    public static IServiceCollection AddDomainRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IStoreRepository, StoreRepository>();
        services.AddScoped<IbranchRepository, BranchRepository>();

        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IShippingAddressRepository, ShippingAddressRepository>();

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductFeatureRepository, ProductFeaturesRepository>();
        services.AddScoped<IProductPriceListRepository, ProductPriceListsRepository>();
        services.AddScoped<IProductImageRepository, ProductImagesRepository>();

        services.AddScoped<IUnitRepository, UnitRepository>();

        services.AddScoped<ICategoryRepository, CategoryRepository>();


        services.AddScoped<IProductSearchRepository, ProductSearchRepository>();

        services.AddScoped<ICheckoutCounterRepository, CheckoutCounterRepository>();

        services.AddScoped<ICustomerSearchRepository, CustomerSearchRepository>();

        return services;
	}

    public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}