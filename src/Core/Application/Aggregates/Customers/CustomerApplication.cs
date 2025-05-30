﻿using Application.Aggregates.Customers.ViewModels;
using Application.ViewModels.Authentication;
using Domain;
using Domain.Aggregates.Customers;
using Framework.DataType;
using Mapster;

namespace Application.Aggregates.Customers;

public class CustomerApplication(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
{
    public async Task<CustomerViewModel> CreateAsync(CreateCustomerViewModel viewModel)
    {
        var customer = Customer.Register
            (
            viewModel.FirstName,
            viewModel.LastName,
            viewModel.NationalCode,
            viewModel.Mobile,
            viewModel.Password,
            viewModel.Email
            );

        await customerRepository.AddAsync(customer);
        await unitOfWork.CommitAsync();

        return customer.Adapt<CustomerViewModel>();
    }

    public async Task<CustomerViewModel> CreateAsync(string mobile, string password)
    {
        var customer = Customer.Register(mobile, password);

        await customerRepository.AddAsync(customer);
        await unitOfWork.CommitAsync();

        return customer.Adapt<CustomerViewModel>();
    }

    public async Task<CreateCustomerViewModelPos> CreateAsync(CreateCustomerViewModelPos viewModel)
    {
        var customer = Customer.Register(
            firstName: null,
            viewModel.LastName,
            viewModel.NationalCode,
            viewModel.Mobile,
            password: null,
            email: null
            );

        await customerRepository.AddAsync(customer);
        await unitOfWork.CommitAsync();

        return customer.Adapt<CreateCustomerViewModelPos>();
    }

    public async Task<List<UpdateCustomerViewModel>> GetAllCustomer()
    {
        var customers = await customerRepository.GetAllAsync();

        return customers.Adapt<List<UpdateCustomerViewModel>>();
    }

    public async Task<UpdateCustomerViewModel> GetCustomerAsync(Guid id)
    {
        var customer = await customerRepository.GetByIdAsync(id);

        if (customer == null || customer.Id == Guid.Empty)
        {
            throw new Exception(Resources.Messages.Errors.NotFound);
        }

        return customer.Adapt<UpdateCustomerViewModel>();
    }

    public async Task<UpdateCustomerViewModel> UpdateAsync(UpdateCustomerViewModel updateViewModel)
    {
        var entity = await customerRepository.GetByIdAsync(updateViewModel.Id);

        if (entity == null || entity.Id == Guid.Empty)
        {
            throw new Exception(Resources.Messages.Errors.NotFound);
        }

        entity.Update(
            updateViewModel.FirstName,
            updateViewModel.LastName,
            updateViewModel.NationalCode,
            updateViewModel.Mobile
            );

        await unitOfWork.CommitAsync();
        return entity.Adapt<UpdateCustomerViewModel>();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await customerRepository.GetByIdAsync(id);

        if (entity == null || entity.Id == Guid.Empty)
        {
            throw new Exception(Resources.Messages.Errors.NotFound);
        }

        await customerRepository.RemoveAsync(entity);
        await unitOfWork.CommitAsync();
    }

    public async Task<ResultContract<CustomerViewModel>> LoginWithMobileAsync(LoginViewModel model)
    {
        var user = await customerRepository.GetWithMobile(model.UserName);

        if (user == null || user.Id == Guid.Empty)
        {
            return (ErrorType.NotFound,
                string.Format(Resources.Messages.Errors.NotFound, Resources.DataDictionary.User));
        }

        if (user.Password != model.Password) // Encryption
        {
            return (ErrorType.InvalidCredentials, Resources.Messages.Validations.Password);
        }

        return user.Adapt<CustomerViewModel>();
    }
}