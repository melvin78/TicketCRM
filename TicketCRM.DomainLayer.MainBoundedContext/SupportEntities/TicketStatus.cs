using System.Collections.Generic;
using TicketCRM.DomainLayer.MainBoundedContext;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace Centrino.DomainLayer.MainBoundedContext.SupportEntities
{
    public class TicketStatus:BaseEntity
    {
        public string TicketStatusVal { get; set; }
        
        public Ticket Ticket { get; set; }
        

    }
}