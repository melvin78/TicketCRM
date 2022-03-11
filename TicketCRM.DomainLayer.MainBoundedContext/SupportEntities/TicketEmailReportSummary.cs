using System;
using TicketCRM.DomainLayer.MainBoundedContext;

namespace Centrino.DomainLayer.MainBoundedContext.SupportEntities
{
    public class TicketEmailReportSummary:BaseEntity
    {
        public string TicketNo { get; set; }
        
        public string EnquiryCategoryVal { get; set; }
        
        public string EnquiryType { get; set; }
        
        public string FirstMessage { get; set; }
        
        public string Email { get; set; }
        
        public string FirstName { get; set; }
        
        public DateTime ExpectedDueDate { get; set; }
            
        public string SecondName { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public string TicketStatusVal { get; set; }
        
        public string UserName { get; set; }
        
        public string Description { get; set; }
        
        public DateTime? ClosedOn { get; set; }
        
        public string Remarks { get; set; }
        
        public string Attachments { get; set; }
        
        
    }
}