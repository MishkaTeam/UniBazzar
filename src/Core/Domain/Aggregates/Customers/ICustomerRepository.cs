﻿using Domain.Aggregates.Units;

namespace Domain.Aggregates.Customers;

public interface ICustomerRepository
{
    void AddCustomer(Customer entity);
    //Task<List<Customer>> GetRootCustomersAsync();
    Task<List<Customer>> GetAllCustomersAsync();
    Task<Customer> GetCustomerAsync(Guid id);
    void Remove(Customer entity);
}
