using Domain;
using Mapster;
using Domain.Aggregates.ShippingAddress;

namespace Application.Aggregates.ShippingAddress
{
    public class ShippingAddressApplication(IShippingAddressRepository shippingAddressRepository, IUnitOfWork unitOfWork)
    {
        public async Task<CreateShippingAddressViewModel> CreateAsync(CreateShippingAddressViewModel ViewModel)
        {
            var entity = Domain.Aggregates.ShippingAddress.ShippingAddress.Create
                (
                ViewModel.Country,
                ViewModel.Province,
                ViewModel.City,
                ViewModel.Address,
                ViewModel.PostalCode
                );
            shippingAddressRepository.AddShippingAddress ( entity );
            await unitOfWork.CommitAsync ();
            return entity.Adapt<CreateShippingAddressViewModel>();
        }
        public async Task<List<UpdateShippingAddressViewModel>> GetAllAddress()
        {
            var adress = await shippingAddressRepository.GetAllShippingAddressAsync();
            return adress.Adapt<List<UpdateShippingAddressViewModel>> ();
        }
        public async Task<UpdateShippingAddressViewModel> GetAddress(Guid id)
        {
            var customer = await shippingAddressRepository.GetShippingAddressAsync(id);

            if (customer == null || customer.Id == Guid.Empty)
            {
                throw new Exception(Resources.Messages.Errors.NotFound);
            }
            return customer.Adapt<UpdateShippingAddressViewModel>();
        }
        public async Task<UpdateShippingAddressViewModel> UpdateAsync(UpdateShippingAddressViewModel UpdateViewModel)
        {
            var entity = await shippingAddressRepository.GetShippingAddressAsync(UpdateViewModel.Id);
            if (entity == null || entity.Id == Guid.Empty)
            {
                throw new Exception(Resources.Messages.Errors.NotFound);
            }
            entity.Update(
                UpdateViewModel.Country,
                UpdateViewModel.Province,
                UpdateViewModel.City,
                UpdateViewModel.Address,
                UpdateViewModel.PostalCode
                );
            await unitOfWork.CommitAsync();
            return entity.Adapt<UpdateShippingAddressViewModel>();

        }
        public async Task DeleteAsync(Guid id)
        {
            var entity = await shippingAddressRepository.GetShippingAddressAsync(id);

            if (entity == null || entity.Id == Guid.Empty)
            {
                throw new Exception(Resources.Messages.Errors.NotFound);
            }

            shippingAddressRepository.Remove(entity);
            await unitOfWork.CommitAsync();
        }

        
    }
}
