using BuildingBlocks.Domain.Aggregates;
using Framework.DataType;

namespace Domain.Aggregates.Customers
{
    public class ShippingAddress : Entity
    {

        public string Country { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }

        public static ShippingAddress create(string country, string province, string city, string address, string postalCode)
        {
            var shippingAddress = new ShippingAddress(country, province, city, address, postalCode)
            {
                Country = country.Fix(),
                Province = country.Fix(),
                City = city.Fix(),
                Address = address.Fix(),
            };
            return shippingAddress;
        }
        private ShippingAddress(string country, string province, string city, string address, string postalCode)
        {
            Country = country;
            Province = province;
            City = city;
            Address = address;
        }
    }
}
