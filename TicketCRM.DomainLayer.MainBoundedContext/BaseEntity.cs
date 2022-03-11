using System.Runtime.Serialization;

namespace TicketCRM.DomainLayer.MainBoundedContext
{
    public abstract class BaseEntity
    {
        public DateTime CreatedAt { get; set; }

        public Guid Id { get; set; }
        
        public string CreatedBy { get; set; }

        [DataMember]
        public virtual DateTime? ModifiedDate { get; set; }
        
        public bool IsTransient()
        {
            return Id == Guid.Empty;
        }
        public void ChangeCurrentIdentity(Guid identity)
        {
            if (identity != Guid.Empty)
            {
                Id = identity;
         
            }
        }
        public void GenerateNewIdentity()
        {
            if (IsTransient())
            {
                Id = Guid.NewGuid();

                // SequentialId = IdentityGenerator.NewSequentialGuid();
            }
        }
    }
}