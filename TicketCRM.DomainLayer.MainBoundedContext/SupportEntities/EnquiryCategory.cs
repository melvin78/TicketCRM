using System;
using System.Collections.Generic;
using TicketCRM.DomainLayer.MainBoundedContext;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace Centrino.DomainLayer.MainBoundedContext.SupportEntities
{
    public class EnquiryCategory:BaseEntity
    {
        public string EnquiryCategoryVal { get; set; }

        public Guid EnquiryId { get; set; }
        
        public Enquiries Enquiries { get; set; }
        
        public Ticket Tickets { get; set; }



    }
}