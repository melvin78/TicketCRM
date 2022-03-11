using System;

namespace IdentityServerAspNetIdentity.Configuration
{
    public class Sacco
    {
        public Guid Id { get; set; }
        
        public string SaccoName { get; set; }
        
        public DateTime? CreatedAt { get; set; }
        
        public string CreatedBy { get; set; }
        
        public DateTime? ModifiedDate { get; set; }
        
    }
}