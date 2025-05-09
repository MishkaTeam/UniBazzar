using Domain;
using Domain.Aggregates.Customers.ShippingAddresses;
using Mapster;

namespace Application.Aggregates.Customers.ShippingAddresses;

public class ShippingAddressApplication(IShippingAddressRepository shippingAddressRepository, IUnitOfWork unitOfWork)
{
    public async Task<CreateShippingAddressViewModel> CreateAsync(CreateShippingAddressViewModel ViewModel)
    {
        var shippingAddress = ShippingAddress.Create
            (
            ViewModel.Country,
            ViewModel.Province,
            ViewModel.City,
            ViewModel.Address,
            ViewModel.PostalCode,
            ViewModel.CustomerId
            );

        await shippingAddressRepository.AddAsync(shippingAddress);
        await unitOfWork.CommitAsync();

        return shippingAddress.Adapt<CreateShippingAddressViewModel>();
    }

    public async Task<List<UpdateShippingAddressViewModel>> GetAllAddress(Guid CustomerId)
    {
        var shippingAddress = await shippingAddressRepository.GetAllShippingAddressAsync(CustomerId);

        return shippingAddress.Adapt<List<UpdateShippingAddressViewModel>>();
    }

    public async Task<UpdateShippingAddressViewModel> GetAddress(Guid id)
    {
        var shippingAddress = await shippingAddressRepository.GetByIdAsync(id);

        if (shippingAddress == null || shippingAddress.Id == Guid.Empty)
        {
            throw new Exception(Resources.Messages.Errors.NotFound);
        }
        return shippingAddress.Adapt<UpdateShippingAddressViewModel>();
    }

    public async Task<UpdateShippingAddressViewModel> UpdateAsync(UpdateShippingAddressViewModel UpdateViewModel)
    {
        var shippingAddress = await shippingAddressRepository.GetByIdAsync(UpdateViewModel.Id);

        if (shippingAddress == null || shippingAddress.Id == Guid.Empty)
        {
            throw new Exception(Resources.Messages.Errors.NotFound);
        }

        shippingAddress.Update(
            UpdateViewModel.Country,
            UpdateViewModel.Province,
            UpdateViewModel.City,
            UpdateViewModel.Address,
            UpdateViewModel.PostalCode,
            UpdateViewModel.CustomerId
               );

        await unitOfWork.CommitAsync();

        return shippingAddress.Adapt<UpdateShippingAddressViewModel>();
    }

    public async Task DeleteAsync(Guid id)
    {
        var shippingAddress = await shippingAddressRepository.GetByIdAsync(id);

        if (shippingAddress == null || shippingAddress.Id == Guid.Empty)
        {
            throw new Exception(Resources.Messages.Errors.NotFound);
        }

        await shippingAddressRepository.RemoveAsync(shippingAddress);
        await unitOfWork.CommitAsync();
    }
}
