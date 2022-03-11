using System;
using TicketCRM.DomainLayer.MainBoundedContext;

namespace Centrino.DomainLayer.MainBoundedContext.SupportEntities
{
    public class Response:BaseEntity
    {
        public string ResponseText { get; set; }

        public string TicketNumber { get; set; }
        
        public bool IsRead { get; set; }
        
        public Guid InboxId { get; set; }
        
        public Guid From { get; set; }

        public Guid To { get; set; }

        public string Attachments { get; set; }
        
    }
}