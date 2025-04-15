using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Aggregates.Users
{
    public class UpdateUserViewModel:CreateUserViewModel
    {
        [Display(ResourceType = typeof(Resources.DataDictionary),
                   Name = nameof(Resources.DataDictionary.Name))]
        public string FirstName { get; set; }

        [Display(ResourceType = typeof(Resources.DataDictionary),
           Name = nameof(Resources.DataDictionary.Family))]
        public string LastName { get; set; }

		


		[Display(ResourceType = typeof(Resources.DataDictionary),
           Name = nameof(Resources.DataDictionary.Id))]
        public Guid Id { get; set; }

    }
}
