using BuildingBlocks.Domain.Aggregates;
using Domain.Aggregates.Customers;
using Framework.DataType;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.Users
{
    public class User:Entity
    {
        public User() 
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

        public static User Register(string firstName, string lastName, string nationalcode, string mobile, string password, string email)
        {
            if (!email.IsValidEmail())
                throw new ValidationException(Resources.Messages.Validations.EmailAddress);

            if (!nationalcode.IsValidNationalCode())
                throw new ValidationException(Resources.Messages.Validations.NationalCode);

            if (!mobile.IsValidMobile())
                throw new ValidationException(Resources.Messages.Validations.CellPhoneNumber);

            if (!password.IsValidPassword())
                throw new ValidationException(Resources.Messages.Validations.Password);

            var customer = new User(firstName, lastName, nationalcode, mobile, password, email)
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

            if (!nationalcode.IsValidNationalCode())
                throw new ValidationException(Resources.Messages.Validations.NationalCode);

            if (!mobile.IsValidMobile())
                throw new ValidationException(Resources.Messages.Validations.CellPhoneNumber);
        }

        private User(string firstName, string lastName, string nationalcode, string mobile, string password, string email)
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
