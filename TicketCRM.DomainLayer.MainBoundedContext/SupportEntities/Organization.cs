namespace TicketCRM.DomainLayer.MainBoundedContext.SupportEntities
{
    public class Organization:BaseEntity
    {
        
        public string OrganizationName { get; set; }
        

        public Ticket Ticket { get; set; }

    }
}