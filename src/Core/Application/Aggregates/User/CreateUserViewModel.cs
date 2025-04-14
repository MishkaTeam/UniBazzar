﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Aggregates.User
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
          Name = nameof(Resources.DataDictionary.NationalCode))]
        public string NationalCode { get; set; }

        [Display(ResourceType = typeof(Resources.DataDictionary),
           Name = nameof(Resources.DataDictionary.Mobile))]
        public string Mobile { get; set; }

        [Display(ResourceType = typeof(Resources.DataDictionary),
           Name = nameof(Resources.DataDictionary.Email))]
        public string Email { get; set; }

        [Display(ResourceType = typeof(Resources.DataDictionary),
           Name = nameof(Resources.DataDictionary.Password))]
        public string Password { get; set; }

        [Display(ResourceType = typeof(Resources.DataDictionary),
                   Name = nameof(Resources.DataDictionary.ConfirmPassword))]
        public string ConfirmPassword { get; set; }
    }
}
