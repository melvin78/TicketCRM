namespace TicketCRM.DomainLayer.MainBoundedContext.SupportEntities
{
    public class TicketStatus:BaseEntity
    {
        public string TicketStatusVal { get; set; }
        
        public Ticket Ticket { get; set; }
        

    }
}