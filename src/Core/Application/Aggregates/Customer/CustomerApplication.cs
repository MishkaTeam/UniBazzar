using Domain;
using Domain.Aggregates.Customers;
using Mapster;

namespace Application.Aggregates.Customer
{
    public class CustomerApplication(ICustomerRepository customerRepository, IUnitOfWork unitOfWork )
    {
        public async Task<CreateCustomerViewModel> CreateAsync(CreateCustomerViewModel viewModel)
        {
            var entity = Domain.Aggregates.Customers.Customer.Register(
               
                viewModel.Mobile,
                viewModel.Email,
                viewModel.Password);

            customerRepository.AddCustomer(entity);
            await unitOfWork.CommitAsync();
            return entity.Adapt<CreateCustomerViewModel>();
        }

        public async Task<List<UpdateCustomerViewModel>> GetCustomersAsync()
        {
            var customers = await customerRepository.GetAllCustomerAsync();
            return customers.Adapt<List<UpdateCustomerViewModel>>();
        }

        public async Task<UpdateCustomerViewModel> GetCustomerAsync(Guid id)
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
                updateViewModel.Mobile,
                updateViewModel.Email,
                updateViewModel.Password);

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
