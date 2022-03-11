using Centrino.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.SupportEntities
{
    public class Sacco:BaseEntity
    {
        
        public string SaccoName { get; set; }
        
        public Ticket Tickets { get; set; }


    }
}