﻿using Application.Aggregates.Customer;
using Application.Aggregates.Units;
using Domain;
using Domain.Aggregates.Customers;
using Domain.Aggregates.Units;
using Persistence;
using Persistence.Aggregates.Customer;
using Persistence.Aggregates.Units;

namespace Server.Infrastructure.Extentions.ServiceCollections;

public static class DomainExtensions
{
	public static IServiceCollection AddDomainApplications(this IServiceCollection services)
	{
		services.AddScoped<UnitsApplication>();
		services.AddScoped<CustomerApplication>();
		return services;
	}

	public static IServiceCollection AddDomainRepositories(this IServiceCollection services)
	{
		services.AddScoped<IUnitRepository,UnitRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        return services;
		
	}

	public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
	{
		services.AddScoped<IUnitOfWork, UnitOfWork>();
		return services;
	}
   

   
}
