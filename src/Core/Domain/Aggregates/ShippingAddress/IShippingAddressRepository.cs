

namespace Domain.Aggregates.ShippingAddress
{
   public  interface IShippingAddressRepository
    {
        void AddShippingAddress(ShippingAddress entity);
        Task<List<ShippingAddress>> GetAllShippingAddressAsync();
        Task<ShippingAddress> GetShippingAddressAsync(Guid id);
        void Remove(ShippingAddress entity);
    }
}
