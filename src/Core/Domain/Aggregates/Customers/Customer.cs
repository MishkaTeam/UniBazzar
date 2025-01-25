using BuildingBlocks.Domain.Aggregates;
using Domain.Aggregates.ShippingAddress;
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

        

        public string NationalCode { get; private set; }

        public string Mobile { get; private set; }

        public string Email { get; private set; }

        public bool IsMobileVerified { get; private set; }

        public bool IsEmailVerified { get; private set; }

        public string Password { get; private set; }

        //public void AddShippingAddress(string country, string province, string city, string address, string postalCode)
        //{
        //    var addressEntity = ShippingAddress.Create(country, province, city, address, postalCode);
        //    ShippingAddresses.Add(addressEntity);
        //}

        public static Customer Register(string firstName, string lastName,string nationalcode, string mobile, string password, string email)
        {
           

            if (!email.IsValidEmail())
                throw new ValidationException(Resources.Messages.Validations.EmailAddress);

            var customer = new Customer(firstName, lastName, nationalcode, mobile, password, email)
            {
                Mobile = mobile.Fix(),
                Password = password.Fix(),
            };

            return customer;
        }

        public void Update(string firstName, string lastName, string nationalcode, string mobile)
        {
            FirstName = firstName.Fix();
            LastName = lastName.Fix();
            NationalCode = nationalcode.Fix();
            Mobile = mobile;
        }

        private Customer(string firstName, string lastName, string nationalcode, string mobile, string password, string email)
        {
           
            FirstName = firstName.Fix();
            LastName = lastName.Fix();
            NationalCode = nationalcode.Fix();
            Mobile = mobile;
            Password = password;
            Email = email;
        }
    }
}

