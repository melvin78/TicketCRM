using System;
using System.Runtime.Serialization;

namespace IdentityServerAspNetIdentity.Configuration
{
    public class AgentDTO
    {


        public Guid UserId { get; set; }
        
        public string Username { get; set; }
        
        public string FirstName { get; set; }
        
        public string SecondName { get; set; }
        
        public Guid DepartmentId { get; set; }
        
        
        public Guid SaccoId { get; set; }
        
        public DateTime? TokenAssignmentDate { get; set; }
        
 
    }
}