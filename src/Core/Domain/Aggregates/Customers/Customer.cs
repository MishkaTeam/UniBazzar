using BuildingBlocks.Domain.Aggregates;
using Framework.DataType;
using System.Collections.Generic;
using System.Net;
using System.Runtime.InteropServices;
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

        public List<ShippingAddress> shippingAddresses { get; private set; }

        public string NationalCode { get; private set; }

        public string Mobile { get; private set; }

        public string Email { get; private set; }

        public string IsMobileVerified { get; private set; }

        public string IsEmailVerified { get; private set; }

        public string Password { get; private set; }
        

        public void shippingAddress(string country, string province, string city, string address, string postalCode)
        {

            var Adress = ShippingAddress.create(country, province, city, address, postalCode);
            shippingAddresses.Add(Adress);
        }

        public static Customer register(string firstName, string lastName, string mobile, string password)
        {

            var customer = new Customer(firstName, lastName, mobile, password)
            {
                //FirstName = ValidateFirstname(firstName),
                FirstName = firstName.Fix(),
                LastName = lastName.Fix(),
            };

            return customer;
        }
        public void Update(string firstName, string lastName, string mobile, string password)
        {
            FirstName = firstName.Fix();
            LastName = lastName.Fix();
            Mobile = mobile;
            Password = password;

        }
        //private static string ValidateFirstname(string firstname)
        //{
        //    if (string.IsNullOrWhiteSpace(firstname))
        //    {
        //        //var message=string.Format(Resources.Messages.Validations.GreaterThan,Resources.DataDictionary.)
        //        throw new ArgumentException("Firstname cannot be null or empty.");
        //    }
        //    return firstname.Trim();
        //}
        private Customer(string firstname, string lastName, string mobile, string password)
        {
            
            shippingAddresses = new List<ShippingAddress>();
            FirstName = firstname;
            LastName = lastName;
            Mobile = mobile;
            Password = password;
        }
    }
}
