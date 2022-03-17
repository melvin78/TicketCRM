using System;
using System.Runtime.Serialization;

namespace IdentityServerAspNetIdentity.Configuration
{
    public class Agent
    {
        public DateTime CreatedAt { get; set; }

        public Guid Id { get; set; }
        
        public string CreatedBy { get; set; }

        [DataMember]
        public virtual DateTime? ModifiedDate { get; set; }
        public Guid UserId { get; set; }
        
        public Guid OrganizationId { get; set; }
        
        public string Username { get; set; }
        
        public string FirstName { get; set; }
        
        public string SecondName { get; set; }
        
        public bool HasToken { get; set; }
        
        public Guid DepartmentId { get; set; }
        
        public DateTime? TokenAssignmentDate { get; set; }
        
    }
}