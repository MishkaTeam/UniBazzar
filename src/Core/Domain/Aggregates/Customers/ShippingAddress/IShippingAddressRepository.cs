using Domain.BuildingBlocks.Data;

namespace Domain.Aggregates.Customers.ShippingAddresses;

public interface IShippingAddressRepository : IRepositoryBase<ShippingAddress>
{
        Task<List<ShippingAddress>> GetAllShippingAddressAsync(Guid customerId);
}
