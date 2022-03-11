using System;
using System.Runtime.Serialization;

namespace IdentityServerAspNetIdentity.Configuration
{
    public class Department
    {
        public string DepartmentVal { get; set; }
        
        public string CreatedBy { get; set; }

        [DataMember]
        public virtual DateTime? ModifiedDate { get; set; }
        
        public Guid Id { get; set; }

    }
}