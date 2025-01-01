using BuildingBlocks.Domain.Aggregates;
using Framework.DataType;
using System.ComponentModel.DataAnnotations;

namespace Domain.Aggregates.Customers
{
    public class Customer : Entity
    {
        public Customer()
        {
            // FOR EF!
        }
        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public List<ShippingAddress> ShippingAddresses { get; private set; }

        public string NationalCode { get; private set; }

        public string Mobile { get; private set; }

        public string Email { get; private set; }

        public string IsMobileVerified { get; private set; }

        public string IsEmailVerified { get; private set; }

        public string Password { get; private set; }
        
        public void AddShippingAddress(string country, string province, string city, string address, string postalCode)
        {

            var Adress = ShippingAddress.Create(country, province, city, address, postalCode);
            ShippingAddresses.Add(Adress);
        }

        public static Customer Register(string firstName, string lastName, string mobile, string password,string email )
        {
            var Customer = new Customer(firstName, lastName, mobile, password, email)
            {
                Mobile = mobile.Fix(),
                Password = password.Fix(),
                FirstName = firstName.Fix(),
                LastName = lastName.Fix(),
                Email = email.Fix(),
            };
            return Customer;
        }
        public void Update(string firstName, string lastName, string mobile, string password,string nationalcode,string email)
        {
            FirstName = firstName.Fix();
            LastName = lastName.Fix();
            Mobile = mobile;
            Password = password;
            NationalCode = nationalcode;
            Email = email;
        }

        private Customer(string firstname, string lastName, string mobile, string password, string email)
        { 
            ShippingAddresses = new List<ShippingAddress>();
            FirstName = firstname;
            LastName = lastName;
            Mobile = mobile;
            Password = password;
        }
    }
}
