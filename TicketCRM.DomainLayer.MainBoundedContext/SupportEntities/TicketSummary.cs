using System;
using TicketCRM.DomainLayer.MainBoundedContext;

namespace Centrino.DomainLayer.MainBoundedContext.SupportEntities
{
    public class TicketSummary:BaseEntity
    {
        public Guid Id { get; set; }

        public string TicketNo { get; set; }
        
        public Guid TicketStatusId { get; set; }

        public Guid CustomerId { get; set; }

        public Guid EnquiryCategoryId { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }
        
        public DateTime? ResolvedOn { get; set; }
        
        public string Username { get; set; }
        
        public Guid CareTaker { get; set; }
        
        public DateTime? ModifiedDate { get; set; }
        
        public string Description { get; set; }
        
        public string TicketStatusVal { get; set; }
        
        public string EnquiryType { get; set; }
        
        public string EnquiryCategoryVal { get; set; }
        
        public string FirstMessage { get; set; }
        
        public string Attachments { get; set; }
        
        public DateTime? ReopenedOn { get; set; }
        
        public DateTime? CancelledOn { get; set; }
        
        public DateTime? PastDueDate { get; set; }
        
        public DateTime? TransferredOn { get; set; }
        
        public DateTime? AssignedOn { get; set; }
        
        public DateTime? ClosedOn { get; set; }
        
        public int Timeline { get; set; }
        
        public string RaisedBy { get; set; }
        
        public string Remarks { get; set; }
        
        public DateTime? ExpectedDueDate { get; set; }
        
        public  string SaccoName { get; set; }
        
        public int OverDueBy { get; set; }
    }
    
}