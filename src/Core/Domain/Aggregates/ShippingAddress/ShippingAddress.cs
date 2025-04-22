using Domain.Aggregates.Customers;
using Framework.DataType;
using System.ComponentModel.DataAnnotations;
using Entity = Domain.BuildingBlocks.Aggregates.Entity;

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
        public Customer Customers { get; set; }
        public Guid CustomerId { get; set; }
        public static ShippingAddress Create(string country, string province, string city, string address, string postalCode, Guid customerid)
        {
            if (!postalCode.IsValidPostalCode())
                throw new ValidationException(Resources.Messages.Validations.PostalCode);

            var ShippingAddress = new ShippingAddress(country, province, city, address, postalCode, customerid)
            {
                Country = country.Fix(),
                Province = province.Fix(),
                City = city.Fix(),
                PostalCode = postalCode.Fix(),
                CustomerId = customerid,
                Address = address.Fix(),
            };
            return ShippingAddress;
        }
        public void Update(string country, string province, string city, string address, string postalCode, Guid customerid)
        {
            Country = country.Fix();
            Province = province.Fix();
            City = city.Fix();
            Address = address.Fix();
            CustomerId = customerid;
            if (!postalCode.IsValidPostalCode())
                throw new ValidationException(Resources.Messages.Validations.PostalCode);

			SetUpdateDateTime();
		}
		private ShippingAddress(string country, string province, string city, string address, string postalCode, Guid customerid)
        {
            Country = country;
            Province = province;
            City = city;
            Address = address;
            CustomerId = customerid;
            PostalCode = postalCode;
        }
    }
}