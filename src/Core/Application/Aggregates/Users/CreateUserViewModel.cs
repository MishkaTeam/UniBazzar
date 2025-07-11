﻿using System.ComponentModel.DataAnnotations;

namespace Application.Aggregates.Users
{
    public class CreateUserViewModel
    {
        [Display(ResourceType = typeof(Resources.DataDictionary),
          Name = nameof(Resources.DataDictionary.Name))]
        public string FirstName { get; set; }

        [Display(ResourceType = typeof(Resources.DataDictionary),
          Name = nameof(Resources.DataDictionary.Family))]
        public string LastName { get; set; }

        [Display(ResourceType = typeof(Resources.DataDictionary),
          Name = nameof(Resources.DataDictionary.UserName))]
        public string UserName { get; set; }

        [Display(ResourceType = typeof(Resources.DataDictionary),
           Name = nameof(Resources.DataDictionary.Mobile))]
        public string Mobile { get; set; }

        [Display(ResourceType = typeof(Resources.DataDictionary),
           Name = nameof(Resources.DataDictionary.Password))]
        public string Password { get; set; }

        [Display(ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.ConfirmPassword))]
        public string ConfirmPassword { get; set; }

        [Display(ResourceType = typeof(Resources.DataDictionary),
              Name = nameof(Resources.DataDictionary.Role))]
        public Guid Role { get; set; }
    }
}
