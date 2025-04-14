using BuildingBlocks.Domain.Aggregates;
using Domain.Aggregates.Customers;
using Domain.Aggregates.Users;
using Framework.DataType;
using System.ComponentModel.DataAnnotations;

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
        public User Users {  get; set; }   
        public Guid UserId { get; set; }
        public static ShippingAddress Create(string country, string province, string city, string address, string postalCode, Guid customerid,Guid userid)
        {
            if (!postalCode.IsValidPostalCode())
                throw new ValidationException(Resources.Messages.Validations.PostalCode);

            var ShippingAddress = new ShippingAddress(country, province, city, address, postalCode, customerid,userid)
            {
                Country = country.Fix(),
                Province = province.Fix(),
                City = city.Fix(),
                PostalCode = postalCode.Fix(),
                CustomerId = customerid,
                UserId=userid,
                Address = address.Fix(),
            };
            return ShippingAddress;
        }
        public void Update(string country, string province, string city, string address, string postalCode, Guid customerid, Guid userid)
        {
            Country = country.Fix();
            Province = province.Fix();
            City = city.Fix();
            Address = address.Fix();
            CustomerId = customerid;
            UserId = userid;
            if (!postalCode.IsValidPostalCode())
                throw new ValidationException(Resources.Messages.Validations.PostalCode);

			SetUpdateDateTime();
		}
		private ShippingAddress(string country, string province, string city, string address, string postalCode, Guid customerid, Guid userid)
        {
            Country = country;
            Province = province;
            City = city;
            Address = address;
            CustomerId = customerid;
            UserId=userid;
            PostalCode = postalCode;
        }
    }
}