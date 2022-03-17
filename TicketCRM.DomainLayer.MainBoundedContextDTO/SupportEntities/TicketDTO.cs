namespace TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities
{
    public class TicketDTO
    {
        public DateTime? CancelledOn { get; set; }

        public DateTime? TransferredOn { get; set; }

        public DateTime? AssignedOn { get; set; }
        
        
        public DateTime? PastDueDate { get; set; }


        public Guid CustomerId { get; set; }
        
        public DateTime? ClosedOn { get; set; }
        
        public  DateTime? ReopenedOn { get; set; }
            
        public string? Remarks { get; set; }
        
        public Guid EnquiryCategoryId { get; set; }
        
        
        public Guid EnquiryId { get; set; }

        public string FirstMessage { get; set; }
        public Guid OrganizationId { get; set; }
        
        public Guid TicketStatusId { get; set; }
        
        public string Attachments { get; set; }
        
        public Guid? CareTaker { get; set; }
        
        public DateTime? ResolvedOn { get; set; }
        
        public int PriorityLevel { get; set; }

        public DateTime? ExpectedDueDate { get; set; }
        
        public string? TicketNo { get; set; }
        
        
        public List<string> AgentAddressed { get; set; }
    
        
        
    }
}