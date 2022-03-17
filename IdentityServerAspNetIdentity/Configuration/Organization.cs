using System;

namespace IdentityServerAspNetIdentity.Configuration
{
    public class Organization
    {
        public Guid Id { get; set; }
        
        public string OrganizationName { get; set; }
        
        public DateTime? CreatedAt { get; set; }
        
        public string CreatedBy { get; set; }
        
        public DateTime? ModifiedDate { get; set; }
        
    }
}