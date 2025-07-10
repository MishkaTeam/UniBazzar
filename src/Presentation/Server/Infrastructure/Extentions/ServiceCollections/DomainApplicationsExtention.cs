using Application.Aggregates.Branches;
using Application.Aggregates.Categories;
using Application.Aggregates.CheckoutCounter;
using Application.Aggregates.Customers;
using Application.Aggregates.Customers.ShippingAddresses;
using Application.Aggregates.Discounts;
using Application.Aggregates.Ordering.Baskets;
using Application.Aggregates.PriceLists;
using Application.Aggregates.ProductReviews;
using Application.Aggregates.Products;
using Application.Aggregates.Products.ProductFeatures;
using Application.Aggregates.Products.ProductImages;
using Application.Aggregates.Stores;
using Application.Aggregates.Units;
using Application.Aggregates.Users;
using Application.CustomerSearch;
using Application.ProductSearch;
using Domain;
using Domain.Aggregates.branches;
using Domain.Aggregates.Categories;
using Domain.Aggregates.CheckoutCounters;
using Domain.Aggregates.Customers;
using Domain.Aggregates.Customers.ShippingAddresses;
using Domain.Aggregates.Discounts;
using Domain.Aggregates.Ordering.Baskets.Data;
using Domain.Aggregates.PriceLists;
using Domain.Aggregates.ProductReviews;
using Domain.Aggregates.Products;
using Domain.Aggregates.Products.ProductFeatures;
using Domain.Aggregates.Products.ProductImages;
using Domain.Aggregates.Stores;
using Domain.Aggregates.Units;
using Domain.Aggregates.Users;
using Domain.CustomerSearch.Data;
using Domain.ProductSearch.Data;
using Persistence;
using Persistence.Aggregates.Comments;
using Persistence.Repositories.Aggregates.Branches;
using Persistence.Repositories.Aggregates.Categories;
using Persistence.Repositories.Aggregates.CheckoutCounters;
using Persistence.Repositories.Aggregates.Customers;
using Persistence.Repositories.Aggregates.Discounts;
using Persistence.Repositories.Aggregates.Ordering;
using Persistence.Repositories.Aggregates.Products;
using Persistence.Repositories.Aggregates.Stores;
using Persistence.Repositories.Aggregates.Units;
using Persistence.Repositories.Aggregates.Users;
using Persistence.Repositories.CustomerSearch;
using Persistence.Repositories.ProductSearch;
using Persistence.Aggregates.Comments;
using Domain.Aggregates.CheckoutCounters;
using Domain.Aggregates.ProductReviews;
using Application.Aggregates.Products.ProductFeatures;
using Application.Aggregates.Ordering.Baskets;
using Application.Aggregates.Products.ProductImages;
using Application.Aggregates.ProductReviews;
using Domain.Aggregates.Customers.ShippingAddresses;
using Application.Aggregates.Customers.ShippingAddresses;
using Domain.Aggregates.Discounts.DsiscounProducts;
using Domain.Aggregates.Discounts.DiscountCustomers;

namespace Server.Infrastructure.Extensions.ServiceCollections;

public static class DomainExtensions
{
    public static IServiceCollection AddDomainApplications(this IServiceCollection services)
    {
        services.AddScoped<UserApplication>();

        services.AddScoped<StoresApplication>();
        services.AddScoped<BranchesApplication>();
        services.AddScoped<CheckoutCounterApplication>();

        services.AddScoped<CustomerApplication>();
        services.AddScoped<ShippingAddressApplication>();

        services.AddScoped<BasketApplication>();
        services.AddScoped<DiscountApplication>();

        services.AddScoped<ProductsApplication>();
        services.AddScoped<ProductImagesApplication>();
        services.AddScoped<ProductFeaturesApplication>();
        services.AddScoped<PriceListsApplication>();

        services.AddScoped<UnitsApplication>();
        services.AddScoped<CategoriesApplication>();

        services.AddScoped<DiscountApplication>();
        services.AddScoped<DiscountProductApplication>();
        services.AddScoped<DiscountCustomerApplication>();


        services.AddScoped<ProductSearchApplication>();
        services.AddScoped<CustomerSearchApplication>();

        services.AddScoped<ProductReviewApplication>();

        return services;
    }

    public static IServiceCollection AddDomainRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IStoreRepository, StoreRepository>();
        services.AddScoped<IbranchRepository, BranchRepository>();
        services.AddScoped<ICheckoutCounterRepository, CheckoutCounterRepository>();

        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IShippingAddressRepository, ShippingAddressRepository>();

        services.AddScoped<IBasketRepository, BasketRepository>();

        services.AddScoped<IDiscountRepository, DiscountRepository>();
        services.AddScoped<IDiscountProductRepository, DiscountProductRepository>();
        services.AddScoped<IDiscountCustomerRepository, DiscountCustomerRepository>();

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductImageRepository, ProductImagesRepository>();
        services.AddScoped<IProductFeatureRepository, ProductFeaturesRepository>();
        services.AddScoped<IPriceListRepository, PriceListsRepository>();
        services.AddScoped<IProductImageRepository, ProductImagesRepository>();

        services.AddScoped<IUnitRepository, UnitRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();



        services.AddScoped<IProductSearchRepository, ProductSearchRepository>();
        services.AddScoped<ICustomerSearchRepository, CustomerSearchRepository>();

        services.AddScoped<IProductReviewRepository, CommentRepository>();

        return services;
    }

    public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}