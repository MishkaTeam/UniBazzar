using Application.Aggregates.Units.ViewModels;
using Domain;
using Domain.Aggregates.Customers;
using Mapster;

namespace Application.Aggregates.Customer
{
    public class CustomerApplication(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
    {
        public async Task<CreateCustomerViewModel> CreateAsync(CreateCustomerViewModel viewModel)
        {
            var entity = Domain.Aggregates.Customers.Customer.Register
                (
                viewModel.FirstName,
                viewModel.LastName,
                viewModel.NationalCode,
                viewModel.Mobile,
                viewModel.Password,
                viewModel.Email
                );
            customerRepository.AddCustomer(entity);
            await unitOfWork.CommitAsync();
            return entity.Adapt<CreateCustomerViewModel>();
        }
        public async Task<List<CustomerViewModel>> GetAllCustomer()
        {
            var customers = await customerRepository.GetAllCustomersAsync();
            return customers.Adapt<List<CustomerViewModel>>();
        }
       

        public async Task<CustomerViewModel> GetCustomerAsync(Guid id)
        {
            var customer = await customerRepository.GetCustomerAsync(id);

            if (customer == null || customer.Id == Guid.Empty)
            {
                throw new Exception(Resources.Messages.Errors.NotFound);
            }
            return customer.Adapt<CustomerViewModel>();
        }
        
            public async Task<UpdateCustomerViewModel> GetRootCustomersAsync(Guid id)
        {
            var customer = await customerRepository.GetCustomerAsync(id);

            if (customer == null || customer.Id == Guid.Empty)
            {
                throw new Exception(Resources.Messages.Errors.NotFound);
            }
            return customer.Adapt<UpdateCustomerViewModel>();
        }

        public async Task<UpdateCustomerViewModel> UpdateAsync(UpdateCustomerViewModel updateViewModel)
        {
            var entity = await customerRepository.GetCustomerAsync(updateViewModel.Id);

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
            var entity = await customerRepository.GetCustomerAsync(id);

            if (entity == null || entity.Id == Guid.Empty)
            {
                throw new Exception(Resources.Messages.Errors.NotFound);
            }

            customerRepository.Remove(entity);
            await unitOfWork.CommitAsync();
        }
    }
}
