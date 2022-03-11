
using System;
using Microsoft.AspNetCore.Identity;

namespace IdentityServerAspNetIdentity.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
 
        
        public string FirstName { get; set; }
        
        public string SecondName { get; set; }

        public Guid? SaccoId { get; set; }
        
        
        
    }
}
