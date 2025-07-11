﻿using Domain.Aggregates.Users.Enums;
using BuildingBlocks.Domain.Aggregates;
using Framework.DataType;
using System.ComponentModel.DataAnnotations;

namespace Domain.Aggregates.Users
{
	public class User : Entity
    {
        public User() 
        {
            // FOR EF!
        }
        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string UserName { get; private set; }
        
        public string Mobile { get; private set; }

        public string Password { get; private set; }

        public Guid Role { get; private  set; }
        public static User Register(string firstName, string lastName, string mobile, string password,string username)
        {
            if (!mobile.IsValidMobile())
                throw new ValidationException(Resources.Messages.Validations.CellPhoneNumber);

            if (!password.IsValidPassword())
                throw new ValidationException(Resources.Messages.Validations.Password);

            var user = new User(firstName, lastName, mobile, password, username)
            {
                Mobile = mobile.Fix(),
                Password = password.Fix(),
            };
            return user;
        }
        public void Update(string firstName, string lastName, string username ,string password, string mobile)
        {
            if (!mobile.IsValidMobile())
                throw new ValidationException(Resources.Messages.Validations.CellPhoneNumber);

            FirstName = firstName.Fix();
            LastName = lastName.Fix();
            Mobile = mobile.Fix();
            UserName = username;
            Password = password.Fix();
           
        }

        private User(string firstName, string lastName, string mobile, string password, string username)
        {
            FirstName = firstName.Fix();
            LastName = lastName.Fix();
            Mobile = mobile;
            Password = password;
            UserName = username;
        }


        public void SetUserRole(Guid roleType)
        {
            if(!RoleType.Contains(roleType))
            {
                throw new InvalidDataException("Please use proper RoleType!");
            }

            Role = roleType;
        }
    }
}
