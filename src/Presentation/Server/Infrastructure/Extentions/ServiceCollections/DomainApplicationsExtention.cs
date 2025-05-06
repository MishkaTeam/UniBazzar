using Application.Aggregates.Categories;
using Application.Aggregates.Customers;
using Application.Aggregates.Customers.ShippingAddresses;
using Application.Aggregates.Products;
using Application.Aggregates.Stores;
using Application.Aggregates.Units;
using Application.Aggregates.Users;
using Application.ProductSearch;
using Domain;
using Domain.Aggregates.Categories;
using Domain.Aggregates.Customers;
using Domain.Aggregates.Customers.ShippingAddresses;
using Domain.Aggregates.Products;
using Domain.Aggregates.Products.ProductFeatures;
using Domain.Aggregates.Products.ProductImages;
using Domain.Aggregates.Products.ProductPriceLists;
using Domain.Aggregates.Stores;
using Domain.Aggregates.Units;
using Domain.Aggregates.Users;
using Domain.ProductSearch.Data;
using Persistence;
using Persistence.Aggregates.Categories;
using Persistence.Aggregates.Customers;
using Persistence.Aggregates.Products;
using Persistence.Aggregates.ShippingAddresses;
using Persistence.Aggregates.Stores;
using Persistence.Aggregates.Units;
using Persistence.Aggregates.Users;
using Persistence.ProductSearch;

namespace Server.Infrastructure.Extensions.ServiceCollections;

public static class DomainExtensions
{
    public static IServiceCollection AddDomainApplications(this IServiceCollection services)
    {
        services.AddScoped<StoresApplication>();

        services.AddScoped<ProductsApplication>();
        services.AddScoped<ProductImagesApplication>();
        services.AddScoped<ProductFeaturesApplication>();
        services.AddScoped<ProductPriceListsApplication>();

        services.AddScoped<UnitsApplication>();

        services.AddScoped<CategoriesApplication>();

        services.AddScoped<CategoryRepository>();

		    services.AddScoped<UserApplication>();

        services.AddScoped<CustomerApplication>();

        services.AddScoped<ShippingAddressApplication>();

        services.AddScoped<UserApplication>();
        services.AddScoped<ProductSearchApplication>();

        return services;
    }

    public static IServiceCollection AddDomainRepositories(this IServiceCollection services)
    {
        services.AddScoped<IStoreRepository, StoreRepository>();

        services.AddScoped<IProductRepository, ProductRepository>();

        services.AddScoped<IProductFeatureRepository, ProductFeaturesRepository>();

        services.AddScoped<IProductPriceListRepository, ProductPriceListsRepository>();

        services.AddScoped<IProductImageRepository, ProductImagesRepository>();

        services.AddScoped<IUnitRepository, UnitRepository>();

        services.AddScoped<ICategoryRepository, CategoryRepository>();

        services.AddScoped<ICustomerRepository, CustomerRepository>();

        services.AddScoped<IShippingAddressRepository, ShippingAddressRepository>();

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IProductSearchRepository, ProductSearchRepository>();
        return services;
    }

    public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}