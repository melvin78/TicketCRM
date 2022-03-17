namespace TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities
{
    public class TicketReportSummaryDTO
    {
        public Guid Id { get; set; }

        public string TicketNo { get; set; }
        
        public Guid TicketStatusId { get; set; }

        public Guid CustomerId { get; set; }

        public Guid EnquiryCategoryId { get; set; }

        public string CreatedAt { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }
        
        public string ResolvedOn { get; set; }
        
        public string Username { get; set; }
        
        public Guid CareTaker { get; set; }
        
        public string Description { get; set; }
        
        public string ModifiedDate { get; set; }
        
        public string TicketStatusVal { get; set; }
        
        public string EnquiryType { get; set; }
        
        public string EnquiryCategoryVal { get; set; }
        
        public string FirstMessage { get; set; }
        
        public string Attachments { get; set; }
        
        public string AssignedOn { get; set; }
        
        public int Timeline { get; set; }

        public string ReopenedOn { get; set; }
        
        public string CancelledOn { get; set; }
        
        public string PastDueDate { get; set; }
        
        public string TransferredOn { get; set; }
        
        public string RaisedBy { get; set; }
        
        public string ExpectedDueDate { get; set; }
        
        public string ClosedOn { get; set; }
        
        public string Remarks { get; set; }
        
        public string OverDueBy { get; set; }
        
        public string OrganizationName { get; set; }

    }
}