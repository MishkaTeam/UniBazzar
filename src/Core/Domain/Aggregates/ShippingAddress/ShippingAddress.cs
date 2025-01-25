using BuildingBlocks.Domain.Aggregates;
using Framework.DataType;

namespace Domain.Aggregates.ShippingAddress
{
    public class ShippingAddress : Entity
    {
        public ShippingAddress()
        {
            // FOR EF!
        }
        public string Country { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }

        
        public static ShippingAddress Create(string country, string province, string city, string address, string postalCode)
        {
            var ShippingAddress = new ShippingAddress(country, province, city, address, postalCode)
            {
                Country = country.Fix(),
                Province = country.Fix(),
                City = city.Fix(),
                PostalCode=postalCode.Fix(),
                Address = address.Fix(),
            };
            return ShippingAddress;
        }
        public void Update(string country, string province, string city, string address, string postalCode)
        {
            Country = country.Fix();
            Province = province.Fix();
            City = city.Fix();
            Address = address.Fix();
            PostalCode = postalCode.Fix();
        }
        private ShippingAddress(string country, string province, string city, string address, string postalCode)
        {
            Country = country;
            Province = province;
            City = city;
            Address = address;
            PostalCode = postalCode;
        }
    }
}
