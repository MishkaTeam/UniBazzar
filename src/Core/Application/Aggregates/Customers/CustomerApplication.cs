using Application.Aggregates.Customers.ViewModels;
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
        await unitOfWork.SaveChangesAsync();

        return customer.Adapt<CustomerViewModel>();
    }

    public async Task<CustomerViewModel> CreateAsync(string mobile, string password)
    {
        var customer = Customer.Register(mobile, password);

        await customerRepository.AddAsync(customer);
        await unitOfWork.SaveChangesAsync();

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
        await unitOfWork.SaveChangesAsync();

        return customer.Adapt<CreateCustomerViewModelPos>();
    }

    public async Task<List<CustomerViewModel>> GetAllCustomer()
    {
        var customers = await customerRepository.GetAllAsync();

        return customers.Adapt<List<CustomerViewModel>>();
    }

    public async Task<CustomerViewModel> GetCustomerAsync(Guid id)
    {
        var customer = await customerRepository.GetByIdAsync(id);

        if (customer == null || customer.Id == Guid.Empty)
        {
            throw new Exception(Resources.Messages.Errors.NotFound);
        }

        return customer.Adapt<CustomerViewModel>();
    }

    public async Task<CustomerViewModel> UpdateAsync(UpdateCustomerViewModel updateViewModel)
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

        await unitOfWork.SaveChangesAsync();
        return entity.Adapt<CustomerViewModel>();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await customerRepository.GetByIdAsync(id);

        if (entity == null || entity.Id == Guid.Empty)
        {
            throw new Exception(Resources.Messages.Errors.NotFound);
        }

        await customerRepository.RemoveAsync(entity);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task<ResultContract<CustomerViewModel>> LoginWithMobileAsync(string mobile)
    {
        var user = await customerRepository.GetWithMobile(mobile);

        if (user == null || user.Id == Guid.Empty)
        {
            return (ErrorType.NotFound,
                string.Format(Resources.Messages.Errors.NotFound, Resources.DataDictionary.User));
        }

        return user.Adapt<CustomerViewModel>();
    }

    public async Task<ResultContract<bool>> IsExistsAsync(string mobile)
    {
        var isCustomerExists = await customerRepository.IsCustomerExists(mobile);
        return isCustomerExists;
    }

    public async Task<ResultContract<bool>> Exist(Guid id)
    {
        var customer =
            await customerRepository.GetByIdAsync(id);

        return customer != null;
    }

    public async Task<ResultContract<Guid>> GetPublicCustomer()
    {
        var publicCustomer =
            await customerRepository.GetAsync(x => x.Email == "public-customer@unibazzar.ir");

        if (publicCustomer == null)
        {
            var customer = await CreateAsync(new CreateCustomerViewModel()
            {
                // Attention!
                // This information is hard code, and needs to change.
                NationalCode = "0927421498",
                LastName = "[ مشتری عمومی ]",
                Mobile = "09000000000",
                Email = "public-customer@unibazzar.ir",
            });

            return customer.Id;
        }

        return publicCustomer.Id;
    }

}