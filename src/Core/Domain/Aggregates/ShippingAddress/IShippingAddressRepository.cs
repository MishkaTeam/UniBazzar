using Domain.Aggregates.Customers;

namespace Domain.Aggregates.ShippingAddress
{
   public  interface IShippingAddressRepository
    {
        void AddShippingAddress(ShippingAddress entity);
        Task<List<ShippingAddress>> GetAllShippingAddressAsync(Guid customerId);
        Task<ShippingAddress> GetShippingAddressAsync(Guid id);
        void Remove(ShippingAddress entity);
    }
}
