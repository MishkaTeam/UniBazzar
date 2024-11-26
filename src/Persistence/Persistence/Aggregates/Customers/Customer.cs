using BuildingBlocks.Domain.Aggregates;

namespace Persistence.Aggregates.Customers
{
    public class Customer: Entity
    {
        
        
        public string FirstName { get; set; } 
        
        public string LastName { get; set; } 

        public ShippingAddress fullAddress { get; set; } 
       
        public string NationalCode { get; set; } 
        
        public string Mobile { get; set; } 

        public string Email { get; set; } 

        public string IsMobileVerified { get; set; } 

        public string IsEmailVerified { get; set; } 

        public string Password { get; set; } 
        
        
    }
}
