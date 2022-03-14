using TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.SupportEntities
{
    public class Ticket : BaseEntity
    {
        public string TicketNo { get; set; }

        public string FirstMessage { get; set; }

        public Guid? CareTaker { get; set; }

        public Guid TicketStatusId { get; set; }

        public Guid CustomerId { get; set; }

        public Guid EnquiryCategoryId { get; set; }
        
        public  Guid EnquiryId { get; set; }


        public Guid SaccoId { get; set; }

        public string Attachments { get; set; }


        public string Remarks { get; set; }

        public DateTime? ClosedOn { get; set; }

        public DateTime? ResolvedOn { get; set; }

        public int PriorityLevel { get; set; }

        public DateTime? ReopenedOn { get; set; }


        public DateTime? CancelledOn { get; set; }

        public DateTime? TransferredOn { get; set; }

        public DateTime? AssignedOn { get; set; }
        
        
        public DateTime? PastDueDate { get; set; }
        
        public DateTime? ExpectedDueDate { get; set; }
        
        public List<EnquiryCategory> EnquiryCategory { get; set; }

        public List<Organization> Sacco { get; set; }

        public List<TicketStatus> TicketStatus { get; set; }

        // public List<ApplicationUser> ApplicationUsers { get; set; }
    }
}