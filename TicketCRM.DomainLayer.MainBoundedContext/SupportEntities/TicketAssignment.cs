using System;
using TicketCRM.DomainLayer.MainBoundedContext;

namespace Centrino.DomainLayer.MainBoundedContext.SupportEntities
{
    public class TicketAssignment:BaseEntity
    {
        public string TicketNo { get; set; }
        
        public  Guid CustomerId { get; set; }
        
        public string CustomerFirstName { get; set; }
        
        public string CustomerSecondName { get; set; }

        public string TypeOfEnquiry { get; set; }
        
        public string TicketStatus { get; set; }
        
        public DateTime OpenedOn { get; set; }
        
        public string EnquiryMessage { get; set; }
        
        public string Attachments { get; set; }
        
        public string SaccoName { get; set; }
        
        public string EnquiryType { get; set; } 
            
        public Guid CareTaker { get; set; }
        
  
        
        
        
    }
}