using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Aggregates.Customers
{
    public class Customer
    {
        
        [MaxLength(50)]
        public string FirstName { get; set; } = "";
        [MaxLength(250)]
        public string LastName { get; set; } = "";

        public string ShippingAddress { get; set; } = "";
        [MaxLength(15)]
        public string NationalCode { get; set; } = "";
        [MaxLength(11)]
        public string Mobile { get; set; } = "";

        public string Email { get; set; } = "";

        public string IsMobileVerified { get; set; } = "";

        public string IsEmailVerified { get; set; } = "";

        public string Password { get; set; } = "";
        
        
    }
}
