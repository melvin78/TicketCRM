namespace TicketCRM.DomainLayer.MainBoundedContext.SupportEntities
{
    public class TicketInformation:BaseEntity
    {
        
        public string? TicketNo { get; set; }
        
        public DateTime? ReopenedOn { get; set; }

        public DateTime? ClosedOn { get; set; }
        public  DateTime? DateAssigned { get; set; }
        
        public string? Remarks { get; set; }
        public string? TicketStatusVal { get; set; }

        public string? EnquiryCategoryVal { get; set; }
        
        
        public Guid OrganizationId { get; set; }
        
        public Guid CustomerId { get; set; }
        
        public string? Description { get; set; }
        
        public string? Attachments { get; set; }
        
        public DateTime? ResolvedOn { get; set; }
        
        public string? FirstMessage { get; set; }
        
        public string? Username { get; set; }
        
    }
}