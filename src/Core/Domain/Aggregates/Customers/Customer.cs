using BuildingBlocks.Domain.Aggregates;
using Domain.Aggregates.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.Customers
{
    public class Customer : Entity
    {
        public Customer()
        {

        }
        private Customer()
        {
            shippingAddresses = new List<ShippingAddress>();
        }

        public Customer(string firstName, string lastName, string email, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<ShippingAddress> shippingAddresses { get; set; }

        public string NationalCode { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }

        public string IsMobileVerified { get; set; }

        public string IsEmailVerified { get; set; }

        public string Password { get; set; }

        public static Customer register(string firstName, string lastName, string email, string password)
        {
            var customer = new Customer(firstName, lastName, email, password)
            {
                FirstName = ValidateFirstname(firstName),

            };
            return customer;
        }
        public void Update(string firstName, string lastName, string email, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;

        }
        private static string ValidateFirstname(string firstname)
        {
            if (string.IsNullOrWhiteSpace(firstname))
            {

                throw new ArgumentException("Firstname cannot be null or empty.");
            }

            return firstname.Trim();
        }

        private Customer(string firstname, string lastName, string email, string password)
        {
            FirstName = firstname;
            LastName = lastName;
            Email = email;
            Password = password;
        }
    }
}
